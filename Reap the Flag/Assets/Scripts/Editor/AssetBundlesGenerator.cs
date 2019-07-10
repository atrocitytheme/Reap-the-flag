using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class AssetBundlesGenerator : Editor
{
    // Start is called before the first frame update
    [MenuItem("Assets/Build Bundles")]
    static void BuildAllAssets() {
        BuildPipeline.BuildAssetBundles("Assets/Resources/Bundles", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.StandaloneWindows64);
    }
}
