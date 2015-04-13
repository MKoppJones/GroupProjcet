using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour {

	bool spawnObjects = false;
	bool spawnVirus = false;
    GameObject trojanObject;
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
		else if (spawnVirus) {
			StartCoroutine ("SpawnVirus");
		}
	}

	public void Invoke(GameObject t, GameObject trojan, int count) 
	{

		spawnCount = count; targetObject = t;
        trojanObject = trojan;
		spawnObjects = true;


	}

	public void InvokeVirus(GameObject t, int count) 
	{
		
		spawnCount = count; targetObject = t;
		
		spawnVirus = true;
		
		
	}

	IEnumerator SpawnVirus()
	{
		spawnVirus = false;
		for (int i = 0; i < spawnCount; i++) {
			GameObject entityClone = (GameObject)Instantiate (targetObject, transform.position, Quaternion.identity);
			yield return new WaitForSeconds (.3f);
		}
	}

	IEnumerator Spawn()
	{
		spawnObjects = false;

        System.Random rnd = new System.Random ();

		for (int i = 0; i < spawnCount; i++) 
        {

            if(rnd.Next(0, 10) == 5)
            {
                GameObject trojanClone = (GameObject)Instantiate(trojanObject, transform.position, Quaternion.identity);
            }
            else 
            {
                GameObject entityClone = (GameObject)Instantiate (targetObject, transform.position, Quaternion.identity);
            }
			
			yield return new WaitForSeconds (.5f);
		}
	}
}