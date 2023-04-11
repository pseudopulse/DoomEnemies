using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Reflection;

namespace DoomEnemies {
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
    
    public class DoomEnemies : BaseUnityPlugin {
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "pseudopulse";
        public const string PluginName = "DoomEnemies";
        public const string PluginVersion = "1.0.0";

        public static AssetBundle bundle;
        public static BepInEx.Logging.ManualLogSource ModLogger;

        public void Awake() {
            // assetbundle loading 
            bundle = AssetBundle.LoadFromFile(Assembly.GetExecutingAssembly().Location.Replace("DoomEnemies.dll", "wheresallthedata"));

            // set logger
            ModLogger = Logger;
        }
    }
}