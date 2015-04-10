using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoreScript : MonoBehaviour {

	public int money = 0;
	public int health = 100;
	public GameObject coreText;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter (Collision col)
	{
		coreText = GameObject.Find ("CoreText");

		if(col.gameObject.tag == "Bit")
		{
			Destroy(col.gameObject);

			money += 5;
		}

		if (col.gameObject.tag == "Virus") 
		{
			Destroy(col.gameObject);
			health -= 5;
			coreText.GetComponent<Text>().text = "Core Health: " + health.ToString();

			if(health == 0)
			{
				Destroy (gameObject);
			}
		}
	}
}
