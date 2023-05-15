using System;

namespace DoomEnemies.States.Icon {
    public class Spawn : BaseState {
        public GameObject prefab => Main.bundle.LoadAsset<GameObject>("SpawnCube.prefab");
        public override void OnEnter()
        {
            base.OnEnter();

            Transform origin = FindModelChild("Spawner");

            FireProjectileInfo info = new();
            info.position = origin.position;
            info.rotation = Util.QuaternionSafeLookRotation(base.GetAimRay().direction);
            info.owner = base.gameObject;
            info.projectilePrefab = prefab;

            ProjectileManager.instance.FireProjectile(info);
            outer.SetNextStateToMain();
        }
    }
}