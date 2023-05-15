using System;

namespace DoomEnemies.States.Chaingunner {
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
            attack.damage = base.damageStat;
            attack.falloffModel = BulletAttack.FalloffModel.DefaultBullet;
            attack.muzzleName = "Muzzle";
            attack.procChainMask = new();
            attack.maxSpread = 1;
            attack.radius = 1;
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