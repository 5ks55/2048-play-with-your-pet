using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class BannerAds : MonoBehaviour
{
    private BannerView bannerView;
    private string adUnitId = "ca-app-pub-3940256099942544/6300978111"; //test

    private void Start()
    {
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        bannerView.OnAdLoaded += HandleOnAdLoaded;
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }

    private void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("Banner ad loaded successfully.");
    }

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        ReloadBannerAd();
    }

    private void ReloadBannerAd()
    {
        bannerView.Destroy(); 

        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
    }

    private void OnDestroy()
    {
        if (bannerView != null)
        {
            bannerView.Destroy();
        }
    }
}
