using UnityEngine;
using System.Collections;

public class PlayerWindParticle : MonoBehaviour {

    private WindController wind;
    public Rigidbody playerShip; 


    public Vector3 debugOutShipVel;
    public Vector3 debugOutParticleVec;


    void Start()
    {
        this.wind = GameObject.FindGameObjectWithTag("Wind").GetComponent<WindController>();
        this.particleSystem.simulationSpace = ParticleSystemSimulationSpace.World;
        this.particleSystem.startSpeed = wind.Wind().magnitude;
    }
	
	// Update is called once per frame
	void Update () {

        // the particles seen from the boat is WIND-BOAT.VEL
        Vector3 particlesVec = wind.Wind() - playerShip.velocity;



        //this.particleSystem.startSpeed = particlesVec.magnitude;
        this.transform.rotation = Quaternion.FromToRotation(Vector3.forward, wind.Wind());


        debugOutShipVel = playerShip.velocity;
        debugOutParticleVec = particlesVec;
	}
}
