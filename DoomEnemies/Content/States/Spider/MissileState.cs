using System;

namespace DoomEnemies.States.Spider {
    public class MissileState : BaseActorState {
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
            BulletAttack attack = new();
            attack.owner = base.gameObject;
            attack.aimVector = base.GetAimRay().direction;
            attack.origin = base.transform.position;
            attack.damage = base.damageStat * 0.6f;
            attack.falloffModel = BulletAttack.FalloffModel.DefaultBullet;
            attack.muzzleName = "Muzzle";
            attack.procChainMask = new();
            attack.maxSpread = 3;
            attack.radius = 1;
            attack.bulletCount = 3;
            attack.tracerEffectPrefab = EntityStates.Commando.CommandoWeapon.FireBarrage.tracerEffectPrefab;

            attack.Fire();
            
            outer.SetNextStateToMain();
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Frozen;
        }
    }
}