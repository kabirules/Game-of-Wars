using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;

public class AdsController : MonoBehaviour {

	private bool adShown;
	private BannerView bannerView;
    private bool bannerDestroyed;	

	// Use this for initialization
	void Start () {
		bannerDestroyed = true; // Banner not exist at beginning
        #if UNITY_ANDROID
            string appId = "ca-app-pub-2228911308495304~5361337236"; //confirmed
        #elif UNITY_IPHONE
            string appId = "ca-app-pub-2228911308495304~2948884166"; //confirmed
        #else
            string appId = "unexpected_platform";
        #endif

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);

		string m_Scene = SceneManager.GetActiveScene().name;
		if (m_Scene == "Home") 
		{
			this.RequestBanner();
		}
	}

    private void RequestBanner()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-2228911308495304/7165373495"; //confirmed
            // string adUnitId = "ca-app-pub-3940256099942544/6300978111"; //test
        #elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-2228911308495304/7410873351"; //confirmed
            // string adUnitId = "ca-app-pub-3940256099942544/2934735716"; //test
        #else
            string adUnitId = "unexpected_platform";
        #endif

        // Create a 320x50 banner at the top of the screen.
        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        bannerView.LoadAd(request);		
		bannerDestroyed = false;
    }

    public void DestroyBanner() 
    {   
        if (!bannerDestroyed)
        {
            bannerView.Destroy();
            bannerDestroyed = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
