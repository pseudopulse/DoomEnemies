using System;

namespace DoomEnemies.States.Archvile {
    public class MissileState : BaseActorState {
        public GameObject prefab => Main.bundle.LoadAsset<GameObject>("ArchvileFlame.prefab");
        public float delay = 0.5f; // spawn the flame around halfway through the animation

        public override void OnEnter()
        {
            base.OnEnter();
            controller.SetNextAnimation(AnimationType.Missile);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            
            base.inputBank.moveVector = Vector3.zero;
            base.characterMotor.velocity = Vector3.zero;
            base.characterMotor.moveDirection = Vector3.zero;

            if (base.fixedAge >= delay) {
                FireProjectileInfo info = new();
                info.position = base.characterBody?.master?.GetComponent<BaseAI>()?.currentEnemy?.lastKnownBullseyePosition.Value ?? base.GetAimRay().GetPoint(10);
                info.rotation = Quaternion.identity;
                info.damage = base.damageStat * 10f;
                info.crit = base.RollCrit();
                info.owner = base.gameObject;
                info.projectilePrefab = prefab;

                ProjectileManager.instance.FireProjectile(info);
                delay = 10000f; // only fire once
            }
        }

        public override void OnAnimationEvent(AnimationType type)
        {
            base.OnAnimationEvent(type);
            outer.SetNextStateToMain();
        }
    }
}