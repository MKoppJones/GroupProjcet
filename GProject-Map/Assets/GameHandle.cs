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

	// Use this for initialization
	public void Initialize (List<GameObject> Spawns) 
	{
		SpawnPoints = Spawns;
		SpawnCount = SpawnPoints.Count;
		isInit = true;

	}
	
	// Update is called once per frame
	void Update () 
	{
		if (isInit) {
			StartCoroutine("Spawn");

		}
	}

	IEnumerator Spawn()
	{
		isInit = false;
		SpawnBits ();
		yield return new WaitForSeconds (5f);

		if (CurrentSpawn == SpawnCount - 1)
			CurrentSpawn = 0;
		else
			CurrentSpawn += 1;
		isInit = true;
	}

	void SpawnBits()
	{
		SpawnScript sHandle = SpawnPoints [CurrentSpawn].GetComponent(typeof(SpawnScript)) as SpawnScript;
		sHandle.Invoke (Bit, 5);
	}

	void SpawnVirus()
	{

	}
}
