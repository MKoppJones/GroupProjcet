using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MenuScript : MonoBehaviour {

	List<Vector2> resolutionList = new List<Vector2>();
	int currentResolution = 0;
	
	public AudioSource gameMusic;
	
	//Main menu UI
	public GameObject Main;
	
	//Options menu UI
	public GameObject Options;
	
	public Text resolutionText;
	public Text musicVolumeText;
	public Toggle fullscreenToggle;
	public Slider musicVolumeSlider;
	
	public bool full = false;
	
	public bool menu = true;
	
	// Use this for initialization
	void Start () {
	
	// Initialise resolution list
		resolutionList.Add (new Vector2(640,480));
		resolutionList.Add (new Vector2(1280,720));
		resolutionList.Add (new Vector2(1366,768));
		resolutionList.Add (new Vector2(1920,1080));
		
	// Start default options
		full = false;
		fullscreenToggle.isOn = false;
		Draw ();
		Set ();
	}
	
	public void StartGame()
	{
		Application.LoadLevel(1);
	}
	
	public void OptionsMenu()
	{
		menu = !menu;
		Options.SetActive(menu);
		Main.SetActive(!menu);
	}
	
	public void QuitGame()
	{
		Application.Quit();
	}
	
	//Option menu methods
	public void Windowed()
	{
		full =! full;
	}
	
	public void Volume()
	{
		gameMusic.volume = musicVolumeSlider.value;
		musicVolumeText.text = Convert.ToString (Math.Truncate(gameMusic.volume * 100));
	}
	
	public void Up()
	{
		currentResolution++;
		if (currentResolution > resolutionList.Count-1)
			currentResolution = 0;
			
		Draw ();
	}

	public void Down()
	{
		currentResolution--;
		if (currentResolution < 0)
			currentResolution = resolutionList.Count-1;
		
		Draw ();
	}

	void Draw()
	{
		resolutionText.text = resolutionList[currentResolution].x + " x " + resolutionList[currentResolution].y;
	}
	
	public void Set()
	{
		Screen.SetResolution(Convert.ToInt32(resolutionList[currentResolution].x), Convert.ToInt32(resolutionList[currentResolution].y), full);
	}
}
