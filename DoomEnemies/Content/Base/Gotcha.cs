using System;

namespace DoomEnemies {
    // forces cyberdemons and spider masterminds to fight
    public class Gotcha : MonoBehaviour { 
        private BaseAI ai;
        private float stopwatch = 0f;
        private BodyIndex cyber;
        private CharacterMaster self;
        public void Start() {
            ai = GetComponent<BaseAI>();
            cyber = BodyCatalog.FindBodyIndex("CyberdemonBody");
            self = GetComponent<CharacterMaster>();
        }

        public void FixedUpdate() {
            stopwatch += Time.fixedDeltaTime;
            if (stopwatch >= 3f) {
                stopwatch = 0f;

                foreach (CharacterMaster master in CharacterMaster.readOnlyInstancesList) {
                    CharacterBody body = master.GetBody();
                    BaseAI target = master.GetComponent<BaseAI>();
                    if (!body || !target) {
                        continue;
                    }

                    if (body.bodyIndex != cyber) {
                        continue;
                    }

                    ai.currentEnemy.gameObject = body.gameObject;
                    target.currentEnemy.gameObject = self.GetBodyObject();
                }
            }
        }
    }
}