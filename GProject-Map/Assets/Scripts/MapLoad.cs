using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class MapLoad : MonoBehaviour {

	//Variables for loading of the map
	public GameObject wall;
	public GameObject node;
	public GameObject spawn;
	public GameObject bwall;
	public GameObject floor;
	public GameObject player;
	public GameObject camera;
	public GameObject testentity;

	private StreamReader theReader = new StreamReader("map.txt", Encoding.Default);

	//private string[] mapTest = {"11211", "10001", "10001", "10001", "11111"};
	private string[] mapString;
	private float startX = 0.0f;
	private float startZ = 0.0f;
	private float maxX = 0.0f;

	protected static MapLoad instance;

	//Variables for map grid referencing

	public int[,] gridCosts;
	private int cArrayC = 0, cArrayR = 0; //Position of the core in the map string array
	private static string[] map = {
		"11111111111111111",
		"10000000000000001",
		"10111111011111101",
		"10100000000000101",
		"10101111311110101",
		"10101000000010101",
		"10101010101010101",
		"10101010001010101",
		"10101010501010101",
		"10101010001010101",
		"10101011311010101",
		"10101000000010101",
		"10101111011110101",
		"10100000000000101",
		"10111111311111101",
		"10000000000000001",
		"11111211111211111"
	};
	
	// Use this for initialization
	void Start () 
	{

		instance = this;

		GetMap ();
		InitializeObjects ();
		InitializeGrid ();

		Instantiate(testentity, new Vector3(1f,1f,1f), Quaternion.identity);
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
			mapString[i].Trim ();
			
			foreach (char oState in mapString[i]) {
				switch(oState)
				{
				case '1':
					Instantiate(wall, new Vector3(startX,0.5f,startZ), Quaternion.identity);
					break;
				case '2':
					Instantiate(spawn, new Vector3(startX,0.5f,startZ), Quaternion.identity);
					break;
				case '3':
					Instantiate(bwall, new Vector3(startX,0.5f,startZ), Quaternion.identity);
					break;
				case '0':
					GameObject fClone = (GameObject)Instantiate(floor, new Vector3(startX,0f,startZ), Quaternion.identity);
					fClone.transform.Rotate (Vector3.right * 90);
					break;
				default:
					break;
				}
				startX += 1.0f;
			}
			if(startX != 0.0f) maxX = startX;
			startX = 0.0f;
			startZ += 1.0f;
		}
		
		GameObject P = (GameObject)Instantiate (player, new Vector3 (maxX / 2.0f - 0.5f, 0.5f, startZ / 2.0f - 0.5f), Quaternion.identity);
		Debug.Log ("*" + P.transform.position.ToString() + "*");
		
		GameObject C = (GameObject)Instantiate (camera, new Vector3 (maxX / 2.0f - 0.5f, 2f, startZ / 2.0f - 0.5f), Quaternion.identity);
		MCamera CClass = C.GetComponent (typeof(MCamera)) as MCamera;
		CClass.Initialize (P.transform);
	}

	void InitializeGrid(/*Vector3 target, string[] mapGrid*/)
	{
		gridCosts = new int[map.Length, map[0].Length];

		//Get the position of the core for the grid.
		for (int i = 0; i < map.Length; i++) 
		{
			for (int j = 0; j < map[i].Length; j++) 
			{
				if(map[i][j] == '5')
				{
					gridCosts[i, j] = 1;
					cArrayC = j; cArrayR = i;
				}

				if(map[i][j] == '1' || map[i][j] == '2' || map[i][j] == '3') gridCosts[i, j] = 1000;

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
