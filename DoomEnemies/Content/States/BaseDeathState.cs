using System;

namespace DoomEnemies.States {
    public class BaseDeathState : BaseActorState {
        public override void OnEnter()
        {
            base.OnEnter();
            controller.SetNextAnimation(AnimationType.Death);
        }

        public override void OnAnimationEvent(AnimationType type)
        {
            if (type == AnimationType.Death) {
                GameObject.Destroy(base.GetModelTransform().gameObject, 2f);
                Destroy(base.gameObject);
            }
        }

        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Death;
        }
    }
}