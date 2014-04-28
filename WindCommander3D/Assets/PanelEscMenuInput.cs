using UnityEngine;
using System.Collections;

public class PanelEscMenuInput : MonoBehaviour
{

		public UIPanel panelEscMenu;

		// Use this for initialization
		void Start ()
		{
	
		}

		// Update is called once per frame
		void Update ()
		{	
				if (Input.GetKeyDown (KeyCode.Escape)) {
			panelEscMenu.gameObject.SetActive(!panelEscMenu.gameObject.activeSelf);	
						Time.timeScale = (Time.timeScale + 1) % 2;
				}
		
		}
}
