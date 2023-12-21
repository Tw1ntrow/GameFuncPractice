using UnityEngine;

public class AssetMemoryDebugManager : MonoBehaviour
{
    [SerializeField]
    private bool showDebugButton = true;

    void OnGUI()
    {
        if (showDebugButton)
        {
            if (GUI.Button(new Rect(10, 10, 400, 100), "Release Assets"))
            {
                ReleaseAssets();
                RunGarbageCollection();
            }
        }
    }

    void ReleaseAssets()
    {
        Resources.UnloadUnusedAssets();
        Debug.Log("ReleasedAssets");
    }

    void RunGarbageCollection()
    {
        System.GC.Collect();
        System.GC.WaitForPendingFinalizers();
        System.GC.Collect();
        Debug.Log("GarbageCollectionDone");
    }
}