using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLauncherHome : MonoBehaviour {

    //Make sure to attach these Buttons in the Inspector
	public Button homeButton;

    void Start()
    {
		Button btnHome = homeButton.GetComponent<Button>();
		btnHome.onClick.AddListener(LevelChangeHome);
    }

    public void LevelChangeHome() {
        Application.LoadLevel("Home");
    }
}
