using System.IO;
using LittleBit.Modules.Analytics.EventSystem;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using UnityEditor;
using UnityEngine;

namespace InternalAssets.Scripts.LittleBit.Modules.Analytics.Editor
{
    public static class ToolbarMenu
    {
        private const string ConfigsFolderPath = "Assets/Resources/Configs";
        private const string ResourcesPath = "Assets/Resources";
        private const string ConfigFolderName = "Configs";
        private const string Extension = ".asset";

        [MenuItem("Tools/Analytics/Create Config")]
        private static void CreateConfig()
        {
            var instance = ScriptableObject.CreateInstance<AnalyticsConfig>();
            
            CheckOrCreateDirectory();
            
            AssetDatabase.CreateAsset(instance, GetFilePath());

            AssetDatabase.SaveAssets();
        }

        private static void CheckOrCreateDirectory()
        {
            if (AssetDatabase.IsValidFolder(ResourcesPath) == false)
            {
                AssetDatabase.CreateFolder("Assets", "Resources");
                AssetDatabase.CreateFolder(ResourcesPath, ConfigFolderName);
                return;
            }

            if (AssetDatabase.IsValidFolder(ConfigsFolderPath) == false)
                AssetDatabase.CreateFolder(ResourcesPath, ConfigFolderName);
        }

        private static string GetFilePath() => Path.Combine(ResourcesPath, Constants.AnalyticsConfigResourcesPath) + Extension;
    }
}
