using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class BannerAdExample : MonoBehaviour
{


    [SerializeField] BannerPosition _bannerPosition = BannerPosition.TOP_CENTER;

    [SerializeField] string _androidAdUnitId = "Banner_Android";
    [SerializeField] string _iOSAdUnitId = "Banner_iOS";
    string _adUnitId = null; // This will remain null for unsupported platforms.

    [SerializeField] GameObject image;

    public bool bannerisLoaded;

    void Start()
    {
        // Get the Ad Unit ID for the current platform:
        // #if UNITY_IOS
        //         _adUnitId = _iOSAdUnitId;
        // #elif UNITY_ANDROID
        //         _adUnitId = _androidAdUnitId;
        // #endif
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSAdUnitId
            : _androidAdUnitId;

        // Set the banner position:
        Advertisement.Banner.SetPosition(_bannerPosition);

    }

    // Implement a method to call when the Load Banner button is clicked:
    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adUnitId, options);
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
        bannerisLoaded = true;
        // ShowBannerAd();
        //  Instantiate(image, image.transform.position, Quaternion.identity, GameObject.FindObjectOfType<Canvas>().transform);
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
        bannerisLoaded = false;
        // Instantiate(image, Vector3.zero, Quaternion.identity, GameObject.FindObjectOfType<Canvas>().transform);
    }

    // Implement a method to call when the Show Banner button is clicked:
    public void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitId, options);
    }

    // Implement a method to call when the Hide Banner button is clicked:
    void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }


}