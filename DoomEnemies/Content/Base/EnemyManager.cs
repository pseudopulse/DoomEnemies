using System;

namespace DoomEnemies {
    public class EnemyManager {
        public static void Scan() {
            List<EnemyDef> defs = Main.bundle.LoadAllAssets<EnemyDef>().ToList();

            foreach (EnemyDef def in defs) {
                def.Setup();
            }

            CharacterCameraParams p = Utils.Paths.CharacterCameraParams.ccpStandard.Load<CharacterCameraParams>();

            List<GameObject> objects = Main.bundle.LoadAllAssets<GameObject>().ToList();

            foreach (GameObject obj in objects) {
                if (obj.GetComponent<EffectComponent>()) {
                    ContentAddition.AddEffect(obj);
                }

                if (obj.GetComponent<CameraTargetParams>()) {
                    obj.GetComponent<CameraTargetParams>().cameraParams = p;
                }
            }

            Hooks();
        }

        private static void Hooks() {
            On.RoR2.CharacterMaster.OnBodyDeath += VileCheck;
        }

        private static void VileCheck(On.RoR2.CharacterMaster.orig_OnBodyDeath orig, CharacterMaster self, CharacterBody body) {
            if (!self.destroyOnBodyDeath) {
                orig(self, body);
                return;
            }

            BodyIndex VileIndex = BodyCatalog.FindBodyIndex("ArchvileBody");

            bool foundVile = false;

            foreach (TeamComponent com in TeamComponent.GetTeamMembers(TeamIndex.Monster)) {
                if (com.body && com.body.bodyIndex == VileIndex) {
                    foundVile = true;
                    break;
                }
            }

            if (foundVile) {
                self.destroyOnBodyDeath = false;
                self.AddComponent<VileChecker>();
            }

            orig(self, body);
        }
    }
}