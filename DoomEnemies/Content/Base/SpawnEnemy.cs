using System;

namespace DoomEnemies {
    public class SpawnEnemy : MonoBehaviour {
        public List<GameObject> masters;
        public GameObject effect;
        public void Spawn(ProjectileImpactInfo info) {
            ProjectileController controller = GetComponent<ProjectileController>();
            GameObject owner = controller.owner;

            MasterSummon summon = new();
            summon.position = info.estimatedPointOfImpact + Vector3.up;
            summon.ignoreTeamMemberLimit = true;
            summon.useAmbientLevel = true;
            summon.rotation = Quaternion.identity;
            summon.summonerBodyObject = owner;
            summon.masterPrefab = masters[UnityEngine.Random.Range(0, masters.Count)];

            summon.Perform();
            EffectManager.SpawnEffect(effect, new EffectData {
                origin = summon.position,
            }, true);

            Destroy(gameObject);
        }
    }
}