using System;

namespace DoomEnemies.States {
    public class BaseActorState : BaseState {
        public SpriteController controller;
        public override void OnEnter()
        {
            base.OnEnter();
            controller = base.characterBody.GetComponent<SpriteController>();
            controller.OnAnimationEvent += OnAnimationEvent;
        }

        public virtual void OnAnimationEvent(AnimationType type) {
            
        }

        public override void OnExit()
        {
            base.OnExit();
            controller.OnAnimationEvent -= OnAnimationEvent;
        }
    }
}