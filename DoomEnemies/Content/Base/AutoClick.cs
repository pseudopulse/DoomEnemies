using System;

namespace DoomEnemies {
    public class AutoClick : MonoBehaviour {
        public InputBankTest bank;

        public void FixedUpdate() {
            bank.skill1.PushState(true);
        }
    }
}