using UnityEngine;
using System.Collections;

public class UICamera : MonoBehaviour {

	float coreLocZ = 0f;
	float coreLocX = 0f;

	// Use this for initialization
	void Start () {
		print (coreLocX.ToString ());
	}
	
	// Update is called once per frame
	void Update () {
		coreLocX = GameObject.Find ("MapLoader").GetComponent<MapLoad> ().coreX;
		coreLocZ = GameObject.Find ("MapLoader").GetComponent<MapLoad> ().coreZ;

		transform.position =  new Vector3(coreLocX, 15f, coreLocZ);
	}
}
