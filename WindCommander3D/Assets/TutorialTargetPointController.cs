using UnityEngine;
using System.Collections;

public class TutorialTargetPointController : MonoBehaviour {

    public TutorialStateSelector tutorialStateSelector;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider other)
    {
        tutorialStateSelector.currentState = TutorialStateSelector.TutorialState.Outro;
    }
}
