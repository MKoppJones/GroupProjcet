using UnityEngine;
using System.Collections;

public class OnSpawn : MonoBehaviour {

	public float moveSpeed = 3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {	
		float speed  = moveSpeed * Time.deltaTime;		

		if (Input.GetButton ("Forward")) {
			Debug.Log("Move player forward");
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
		}
		else if (Input.GetButton ("Backward")) {
			Debug.Log("Move player backward");
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
		}
		if (Input.GetButton ("Left")) {
			Debug.Log("Move player left");
			transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
		}
		else if (Input.GetButton ("Right")) {
			Debug.Log("Move player right");
			transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
		}
	}
}
