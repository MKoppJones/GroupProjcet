using UnityEngine;
using System.Collections;

public class EHealth : MonoBehaviour {

    public float health = 100;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (health <= 0)
        {
            PlayerScript pScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
            if(gameObject.tag == "Virus")
            {
                pScript.score += 5; pScript.points += 5; pScript.destroyed += 1;
            }
            else pScript.score -= 20;
            Destroy(gameObject);
        }
	}
}
