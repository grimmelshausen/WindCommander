using UnityEngine;
using System.Collections;

public class SailRopeControllerThree : MonoBehaviour
{

    public WindController wind;
    public float angleSailWind;
    public float angleShipWind;
    public float angleSailShip;
    public bool isInIrons;
    public ShipController ship;
    private float inIronsTolerance = 10;
    public float inputRotSailDeg;
    public float windRotSailDeg;
    public float sheetLength; //the ropes

    public float targetSailRot;



    // Use this for initialization
    void Start()
    {
        sheetLength = 0;
    }

    
    // Update is called once per frame
    void Update()
    {

        /* This approach: per default the wind just quickly blows the sail into position. That is, it is always 
         * rotated so it points in the same direction as the wind. And then there is only one other contstrain. The sheets
         * (rope). It defines how far away (both sides) from the boat.back vector the sail can be. The user can then make it
         * longer or shorter.
         */

        float input = Input.GetAxis("SailRope") * 5; //input>0 means ease off sheets, input<0 means shorten sails

        sheetLength += input;

        sheetLength = Mathf.Clamp(sheetLength, 0, 88);


        // The wind relative to the ship's velocity
        Vector3 relWind = wind.Wind() - ship.rigidbody.velocity;

        Debug.DrawLine(ship.transform.position + new Vector3(0,  4.1f, 0), ship.transform.position + new Vector3(0, 4.1f, 0) + relWind, Color.red);
        
        
        /*
         * Calculate angles between wind and sail and boat
         */
        angleSailWind = Mathhelp.AngleY(this.transform.forward, relWind);
        angleShipWind = Mathhelp.AngleY(-ship.transform.forward, relWind);
        angleSailShip = Mathhelp.AngleY(-ship.transform.forward, this.transform.forward);
        
        //Check if we are in irons
        if (Mathf.Abs(angleSailWind) <= inIronsTolerance || Mathf.Abs(angleSailWind) >= 180 - inIronsTolerance)
        {
            isInIrons = true;
        } else
        {
            isInIrons = false;
        }
            
   
        /*
         * Blow the sail into the direction of the wind, with some tolerance
         * 
         */
        if (angleSailWind > 3)
        { //if wind is on right side of sail then blow it clockwise
            targetSailRot += 3;
        } else if (angleSailWind < -3)
        { //if wind is on left side then blow it counterclockwise
            targetSailRot -= 3;
        }


        /*
         * Now we want to apply the sheets constrain: meaning the sail can only be max sheetLength
         * degrees away from the ship. we need to do this independently for each side see image with A B C D
         */

        if (angleSailShip > 0) //sail is on right side of ship (seen from the back of the ship, so left side (port side))
        { 
            if (angleSailWind > 0)  // wind is right side of sail
            {
                // A
                if (angleSailShip > sheetLength)
                {
                    targetSailRot = sheetLength;
                }
            } else // wind is left side of sail
            {
                // B
            }
        } else //sail is on left side of ship (seen from the back, so right side (starboard)
        {
            if (angleSailWind > 0)  // wind is right side of sail
            {
                // D

            } else // wind is left side of sail
            {
                // C
                if (angleSailShip < -sheetLength)
                {
                    targetSailRot = -sheetLength;
                }
            }
        }
       
        this.transform.localRotation = Quaternion.Slerp(this.transform.localRotation, Quaternion.Euler(0, targetSailRot, 0), Time.deltaTime*2);

    }
}
