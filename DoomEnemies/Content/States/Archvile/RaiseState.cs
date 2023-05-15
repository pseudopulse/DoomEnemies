using System;

namespace DoomEnemies.States.Archvile {
    public class RaiseState : BaseActorState {
        public List<CharacterMaster> dead = new();
        public override void OnEnter()
        {
            base.OnEnter();

            foreach (CharacterMaster master in CharacterMaster.readOnlyInstancesList) {
                if (master.teamIndex == TeamIndex.Monster && !master.hasBody && Vector3.Distance(master.deathFootPosition, base.transform.position) < 30f) {
                    Debug.Log("adding dead enemy: " + master);
                    dead.Add(master);
                }
            }

            if (dead.Count <= 0) {
                outer.SetNextStateToMain(); 
                return; // exit if no dead enemies exist
            }

            controller.SetNextAnimation(AnimationType.Melee); // vile raise anim
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            base.inputBank.moveVector = Vector3.zero;
            base.characterMotor.velocity = Vector3.zero;
            base.characterMotor.moveDirection = Vector3.zero;
        }

        public override void OnAnimationEvent(AnimationType type)
        {
            outer.SetNextStateToMain();
        }

        public override void OnExit()
        {
            base.OnExit();
            // Debug.Log("dead count: " + dead.Count);
            foreach (CharacterMaster master in dead) {
                master.RespawnExtraLife();
                // Debug.Log("respawning: " + master);
            }
        }
    }
}