using System.IO;

using UnityEditor;

public class AssetBundlesBuilder
{
    private static string _outputPath = "Assets/AssetBundles";

    [MenuItem("Assets/Build AssetBundles")]
    public static void BuildAllAssetBundles()
    {
        if (!Directory.Exists(_outputPath))
        {
            Directory.CreateDirectory(_outputPath);
        }

        BuildPipeline.BuildAssetBundles(_outputPath, BuildAssetBundleOptions.None, BuildTarget.Android);
    }
}
