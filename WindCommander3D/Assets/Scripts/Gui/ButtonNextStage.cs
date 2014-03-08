using UnityEngine;
using System.Collections;

public class ButtonNextStage : MonoBehaviour {

	public TutorialStateSelector tutorialStateSelector;
	
	void OnClick()
	{
		tutorialStateSelector.NextState ();
	}

}
