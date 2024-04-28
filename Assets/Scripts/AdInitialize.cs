using GoogleMobileAds.Api;
using UnityEngine;

public class AdInitialize : MonoBehaviour
{
    void Awake()
    {
        MobileAds.Initialize(initStatus => { });
    }
}
