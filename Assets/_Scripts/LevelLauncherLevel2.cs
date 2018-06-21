using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLauncherLevel2 : MonoBehaviour {

    //Make sure to attach these Buttons in the Inspector
    public Button level2Button;

    void Start()
    {
        Button btnLevel2 = level2Button.GetComponent<Button>();
        btnLevel2.onClick.AddListener(LevelChangeLevel2);
    }

    public void LevelChangeLevel2() {
        Application.LoadLevel("Level2");
    }
}

