/* This class is used for the handling of the game environment in terms of spawning. This class could further
 * be extended to set the lose conditions of the game (e.g. by getting the Core object's script and checking the health)
 * You can dictate in this class whether we make the game wave-based or endless, making adjustments to spawn timers
 * as it progresses.
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandle : MonoBehaviour {
	
	int virusTier = 1;

    //To be fully implemented. Would allow us to spawn different viruses at different times.
	public GameObject[] EasyViruses;
	public GameObject[] MediumViruses;
	public GameObject[] HardViruses;

    //The income Bit
	public GameObject Bit;
	
    //List of all spawn points so we know where we can Instantiate objects
	private List<GameObject> SpawnPoints;
    //Total amount of Spawn Points
	private int SpawnCount = 0;
    //Currently reference Spawn point
	private int CurrentSpawn = 0;
    //Default time bewteen spawning more Bits
	private float TimeBetweenSpawns = 5f;

    //Bools to handle Bit or Virus spawning
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
        //Handling of when to spawn viruses or bits
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
			CurrentSpawn += 1; //Increments the spawn point in use
		isInit = true;

	}

	void SpawnBits()
	{
        //Gets the Spawn point, and invokes its creation routine, under the standard of Invoke(Bit Object, Trojan Object, Amount to Spawn);
		SpawnScript sHandle = SpawnPoints [CurrentSpawn].GetComponent(typeof(SpawnScript)) as SpawnScript;
		sHandle.Invoke (Bit, EasyViruses[1], 3);
	}

	IEnumerator SpawnVirus()
	{
		spawnVirus = false;

        //Time to wait until the spawning of the next virus
		float nextVirus = Random.Range (5, 10);
        //Random spawn points to cast the virus from
		int spawnPoint = (int)Random.Range (0, SpawnCount - 1);

        //Invoke the creation of the virus
		SpawnScript sHandle = SpawnPoints [spawnPoint].GetComponent(typeof(SpawnScript)) as SpawnScript;
		sHandle.InvokeVirus (EasyViruses[0], 1);
		yield return new WaitForSeconds (nextVirus);

		spawnVirus = true;
	}
}
