using UnityEngine;
using System.Collections;

public class MapLoad : MonoBehaviour {

	public GameObject wall;
	public GameObject spawn;

	private string[] mapTest = {"11211", "10001", "10001", "10001", "11111"};
	private float startX = 0.0f;
	private float startZ = 0.0f;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < mapTest.Length; i++) {
			foreach (char oState in mapTest[i]) {
				switch(oState)
				{
				case '1':
					Instantiate(wall, new Vector3(startX,0.5f,startZ), new Quaternion(0f,0f,0f,0f));
					break;
				case '2':
					Instantiate(spawn, new Vector3(startX,0.5f,startZ), new Quaternion(0f,0f,0f,0f));
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
