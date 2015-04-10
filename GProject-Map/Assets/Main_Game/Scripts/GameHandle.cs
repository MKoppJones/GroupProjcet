using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandle : MonoBehaviour {
	
	int virusTier = 1;

	public GameObject[] EasyViruses;
	public GameObject[] MediumViruses;
	public GameObject[] HardViruses;
	public GameObject Bit;
	

	private List<GameObject> SpawnPoints;
	private int SpawnCount = 0;
	private int CurrentSpawn = 0;
	private float TimeBetweenSpawns = 5f;
	private bool isInit = false;
	private bool spawnVirus = false;

	// Use this for initialization
	public void Initialize (List<GameObject> Spawns) 
	{
		SpawnPoints = Spawns;
		SpawnCount = SpawnPoints.Count;
		isInit = true;
		spawnVirus = true;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isInit) {
			StartCoroutine("Spawn");

		}
		if (spawnVirus) {
			StartCoroutine("SpawnVirus");
		}
	}

	IEnumerator Spawn()
	{
		isInit = false;
		SpawnBits ();
		yield return new WaitForSeconds (TimeBetweenSpawns);

		if (CurrentSpawn == SpawnCount - 1)
			CurrentSpawn = 0;
		else
			CurrentSpawn += 1;
		isInit = true;

	}

	void SpawnBits()
	{
		SpawnScript sHandle = SpawnPoints [CurrentSpawn].GetComponent(typeof(SpawnScript)) as SpawnScript;
		sHandle.Invoke (Bit, 3);
	}

	IEnumerator SpawnVirus()
	{
		spawnVirus = false;

		float nextVirus = Random.Range (5, 10);
		int spawnPoint = (int)Random.Range (0, SpawnCount - 1);

		SpawnScript sHandle = SpawnPoints [spawnPoint].GetComponent(typeof(SpawnScript)) as SpawnScript;
		sHandle.InvokeVirus (EasyViruses[0], 1);
		yield return new WaitForSeconds (nextVirus);

		spawnVirus = true;
	}
}
