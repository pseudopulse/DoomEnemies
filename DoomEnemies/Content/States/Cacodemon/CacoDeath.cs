using System;

namespace DoomEnemies.States.Cacodemon {
    public class CacoDeath : BaseDeathState {
        private bool hasFinished = false;
        public override void OnAnimationEvent(AnimationType type)
        {
            hasFinished = true;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            base.rigidbody.useGravity = true;
            base.characterMotor._flightParameters.channeledFlightGranterCount = 0;
            base.characterMotor._gravityParameters.channeledAntiGravityGranterCount = 0;
            base.characterMotor._gravityParameters.environmentalAntiGravityGranterCount = 0;
            base.characterMotor.isFlying = false;
            base.characterMotor.useGravity = true;
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            base.rigidbody.useGravity = true;
            base.characterMotor._flightParameters.channeledFlightGranterCount = 0;
            base.characterMotor._gravityParameters.channeledAntiGravityGranterCount = 0;
            base.characterMotor._gravityParameters.environmentalAntiGravityGranterCount = 0;
            base.characterMotor.isFlying = false;
            base.characterMotor.useGravity = true;
            if (base.characterMotor.isGrounded && hasFinished) {
                GameObject.Destroy(base.gameObject);
                GameObject.Destroy(base.GetModelTransform().gameObject, 2f);
            }
        }
    }
}