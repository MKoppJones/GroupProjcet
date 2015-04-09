using UnityEngine;
using System.Collections;

public class MCamera : MonoBehaviour {
	
	public Transform target;
	public float smooth= 5.0f;
	
	void Start () 
	{
		//Do something here
	}

	public void Initialize(Transform t)
	{
		target = t;
		//transform.RotateAround(transform.position, transform.right, 65f);
		transform.rotation *= Quaternion.Euler(65, 0, 0);

	}
	
	void Update () 
	{
		transform.position = Vector3.Lerp (new Vector3(transform.position.x, transform.position.y, transform.position.z), 
		                                   new Vector3(target.position.x, target.position.y+4f, target.position.z-2f), 
		            					   Time.deltaTime * smooth);
	}
}
