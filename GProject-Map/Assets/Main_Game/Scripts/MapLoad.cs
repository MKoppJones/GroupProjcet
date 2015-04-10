using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class MapLoad : MonoBehaviour {

	//Variables for loading of the map
	public GameObject wall;
	public GameObject core;
	public GameObject spawn;
	public GameObject bwall;
	public GameObject floor;
	public GameObject player;
	public GameObject camera;

	public float coreX = 0f;
	public float coreZ = 0f;
	
	private StreamReader theReader = new StreamReader("map.txt", Encoding.Default);

	//private string[] mapTest = {"11211", "10001", "10001", "10001", "11111"};
	private string[] mapString;
	private float startX = 0.0f;
	private float startZ = 0.0f;
	private List<GameObject> Spawns = new List<GameObject>();

	protected static MapLoad instance;

	//Variables for map grid referencing

	public int[,] gridCosts;
	
	// Use this for initialization
	void Start () 
	{

		instance = this;

		GetMap ();
		InitializeObjects ();
		InitializeGrid ();

		GameHandle gHandle = this.GetComponent(typeof(GameHandle)) as GameHandle;
		gHandle.Initialize (Spawns);

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void GetMap()
	{
		using (theReader) {
		
			mapString = theReader.ReadToEnd ().Split ('\n');
			theReader.Close ();

		}
	}

	void InitializeObjects()
	{
		for (int i = 0; i < mapString.Length; i++) {
			mapString[i] = mapString[i].Replace ("\r", string.Empty);
			
			foreach (char oState in mapString[i]) {
				switch(oState)
				{
				case '1':
					GameObject sWall = (GameObject)Instantiate(wall, new Vector3(startX,0.5f,startZ), Quaternion.identity);
					sWall.transform.parent = transform;
					break;

				case '2':
					GameObject sClone = (GameObject)Instantiate(spawn, new Vector3(startX,0.5f,startZ), Quaternion.identity);
					Spawns.Add (sClone);
					sClone.transform.parent = transform;
					break;

				case '3':
					Instantiate(bwall, new Vector3(startX,0.5f,startZ), Quaternion.identity);
					break;

				case '4':
					GameObject bClone = (GameObject)Instantiate(floor, new Vector3(startX,0f,startZ), Quaternion.identity);
					bClone.transform.Rotate (Vector3.right * 90);
					bClone.transform.parent = transform;

					GameObject P = (GameObject)Instantiate (player, new Vector3 (startX,1f,startZ), Quaternion.identity);
					
					GameObject C = (GameObject)Instantiate (camera, new Vector3 (startX,2f,startZ), Quaternion.identity);
					MCamera CClass = C.GetComponent (typeof(MCamera)) as MCamera;
					CClass.Initialize (P.transform);
					break;

				case '5':
					GameObject aClone = (GameObject)Instantiate(floor, new Vector3(startX,0f,startZ), Quaternion.identity);
					aClone.transform.Rotate (Vector3.right * 90);

					Instantiate(core, new Vector3(startX,0.5f,startZ), Quaternion.identity);
					break;

				case '0':
					GameObject fClone = (GameObject)Instantiate(floor, new Vector3(startX,0f,startZ), Quaternion.identity);
					fClone.transform.Rotate (Vector3.right * 90);
					fClone.transform.parent = transform;
					break;

				default:
					break;
				}
				startX += 1.0f;
			}
			startX = 0.0f;
			startZ += 1.0f;
		}
	}

	void InitializeGrid(/*Vector3 target, string[] mapGrid*/)
	{
		gridCosts = new int[mapString.Length, mapString[0].Length];

		//Get the position of the core for the grid.
		for (int i = 0; i < mapString.Length; i++) 
		{
			for (int j = 0; j < mapString[i].Length; j++) 
			{
				if(mapString[i][j] == '5')
				{
					gridCosts[i, j] = 1;
					coreX = (float)j; coreZ = (float)i;
				}

				if(mapString[i][j] == '1' || mapString[i][j] == '2' || mapString[i][j] == '3') gridCosts[i, j] = 1000;

			}
		}
		
		SetGridCosts ();
		Debug.Log ("Initialize complete!");
		
	}
	
	void SetGridCosts()
	{
		bool finished = false;
		int stage = 1, found = 0, timeoutCount = 0;
		
		while (!finished) {
			for (int i = 0; i < gridCosts.GetLength (0); i++) {
				for (int j = 0; j < gridCosts.GetLength (1); j++) {
					
					if(gridCosts[i, j] == stage)
					{
						//Need to check the positions above, to the right, to the left, and below the current one
						
						//To the left
						if(j != 0) if(gridCosts[i,j-1] == 0)
						{
							found++;
							gridCosts[i, j-1] = stage+1;
						}
						
						//To the right
						if(j != gridCosts.GetLength (1)-1) if(gridCosts[i,j+1] == 0)
						{
							found++;
							gridCosts[i, j+1] = stage+1;
						}
						
						//Up above
						if(i != 0) if(gridCosts[i-1, j] == 0)
						{
							found++;
							gridCosts[i-1, j] = stage+1;
						}
						
						//Down below
						if(i != gridCosts.GetLength (0)-1) if(gridCosts[i+1, j] == 0)
						{
							found++;
							gridCosts[i+1, j] = stage+1;
						}
					}
				}
			}
			if(timeoutCount == 4)
			{
				finished = true;
			}
			
			if(found == 0)
			{
				timeoutCount++;
				stage++;
			}
			else
			{
				timeoutCount = 0;
			}
			
			found = 0;
		}
	}
	
	void MapGridDebug()
	{
		string debugString = "";
		
		for (int i = 0; i < gridCosts.GetLength (0); i++) {
			for (int j = 0; j < gridCosts.GetLength (1); j++) {
				
				debugString += gridCosts[i, j].ToString();
				
			}
			
			debugString += "\n";
		}
		
		Debug.Log (debugString);
	}
	
}
