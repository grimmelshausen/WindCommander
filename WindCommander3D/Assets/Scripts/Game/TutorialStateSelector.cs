using UnityEngine;
using System.Collections;

public class TutorialStateSelector : MonoBehaviour {

    public enum TutorialState
    {
        Intro = 1,
        Stage1 = 2,
		Intermezzo1 = 3,
		Stage2 = 4,
        Outro = 5,
        End = 6,
    }
    
    public TutorialState currentState = TutorialState.Intro;

	public void NextState()
	{
		currentState += 1;
	}

}
