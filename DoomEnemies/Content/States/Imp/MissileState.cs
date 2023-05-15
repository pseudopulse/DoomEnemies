using System;

namespace DoomEnemies.States.Imp {
    public class MissileState : BaseActorState {
        public GameObject prefab => Main.bundle.LoadAsset<GameObject>("ImpFireball.prefab");
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
            info.damage = base.damageStat * 5f;
            info.crit = base.RollCrit();
            info.position = base.FindModelChild("Muzzle").position;
            info.owner = base.gameObject;
            info.rotation = Util.QuaternionSafeLookRotation(base.GetAimRay().direction);
            info.projectilePrefab = prefab;

            ProjectileManager.instance.FireProjectile(info);
            
            outer.SetNextStateToMain();
        }
    }
}