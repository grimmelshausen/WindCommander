using UnityEngine;
using System.Collections;

/************************************************************************/
/* This is the main tutorial handler controller class that organizes
 * the logic of the tutorial. Basically it uses the TutorialState to
 * determine what to do.
/************************************************************************/
public class Tutorial1Rudder : MonoBehaviour {


    public TutorialStateSelector tutorialStateSelector;
    public ShipController shipController;
    public ShipRudderForce shipRudderForce;
    public ShipWindForce shipWindForce;
    public WindController wind;
    public ShipMainSailForce shipMainSailForce;
    public UIPanel tutorialIntroPanel;
    public UIPanel tutorialOutroPanel;
    public SailController sailController;

    private float sailControllerSpeedSave;

	// Use this for initialization
	void Start () {
        tutorialStateSelector.currentState = TutorialStateSelector.TutorialState.Intro;
        sailControllerSpeedSave = sailController.mainSailMoveSpeed;
	}
	
	// Update is called once per frame
	void Update () {

        switch (tutorialStateSelector.currentState)
        {
            case TutorialStateSelector.TutorialState.Intro:
                {
                    shipController.transform.position = Vector3.zero;
                    sailController.mainSailMoveSpeed = 0; //disable main sail moving
                    shipMainSailForce.mainSailDragStrength = 0; //disable sail drag
                    shipMainSailForce.mainSailLiftStrength = 0; //disable sail lift
                    shipWindForce.windOnHullStrength = 0; //disable wind hull force
                    tutorialIntroPanel.gameObject.SetActive(true);
                    tutorialOutroPanel.gameObject.SetActive(false);
                    break;
                }
            case TutorialStateSelector.TutorialState.Playing:
                {
                    shipWindForce.windOnHullStrength = 0.6f; //enable strong wind on hull force
                    tutorialIntroPanel.gameObject.SetActive(false);
                    tutorialOutroPanel.gameObject.SetActive(false);
                    break;
                }
            case TutorialStateSelector.TutorialState.Outro:
                {
                    shipWindForce.windOnHullStrength = 0;
                    tutorialIntroPanel.gameObject.SetActive(false);
                    tutorialOutroPanel.gameObject.SetActive(true);
                    break;
                }
            case TutorialStateSelector.TutorialState.End:
                {
                    // Here load the next tutorial scene
                    Application.Quit();
                    tutorialStateSelector.currentState = TutorialStateSelector.TutorialState.Intro;

                    break;
                }  
        }
	
	}
}
