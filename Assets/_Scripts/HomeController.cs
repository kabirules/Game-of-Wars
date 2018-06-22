using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;


public class HomeController : MonoBehaviour {

	private adShown = false;

	// Use this for initialization
	void Start () {
		// ADMOB
		Init();
		RequestInterstitial();
		// Create an empty ad request.
    	AdRequest request = new AdRequest.Builder().Build();
    	// Load the interstitial with the request.
    	interstitial.LoadAd(request);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit(); 
        }
		if (interstitial.IsLoaded() && !adShown) {
    		interstitial.Show();
			adShown = true;
			//interstitial.Destroy();
  		}

	}

	void Init()
	{
		#if UNITY_ANDROID
            string appId = "ca-app-pub-2228911308495304~5361337236";
        #elif UNITY_IPHONE
            string appId = "unexpected_platform";
        #else
            string appId = "unexpected_platform";
        #endif
		// Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
	}

	private void RequestInterstitial()
	{
		#if UNITY_ANDROID
			string adUnitId = "ca-app-pub-2228911308495304/9919954379";
		#elif UNITY_IPHONE
			string adUnitId = "unexpected_platform";
		#else
			string adUnitId = "unexpected_platform";
		#endif

		// Initialize an InterstitialAd.
		string adUnitId = "ca-app-pub-3940256099942544/1033173712"; //TODO test - remove
		InterstitialAd interstitial = new InterstitialAd(adUnitId);
	}


}
