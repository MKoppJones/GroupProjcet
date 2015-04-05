using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	bool spawnObjects = false;
	int spawnCount = 0;

	GameObject targetObject;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (spawnObjects) {
			StartCoroutine("Spawn");
		}
	}

	public void Invoke(GameObject t, int count) 
	{

		spawnCount = count; targetObject = t;

		spawnObjects = true;


	}

	IEnumerator Spawn()
	{
		spawnObjects = false;
		for (int i = 0; i < spawnCount; i++) {
			GameObject entityClone = (GameObject)Instantiate (targetObject, transform.position, Quaternion.identity);
			yield return new WaitForSeconds (.3f);
		}
	}
}