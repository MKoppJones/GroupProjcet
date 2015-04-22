using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	public float moveSpeed = 3f;
	public float turningSpeed = 5f;
	public float attackSpeed = 2f;
	public float moveMultiplier = 1f;
	public float attackMultiplier = 1f;
    public int score = 0;
    public int points = 0;
    public int destroyed = 0;

	// Use this for initialization
	void Start () {
		//Physics.IgnoreLayerCollision(0, 8, true);

	}
	
	// Update is called once per frame
	void Update () {

		//Multiplier handling
		float speed  = (moveSpeed * moveMultiplier) * Time.deltaTime;	
		float turnSpeed = (turningSpeed * moveMultiplier) * Time.deltaTime;

		//Movement
		if (Input.GetButton ("Forward")) {
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + speed);
		}
		else if (Input.GetButton ("Backward")) {
			transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - speed);
		}
		if (Input.GetButton ("Left")) {
			transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
		}
		else if (Input.GetButton ("Right")) {
			transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
		}

		//Rotation
		Plane playerPlane = new Plane(Vector3.up, transform.position);

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		float hitdist = 0f;

		if (playerPlane.Raycast (ray, out hitdist)) {

			Vector3 targetPoint = ray.GetPoint(hitdist);
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
		}

        UpdateUI();
	}

    void UpdateUI()
    {
        string scannerActive = "Offline";
        if (GameObject.FindWithTag("Scanner").GetComponent<ScannerScript>().isActive)
            scannerActive = "Online";
        GameObject.Find("StatusText").GetComponent<Text>().text =
            "UP\t\t:\t" + points.ToString() + "\nScore\t:\t" + points.ToString() + "\nTime\t:\t" + Time.time.ToString("F1") + "\nScan\t:\t" + scannerActive;

    }
}
