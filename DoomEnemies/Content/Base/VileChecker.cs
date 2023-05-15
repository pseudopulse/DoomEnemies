using System;

namespace DoomEnemies {
    // kills the master if there are no archviles alive
    public class VileChecker : MonoBehaviour {
        private BodyIndex VileIndex;
        private float stopwatch = 0f;
        private CharacterMaster master;
        public void Start() {
            VileIndex = BodyCatalog.FindBodyIndex("ArchvileBody");
            master = GetComponent<CharacterMaster>();
        }

        public void FixedUpdate() {
            stopwatch += Time.fixedDeltaTime;

            if (stopwatch >= 4f) {
                stopwatch = 0f;

                bool foundVile = false;

                foreach (TeamComponent com in TeamComponent.GetTeamMembers(TeamIndex.Monster)) {
                    if (com.body && com.body.bodyIndex == VileIndex) {
                        foundVile = true;
                        break;
                    }
                }

                if (!foundVile) {
                    master.destroyOnBodyDeath = true;

                    if (!master.hasBody) {
                        Debug.Log("destroying due to no vile: " + master);
                        Destroy(master.gameObject);
                    }
                }
            }
        }
    }
}