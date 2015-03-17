using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class Scanner : MonoBehaviour {
	[System.Serializable]
	public class Freeware
	{
		public string name = "Freeware";
		public float speedReduction = 0;
		public float cost = 0.0f;

		public float Activate(float startSpeed)
		{
			return startSpeed - speedReduction;
		}

		public void Tick()
		{
			Debug.Log ("Freeware Tick");
		}

		public float Deactivate(float startSpeed)
		{
			return startSpeed + speedReduction;
		}
	}
	
	[System.Serializable]
	public class Paid
	{
		public string name = "Paid";
		public float detectionRate = 0.75f;
		public float cost = 0.0f;
		
		public void Activate()
		{
		}
		
		public void Tick()
		{
			Debug.Log ("Paid Tick");
		}
		
		public void Deactivate()
		{
		}
	}
	
	[System.Serializable]
	public class Commercial
	{
		public string name = "Commercial";
		public float scanTime = 1.0f;
		public float cost = 0.0f;

		public void Activate()
		{
		}
		
		public void Tick()
		{
			Debug.Log ("Commercial Tick");
		}
		
		public void Deactivate()
		{
		}
	}

	public Freeware freeware = new Freeware(); 		//0
	public Paid paid = new Paid();					//1
	public Commercial commercial = new Commercial();//2

	public int currentScanner = 0;

	public float playerSpeed = 15.0f;

	// Use this for initialization
	void Start () {
		Activate (currentScanner);
	}

	void ChangeScanner(int index)
	{
		Deactivate(currentScanner);
		
		currentScanner = index;
		Activate (currentScanner);
	}

	void Activate(int index)
	{
		switch (index) {
		case 0:
			playerSpeed = freeware.Activate(playerSpeed);
			break;
		case 1:
			paid.Activate ();
			break;
		case 2:
			commercial.Activate ();
			break;
		}
	}

	void Tick(int index)
	{
		switch (index) {
		case 0:
			freeware.Tick ();
			break;
		case 1:
			paid.Tick ();
			break;
		case 2:
			commercial.Tick ();
			break;
		}
	}

	void Deactivate(int index)
	{
		switch (index) {
		case 0:
			playerSpeed = freeware.Deactivate(playerSpeed);
			break;
		case 1:
			paid.Deactivate ();
			break;
		case 2:
			commercial.Deactivate ();
			break;
		}
	}

	// Update is called once per frame
	void Update () {
		int i = 0;
		if (Input.GetKeyDown (KeyCode.Alpha1))
			i = 0;
		else
			if (Input.GetKeyDown (KeyCode.Alpha2))
			i = 1;
		else
			if (Input.GetKeyDown (KeyCode.Alpha3))
			i = 2;
		else
			i = -1;

		switch (i) {
		case 0:
			ChangeScanner(i);
			break;
		case 1:
			ChangeScanner(i);
			break;
		case 2:
			ChangeScanner(i);
			break;
		}

		Tick (currentScanner);
	}
}
