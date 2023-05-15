using System;
using Unity;

namespace DoomEnemies {
    [CreateAssetMenu(fileName = "EnemyDef", menuName = "ScriptableObjects/EnemyDef", order = 1)]
    public class EnemyDef : ScriptableObject {
        public List<Token> Tokens;
        public GameObject BodyPrefab;
        public GameObject MasterPrefab;
        public List<DirectorAPI.Stage> Stages;
        public int Credits;
        public int MinimumCompletions;
        public DirectorAPI.MonsterCategory Category;
        public bool isFlying = false;
        public bool isBoss = false;

        public void Setup() {
            if (!BodyPrefab || !MasterPrefab) {
                return;
            }
            ContentAddition.AddBody(BodyPrefab);
            ContentAddition.AddMaster(MasterPrefab);

            SetupLanguage();
            SetupDirector();
        }

        private void SetupLanguage() {
            foreach (Token pair in Tokens) {
                LanguageAPI.Add(pair.Key, pair.Value);
            }
        }   

        private void SetupDirector() {
            DirectorCard card = new();
            card.selectionWeight = 1;
            card.minimumStageCompletions = MinimumCompletions;
            card.spawnDistance = DirectorCore.MonsterSpawnDistance.Standard;

            CharacterSpawnCard csc = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            csc.directorCreditCost = Credits;
            csc.prefab = MasterPrefab;
            csc.name = "csc" + BodyPrefab.name;
            csc.nodeGraphType = isFlying ? RoR2.Navigation.MapNodeGroup.GraphType.Air : RoR2.Navigation.MapNodeGroup.GraphType.Ground;
            csc.sendOverNetwork = true;
            csc.eliteRules = SpawnCard.EliteRules.ArtifactOnly;
            csc.noElites = true;
            csc.occupyPosition = true;

            card.spawnCard = csc;
            
            foreach (DirectorAPI.Stage stage in Stages) {
                #pragma warning disable
                // obsolete? ok and?
                DirectorAPI.Helpers.AddNewMonsterToStage(card, Category, stage);
                #pragma warning restore
            }
        }
    }

    [Serializable]
    public struct Token {
        public string Key;
        public string Value;
    }
}