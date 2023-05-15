using System;

namespace DoomEnemies {
    public class SpawnPoints : MonoBehaviour {
        public static SpawnPoints instance;
        public List<Transform> Points;
        private int index;

        public Transform GetPoint() {
            Transform point = Points[index];

            index++;
            if (index > Points.Count - 1) {
                index = 0;
            }

            return point;
        }
    }
}