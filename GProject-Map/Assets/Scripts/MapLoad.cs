using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;

public class MapLoad : MonoBehaviour {

	public GameObject wall;
	public GameObject spawn;
	public GameObject bwall;
	public GameObject floor;
	public GameObject player;
	public GameObject camera;

	private StreamReader theReader = new StreamReader("map.txt", Encoding.Default);

	//private string[] mapTest = {"11211", "10001", "10001", "10001", "11111"};
	private string[] mapString;
	private float startX = 0.0f;
	private float startZ = 0.0f;
	private float maxX = 0.0f;

	protected static MapLoad instance;
	
	// Use this for initialization
	void Start () 
	{
		instance = this;

		using (theReader) 
		{

			mapString = theReader.ReadToEnd ().Split ('\n');
			theReader.Close();

		}


		for (int i = 0; i < mapString.Length; i++) {

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
					GameObject fClone = (GameObject)Instantiate(floor, new Vector3(startX,0.5f,startZ), Quaternion.identity);
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

		GameObject P = (GameObject)Instantiate (player, new Vector3 (maxX / 2, 0.5f, startZ / 2), Quaternion.identity);
		Debug.Log ("*" + P.transform.position.ToString() + "*");

		GameObject C = (GameObject)Instantiate (camera, new Vector3 (maxX / 2, 2f, startZ-1 / 2), Quaternion.identity);
		MCamera CClass = C.GetComponent (typeof(MCamera)) as MCamera;
		CClass.Initialize (P.transform);

	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
