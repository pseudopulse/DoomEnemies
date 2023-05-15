using System;

namespace DoomEnemies.States.Baron {
    public class MissileState : BaseActorState {
        public GameObject prefab => Main.bundle.LoadAsset<GameObject>("BaronFireball.prefab");
        public override void OnEnter()
        {
            base.OnEnter();
            controller.SetNextAnimation(AnimationType.Missile);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            base.characterDirection.forward = base.GetAimRay().direction;
        }

        public override void OnAnimationEvent(AnimationType type)
        {
            FireProjectileInfo info = new();
            info.damage = base.damageStat * 12f;
            info.crit = base.RollCrit();
            info.position = base.transform.position + (Vector3.up);
            info.owner = base.gameObject;
            info.rotation = Util.QuaternionSafeLookRotation(base.GetAimRay().direction);
            info.projectilePrefab = prefab;

            ProjectileManager.instance.FireProjectile(info);
            
            outer.SetNextStateToMain();
        }
    }
}