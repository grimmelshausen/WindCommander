using UnityEngine;
using System.Collections;
using System.IO;

public class SimulationLogger : MonoBehaviour {

	public WindController wind;
	public  Transform mainSailHinge;
	public  ShipMainSailForce mainSailForce;
	public Transform ship;
	public SailRopeController sailRopeController;

	float iterationInterval = 0.2f;

	StreamWriter sw;
	float lastWriteoutTime;
	int rotateStepCounter = 0;


	// Use this for initialization
	void Start () 
	{

		// itinialize values
		sailRopeController.targetRopeLengthPercent = 0;
		wind.transform.rotation = Quaternion.identity;
		lastWriteoutTime = 0;


		sw = new StreamWriter("simulationLogger.txt");
		sw.Write("ShipRotationDeg SailAngleDeg SailRopeLengthDeg DragForward LiftForward \n");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*
		 * THE IDEA IS! EHH! I HAVE AN IPAD! And then I iterate over every 
		 * main sail rotation angle (set the percent from 0 to 1) for every wind angle (in 360 deg)
		 * Wait iterationInterval seconds before writing down the numbers (because of physics)
		 */

		if (Time.time - lastWriteoutTime > iterationInterval)
		{
			//Update time
			lastWriteoutTime = Time.time;

			//Write down the numbers
			string s = 
				Quaternion.Angle(Quaternion.identity, this.ship.rotation).ToString() + " " +
					Quaternion.Angle(Quaternion.identity, this.mainSailHinge.transform.rotation).ToString() + " " +
					sailRopeController.targetRopeLength + " " +
					mainSailForce.dragForward.magnitude + " " +
					mainSailForce.liftForward.magnitude + "\n";			

			sw.Write(s);
			Debug.Log(s);

			//Change sail and or wind. Note: for this to work disable the slerp for nice 
			//animation in the sail rope controller, just let it flipp the fuck over
			sailRopeController.targetRopeLengthPercent += 0.05f;
			if (sailRopeController.targetRopeLengthPercent >= 1)
			{
				sailRopeController.targetRopeLengthPercent = 0;
				rotateStepCounter++;
				//wind.transform.Rotate(new Vector3(0,45,0)); //lets not rotate the wind
			}
		}
		//Let's just say we also make sure the ship does not move or turn while we do this stuff
		ship.position = Vector3.zero;
		//change of plans, we rotate the ship not the wind
		ship.rotation = Quaternion.AngleAxis(45.0f * rotateStepCounter, Vector3.up);

	}

	void Destroy()
	{
		sw.Close();
	}
}
