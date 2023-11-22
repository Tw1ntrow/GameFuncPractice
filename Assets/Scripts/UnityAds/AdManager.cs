using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private string gameId = "5482497";
    private string videoPlacementId = "video";
    private string bannerPlacementId = "banner";

    void Start()
    {
        // テストモードで初期化
        // UnityCloudからもテストモードにする必要がある
        Advertisement.Initialize(gameId, true, this);
        Advertisement.Load(videoPlacementId, this);
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);
    }

    public void ShowVideoAd()
    {
        if (Advertisement.isInitialized)
        {
            Advertisement.Show(videoPlacementId, this);
        }
        else
        {
            Debug.Log("Video ad not ready.");
        }
    }

    public void ShowBannerAd()
    {
        if (Advertisement.isInitialized)
        {   
            Advertisement.Banner.Show(bannerPlacementId);
        }
        else
        {
            Debug.Log("Banner ad not ready.");
        }
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Ad Loaded: {placementId}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Failed to load Ad ({placementId}): {message}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"Ad Show Complete ({placementId}): {showCompletionState}");
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Failed to show Ad ({placementId}): {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log($"Ad Show Start: {placementId}");
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log($"Ad Clicked: {placementId}");
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Failed to initialize Unity Ads: {message}");
    }
}