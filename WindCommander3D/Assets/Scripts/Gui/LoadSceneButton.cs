using UnityEngine;
using System.Collections;

public class LoadSceneButton : MonoBehaviour {

    public string levelName;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnClick()
    {
        Application.LoadLevel(levelName);
    }
}
