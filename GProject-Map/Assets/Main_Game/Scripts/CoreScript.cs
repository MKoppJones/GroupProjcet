using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoreScript : MonoBehaviour {

	public int money = 0;
	public int health = 100;
	public GameObject scannerObject;

    private GameObject coreText;

	// Use this for initialization
	void Start () {
		GameObject scanner = (GameObject)Instantiate (scannerObject, transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateUI ();
	}

	void UpdateUI () {

		//Update core health

		coreText = GameObject.Find ("CoreText");
		coreText.GetComponent<Text>().text = "Core Health: " + health.ToString();
	}

	void OnCollisionEnter (Collision col)
	{

		//Safe
		if(col.gameObject.tag == "Bit")
		{
			Destroy(col.gameObject);

            PlayerScript pScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
            pScript.score += 5; pScript.points += 5;
        }
        
		//Virus collision detection
		if (col.gameObject.tag == "Virus") 
		{
			Destroy(col.gameObject);
			health -= 5;


			if(health == 0)
			{
				//Set end game conditions here
				Destroy (gameObject);
			}
		}
	}
}
