using UnityEngine;
using System.Collections;

public class TogglableHelpGUI : MonoBehaviour {

    void OnHelpGUIToggle()
    {
        //toggle the rendereres of the children      
        foreach (Renderer rc in GetComponentsInChildren<Renderer>())
        {   
            rc.enabled = !rc.enabled;
        }
    }
}
