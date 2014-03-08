using UnityEngine;
using System.Collections;

public class ButtonRestart : MonoBehaviour {

	void OnClick()
	{
		Application.LoadLevel (Application.loadedLevelName);
	}
}
