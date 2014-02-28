using UnityEngine;
using System.Collections;

public class ButtonEndTutorial : MonoBehaviour {

    public TutorialStateSelector tutorialStateSelector;

    void OnClick()
    {
        tutorialStateSelector.currentState = TutorialStateSelector.TutorialState.End;
    }
}
