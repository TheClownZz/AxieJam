using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : MonoSingleton<ServiceManager>
{
    const string NoAdsKey = "NoAds";
    public void OnInit()
    {

    }
    public bool IsNoAds()
    {
        return PlayerPrefs.GetInt(NoAdsKey, 0) != 0;
    }

    public void ShowAdUnit(Action actionDone, string position, bool ignore_ads_interval = false)
    {
#if !HAS_SERVICE
        actionDone();
#else
        AdsManager.instance.ShowAdUnit(actionDone, position, ignore_ads_interval);
#endif
    }

    public void ShowRewardVideo(Action actionDone, string position = "")
    {
#if !HAS_SERVICE
        actionDone();
#else
        AdsManager.instance.ShowRewardVideo(actionDone, position);
#endif
    }

    public void ShowBanner()
    {
#if HAS_SERVICE
         if (!IsNoAds())
         {
           MaxSdk.ShowBanner(AdsManager.BannerAdUnitId);
         }
#endif
    }

    public void HideBanner()
    {
#if HAS_SERVICE
        MaxSdk.HideBanner(AdsManager.BannerAdUnitId);
#endif

    }

    public void LogEvent(string nameEv)
    {
#if HAS_SERVICE
        AnalyticsManager.instance.LogEvent(nameEv);
#endif
    }

    public void TryInitAndRequestAd()
    {
#if HAS_SERVICE
        AnalyticsManager.instance.TryInitAndRequestAd();
#endif
    }
}
