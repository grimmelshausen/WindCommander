using UnityEngine;
using System.Collections;

public class HelpGUIToggle : MonoBehaviour {

    public KeyCode toggleKey;


	void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            BroadcastMessage("OnHelpGUIToggle");
        }
    }
}
