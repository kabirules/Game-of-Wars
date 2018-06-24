using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLauncherLevel1 : MonoBehaviour {

    //Make sure to attach these Buttons in the Inspector
    public Button level1Button;
    public GameObject adsControllerGO;

    private AdsController adsController;

    void Start()
    {
        adsController = adsControllerGO.GetComponent<AdsController>();
        Button btnLevel1 = level1Button.GetComponent<Button>();
        btnLevel1.onClick.AddListener(LevelChangeLevel1);
    }

    public void LevelChangeLevel1() {
        adsController.DestroyBanner();
        Application.LoadLevel("Level1");
    }
}
