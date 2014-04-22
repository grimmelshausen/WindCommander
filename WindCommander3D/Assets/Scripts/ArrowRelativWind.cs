    using UnityEngine;
using System.Collections;

public class ArrowRelativWind : TogglableHelpGUI {

    public Vector3 offset;
    public Transform ship;
    private WindController wind;
    
    
    float normalScale = 20;
    
    // Use this for initialization
    void Start () {
        this.wind = GameObject.FindGameObjectWithTag("Wind").GetComponent<WindController>();
    }
    
    // Update is called once per frame
    void Update () {
        Vector3 to = wind.Wind() - ship.rigidbody.velocity;
        this.transform.position = ship.transform.position + offset;
        this.transform.rotation = Quaternion.FromToRotation(Vector3.forward, to);  
        this.transform.Rotate(Vector3.up, 180); // Yup. Rotate 180 deg because the arrow looks backwards
        this.transform.localScale = new Vector3(normalScale, normalScale, normalScale * (to).magnitude / 4);

    }
}
