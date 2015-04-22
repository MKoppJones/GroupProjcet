using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameScript : MonoBehaviour {

    GameObject g;

    public Text t;
	// Use this for initialization
	void Start () {
        g = GameObject.FindGameObjectWithTag("Player");
        t.text = "Your score was " + g.GetComponent<PlayerScript>().score;
        Destroy(g.GetComponent<MeshRenderer>());
	}
	
    public void Menu()
    {
        Application.LoadLevel(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

	// Update is called once per frame
	void Update () {
	
	}
}
