using UnityEngine;
using System.Collections;

public class ButtonStartTutorial : MonoBehaviour {

    public TutorialStateSelector tutorialStateSelector;

	void OnClick()
    {
        tutorialStateSelector.currentState = TutorialStateSelector.TutorialState.Playing;
    }
}
