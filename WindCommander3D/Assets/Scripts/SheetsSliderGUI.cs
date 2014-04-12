using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UISlider))]
public class SheetsSliderGUI : MonoBehaviour {
    
    public SailRopeControllerThree sailController;
    
    public  UISlider ui;
    
    // Use this for initialization
    void Start () {
        this.ui.numberOfSteps = 100;
    }   
    
    // Update is called once per frame
    void Update () {
        ui.sliderValue = sailController.sheetLength/88;
    }
}
