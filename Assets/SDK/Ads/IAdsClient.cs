using System;

namespace VC.Ads
{
    public interface IAdsClient
    {
        /// <summary>
        /// NetWork 提供商
        /// </summary>
        string AdProvider { get; }
        event Action OnSdkInitalized;

        event Action<string> OnBannerOpened;
        event Action<string> OnBannerHided;
        event Action<string> OnBannerClicked;

        event Action<string> OnRewardVideoClosed;
        event Action<string> OnRewardVideoClicked;
        event Action<string> OnRewardVideoLoaded;
        event Action<string> OnRewardVideoOpened;
        event Action<string> OnRewardVideoReceivedReward;
        event Action<string, string> OnRewardVideoLoadFailed;
        event Action<string, string> OnRewardVideoFailedToPlay;
       
        event Action<string> OnInterstitialClosed;
        event Action<string> OnInterstitialClicked;
        event Action<string> OnInterstitialLoaded;
        event Action<string> OnInterstitialOpened;
        event Action<string, string> OnInterstitialLoadFailed;

        

        void CreateBanner(BannerPosition bannerPosition);
        void ShowBanner(BannerPosition bannerPosition);

        void HideBanner();
        void DestroyBanner();

        void FetchInterstitial();
        void FetchRewardVideo();

        bool IsRewardVideoAvailable();
        bool IsInterstitialAvailable();
        void ShowInterstitial(Action<InterstitialStatus> callback);

        void SetPrivacy(bool isProtect);
        void ShowRewardVideo(Action rewardVideoOpenEvent, Action rewardVideoReceivedRewardEvent, Action rewardVideoCloseEvent);
    }
}