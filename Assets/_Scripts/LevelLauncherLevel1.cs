using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLauncherLevel1 : MonoBehaviour {

    //Make sure to attach these Buttons in the Inspector
    public Button level1Button;

    void Start()
    {
        Button btnLevel1 = level1Button.GetComponent<Button>();
        btnLevel1.onClick.AddListener(LevelChangeLevel1);
    }

    public void LevelChangeLevel1() {
        Application.LoadLevel("Level1");
    }
}
