using System.IO;
using LittleBit.Modules.Analytics.EventSystem;
using LittleBit.Modules.Analytics.EventSystem.Configs;
using UnityEditor;
using UnityEngine;

namespace InternalAssets.Scripts.LittleBit.Modules.Analytics.Editor
{
    public static class ToolbarMenu
    {
        private const string Extension = ".asset";
        private const string RootFolder = "Assets/Resources";

        [MenuItem("Tools/Analytics/Create Config")]
        private static void CreateConfig()
        {
            var instance = ScriptableObject.CreateInstance<AnalyticsConfig>();

            AssetDatabase.CreateAsset(instance, GetFilePath());

            AssetDatabase.SaveAssets();
        }

        private static string GetFilePath() => Path.Combine(RootFolder, Constants.AnalyticsConfigResourcesPath) + Extension;
    }
}