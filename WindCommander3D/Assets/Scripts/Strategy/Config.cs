using UnityEngine;
using System.Collections.Generic;

[AddComponentMenu("Strategy/Config")]
public class Config : MonoBehaviour
{
	public static Config Instance = null;

	public int			gold				= 10000;
	public bool			showWelcome			= true;
	public GUISkin		skin				= null;
	public Font 		townNameFont 		= null;
	public Texture2D	townNameBackground 	= null;
	public Texture2D	windowBackground	= null;
	public Texture2D	windowBorder		= null;
	public int			windowPadding		= 7;

	public Color		affordableText		= new Color(32.0f / 255.0f, 121.0f / 255.0f, 32.0f / 255.0f, 1.0f);
	public Color		notAffordableText	= new Color(168.0f / 255.0f, 43.0f / 255.0f, 0.0f, 1.0f);

	public GUIStyle 	headerStyle 		= null;
	public GUIStyle 	descStyle			= null;
	public GUIStyle 	infoStyle			= null;
	public GameObject	playerShip;

	public delegate void OnGUICallback ();

	public List<OnGUICallback> onGUI = new List<OnGUICallback>();
	public List<OnGUICallback> onLateGUI = new List<OnGUICallback>();

	void OnEnable ()
	{
		Instance = this;
		playerShip.SetActiveRecursively(true);
	}

	void OnDisable ()
	{
		if (Instance == this) Instance = null;
	}

	void Update ()
	{
		if (playerShip != null && Input.GetKeyDown(KeyCode.F5))
		{
			GameCamera.DetachFromParent(playerShip.transform);
			playerShip.SetActiveRecursively(!playerShip.active);
		}
	}

	void OnGUI ()
	{
		if (Instance == null) return;

		foreach (OnGUICallback callback in onGUI) { callback(); }
		foreach (OnGUICallback callback in onLateGUI) { callback(); }


		// DisplayWelcome();
	}



	void DisplayWelcome ()
	{

		Rect rect = UI.DrawWindow(new Rect(Screen.width * 0.5f - 200f, Screen.height * 0.5f - 220f, 400f, 440f), "Welcome");

		GUILayout.BeginArea(rect);
		{
			GUILayout.Space(10f);

			if (Application.isEditor)
			{
				GUILayout.Label("Thank you for buying the Ship Game Starter Kit!\n\n" +
					"We hope it will help you with your own game ambitions. If you have any suggestions, comments, feature or even new starter kit requests, " +
					"please don't hesitate to contact us via support@tasharen.com.\n\nWe hope your game will be a stellar success!", skin.label);
			}
			else
			{
				GUILayout.Label("Welcome to the Ship Game Starter Kit!\n\n" +
					"You will want to start by selecting your first town and creating a trade route that links it to another town (you can read the instructions below).\n\n" +
					"At any time press F5 to switch between Strategy and Exploration modes.\n\n" +
					"May this rough prototype help you with your own game ambitions!", skin.label);
			}

			if (GUI.Button(new Rect(100f, rect.height - 80f, 200f, 30f), "Try Strategy Mode", skin.button))
			{
				playerShip.SetActiveRecursively(false);
				showWelcome = false;
			}

			if (GUI.Button(new Rect(100f, rect.height - 40f, 200f, 30f), "Try Exploration Mode", skin.button))
			{
				playerShip.SetActiveRecursively(true);
				showWelcome = false;
			}
		}
		GUILayout.EndArea();
	}
}