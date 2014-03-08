using UnityEngine;
using System.Collections;

public class WCMainMenuButton : MonoBehaviour {

    private UISprite sprite;
    private UILabel label;
    public Camera uiCamera;
    

	// Use this for initialization
	void Start () {

        this.sprite = gameObject.GetComponentInChildren<UISprite>();
        this.label = gameObject.GetComponentInChildren<UILabel>();

        this.sprite.enabled = false; //siable background sprite image on button
        this.label.color = Color.white;

	}

    void OnPress(bool isPressed)
    {
        if (isPressed)
            this.label.color = Color.gray;
        else
            this.label.color = Color.white;
    }

 
}
