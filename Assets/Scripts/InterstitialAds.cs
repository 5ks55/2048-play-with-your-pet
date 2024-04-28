using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class InterstitialAds : MonoBehaviour
{
    private InterstitialAd interstitial;
    private string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //test

    private void Start()
    {
        this.interstitial = new InterstitialAd(adUnitId);

        RegisterReloadHandler(this.interstitial);

        LoadInterstitialAd();
    }

    public void Show()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
        else
        {
            LoadInterstitialAd();
        }
    }

    private void RegisterReloadHandler(InterstitialAd interstitialAd)
    {
        interstitialAd.OnAdClosed += HandleOnAdClosed;

        interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
    }

    private void HandleOnAdClosed(object sender, EventArgs args)
    {
        LoadInterstitialAd();
    }

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        LoadInterstitialAd();
    }

    private void LoadInterstitialAd()
    {
        AdRequest request = new AdRequest.Builder().Build();

        this.interstitial.LoadAd(request);
    }

}
