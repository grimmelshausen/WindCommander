using UnityEngine;
using System.Collections;

/************************************************************************/
/* This is the main tutorial handler controller class that organizes
 * the logic of the tutorial. Basically it uses the TutorialState to
 * determine what to do.
/************************************************************************/
public class Tutorial1Sail: MonoBehaviour
{
		//tutorial state
		public TutorialStateSelector tutorialStateSelector;

		// ship controllers
		public ShipController shipController;
		public ShipRudderForce shipRudderForce;
		public ShipWindForce shipWindForce;
		public WindController wind;
		public ShipMainSailForce shipMainSailForce;
		public SailController sailController;

		// gui panels
		public UIPanel tutorialIntroPanel;
		public UIPanel tutorialOutroPanel;
		public UIPanel tutorialIntermezzo1Panel;
		public UIPanel panelEscMenu;

		// tutorial targets
		public TutorialTargetPointController target1;
		public TutorialTargetPointController target2;

		//mist
		private float sailControllerSpeedSave;

		// Use this for initialization
		void Start ()
		{
				tutorialStateSelector.currentState = TutorialStateSelector.TutorialState.Intro;
				sailControllerSpeedSave = sailController.mainSailMoveSpeed;
				Time.timeScale = 1;
		}

		// Update is called once per frame
		void Update ()
		{

				//Esc menu
				if (Input.GetKeyDown (KeyCode.Escape)) {
						panelEscMenu.gameObject.SetActive (!panelEscMenu.gameObject.activeSelf);
			Time.timeScale = (Time.timeScale+1) % 2;
		
				}



				switch (tutorialStateSelector.currentState) {
				case TutorialStateSelector.TutorialState.Intro:
					
						shipController.transform.position = Vector3.zero;
						sailController.mainSailMoveSpeed = 0; //disable main sail moving
						shipMainSailForce.mainSailDragStrength = 0; //disable sail drag
						shipMainSailForce.mainSailLiftStrength = 0; //disable sail lift
						shipWindForce.windOnHullStrength = 0; //disable wind hull force
						wind.rotateSpeed = 4;
						wind.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 0));

						tutorialIntroPanel.gameObject.SetActive (true);
						tutorialIntermezzo1Panel.gameObject.SetActive (false);
						tutorialOutroPanel.gameObject.SetActive (false);

						target1.gameObject.SetActive (true);
						target2.gameObject.SetActive (false);
						break;
					
				case TutorialStateSelector.TutorialState.Stage1:
					
						shipWindForce.windOnHullStrength = 0.02f; //enable hull force
						shipMainSailForce.mainSailDragStrength = 0.5f; // The ship should now move
						sailController.mainSailMoveSpeed = 0.5f; //main sail can now move


						tutorialIntroPanel.gameObject.SetActive (false);
						tutorialIntermezzo1Panel.gameObject.SetActive (false);
						tutorialOutroPanel.gameObject.SetActive (false);

						target1.gameObject.SetActive (true);
						target2.gameObject.SetActive (false);
						break;
					
				case TutorialStateSelector.TutorialState.Intermezzo1:
						
						

						tutorialIntroPanel.gameObject.SetActive (false);
						tutorialIntermezzo1Panel.gameObject.SetActive (true);					
						tutorialOutroPanel.gameObject.SetActive (false);

						target1.gameObject.SetActive (false);
						target2.gameObject.SetActive (true);

						break;
						
				case TutorialStateSelector.TutorialState.Stage2:
						
						shipMainSailForce.mainSailLiftStrength = 0.5f; //enable sail
						shipMainSailForce.mainSailDragStrength = 0.2f;
//						shipWindForce.windOnHullStrength = 0.0f; //enable hull force
//						shipController.transform.position = new Vector3(40, 0, 100);
//						shipController.transform.rotation = Quaternion.Euler (new Vector3 (0, 180, 0));

						tutorialIntroPanel.gameObject.SetActive (false);
						tutorialIntermezzo1Panel.gameObject.SetActive (false);					
						tutorialOutroPanel.gameObject.SetActive (false);

						target1.gameObject.SetActive (false);
						target2.gameObject.SetActive (true);
						break;
						
				case TutorialStateSelector.TutorialState.Outro:
						
						shipWindForce.windOnHullStrength = 0;

						tutorialIntroPanel.gameObject.SetActive (false);
						tutorialIntermezzo1Panel.gameObject.SetActive (false);		
						tutorialOutroPanel.gameObject.SetActive (true);

						target1.gameObject.SetActive (false);
						target2.gameObject.SetActive (false);
						break;
						
				case TutorialStateSelector.TutorialState.End:
						
								// Here load the next tutorial scene
						Application.Quit ();
						tutorialStateSelector.currentState = TutorialStateSelector.TutorialState.Intro;

						break;
						
				}

		}
}
