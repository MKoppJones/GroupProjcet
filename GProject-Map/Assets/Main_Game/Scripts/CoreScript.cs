using UnityEngine;
using System.Collections;

public class CoreScript : MonoBehaviour {

	public int money = 0;
	public int health = 100;
	//public  HUD;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Bit")
		{
			Destroy(col.gameObject);
		}

		if (col.gameObject.tag == "Virus") 
		{
			Destroy(col.gameObject);
			health -= 5;
			//HUD.GetComponentInChildren<GUIText>().text = "Core Health: " + health.ToString();

			if(health == 0)
			{
				Destroy (gameObject);
			}
		}
	}
}
