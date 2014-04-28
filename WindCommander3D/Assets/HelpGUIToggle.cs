using UnityEngine;
using System.Collections;

public class HelpGUIToggle : MonoBehaviour {

    public KeyCode toggleKey;

	void Start()
	{
		BroadcastMessage("OnHelpGUIToggle"); //to start, disable the gui
	}

	void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            BroadcastMessage("OnHelpGUIToggle");
        }
    }
}
