using UnityEngine;
using System.Collections;

public class TutorialStateSelector : MonoBehaviour {

    public enum TutorialState
    {
        Intro,
        Playing,
        Outro,
        End,
    }
    
    public TutorialState currentState = TutorialState.Intro;

}
