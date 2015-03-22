using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Scanner : MonoBehaviour {
	[System.Serializable]
	public class Freeware
	{
        public string name = "Freeware";
        public int cost = 0;
		public float pingTime = 8.0f;
		public bool isScanning = true;
		public float scanRadius = 25;
		public float timer = 0.0f;

        public void Activate(ref float startSpeed, ref float attackSpeed, ref int playerMoney)
		{
			attackSpeed /= 2;
			startSpeed /= 2;
            playerMoney -= cost;
		}

		public void Scan(GameObject g)
		{
			g.GetComponent<SphereCollider> ().radius += 0.05f;
			if (g.GetComponent<SphereCollider> ().radius >= scanRadius) {
				g.GetComponent<SphereCollider> ().radius = 0.5f;
				isScanning = false;
				timer = 0;
			}
		}
		
		public void Tick(GameObject g)
		{
			if (isScanning) {
				Scan(g);
			} else {
				timer += Time.deltaTime;
				if (timer >= pingTime)
				{
					isScanning = true;
					Scanner.badFiles = 0;
					Scanner.filesScanned = 0;
				}
			}
			Debug.Log ("Freeware Tick");
		}

		public void Deactivate(ref float startSpeed, ref float attackSpeed)
		{
			attackSpeed *= 2;
			startSpeed *= 2;
		}
	}
	
	[System.Serializable]
	public class Paid
	{
		public string name = "Paid";
        public int cost = 0;
		public float pingTime = 8.0f;
		//public float altPingTime = 3.0f;
		//public bool altPing = false;
		public bool isScanning = true;
		public float scanRadius = 25;
		public float timer = 0.0f;

        public void Activate(ref float attackSpeed, ref int playerMoney)
		{
            attackSpeed /= 2;
            playerMoney -= cost;
		}

		public void Scan(GameObject g)
		{
			g.GetComponent<SphereCollider> ().radius += 0.05f;
			if (g.GetComponent<SphereCollider> ().radius >= scanRadius) {
				g.GetComponent<SphereCollider> ().radius = 0.5f;
				isScanning = false;
				timer = 0;
			}
		}

		public void Tick(GameObject g)
		{
			if (isScanning) {
				Scan(g);
			} else {
				timer += Time.deltaTime;
				if (timer >= pingTime)
				{
					//Alt ping thing here
					isScanning = true;
					Scanner.badFiles = 0;
					Scanner.filesScanned = 0;
				}
			}
			Debug.Log ("Paid Tick");
		}
		
		public void Deactivate(ref float attackSpeed)
		{
			attackSpeed *= 2;
		}
	}
	
	[System.Serializable]
	public class Commercial
	{
		public string name = "Commercial";
        public int cost = 0;
		public bool isScanning = true;
		public float scanRadius = 25;
		public float timer = 0.0f;

        public void Activate(ref int playerMoney)
        {
            playerMoney -= cost;
		}
		
		public void Scan(GameObject g)
		{
            Debug.Log("SCAN");
			g.GetComponent<SphereCollider> ().radius += 0.05f;
			if (g.GetComponent<SphereCollider> ().radius >= scanRadius) {
				g.GetComponent<SphereCollider> ().radius = 0.5f;
				Scanner.badFiles = 0;
				Scanner.filesScanned = 0;
			}
		}
		
		public void Tick(GameObject g)
		{
			if (isScanning) {
				Scan(g);
			}

			Debug.Log ("Commercial Tick");
		}
		
		public void Deactivate()
		{

		}
	}

	public Freeware freeware = new Freeware(); 		//0
	public Paid paid = new Paid();					//1
	public Commercial commercial = new Commercial();//2

	public int currentScanner = 0;

	public float playerSpeed = 15.0f;
	public float playerAttackSpeed = 15.0f;

	public static int filesScanned = 0;
	public static int badFiles = 0;

    public int playerMoney = 0;

	public Text totalScannedText;
	public Text totalBadFoundText;

	// Use this for initialization
	void Start () {
		Activate (currentScanner);
	}

	void ChangeScanner(int index)
	{
		Deactivate(currentScanner);
		
		currentScanner = index;
		Activate (currentScanner);
	}

	void Activate(int index)
	{
		switch (index) {
		case 0:
			freeware.Activate(ref playerSpeed, ref playerAttackSpeed, ref playerMoney);
			break;
		case 1:
            paid.Activate(ref playerAttackSpeed, ref playerMoney);
			break;
		case 2:
            commercial.Activate(ref playerMoney);
			break;
		}
	}

	void Tick(int index)
	{
		switch (index) {
		case 0:
			freeware.Tick (gameObject);
			break;
		case 1:
			paid.Tick (gameObject);
			break;
		case 2:
			commercial.Tick (gameObject);
			break;
		}
	}

	void Deactivate(int index)
	{
		switch (index) {
		case 0:
			freeware.Deactivate(ref playerSpeed, ref playerAttackSpeed);
			break;
		case 1:
			paid.Deactivate (ref playerAttackSpeed);
			break;
		case 2:
			commercial.Deactivate ();
			break;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		switch (currentScanner) {
		case 0:
			if (col.tag == "File")
				filesScanned++;
			
			if (col.tag == "Enemy")
			{
				filesScanned++;
				System.Random rnd = new System.Random();
				if (rnd.Next (101) > 50)
					badFiles++;
			}
			break;
		case 1:
			if (col.tag == "File")
				filesScanned++;

			if (col.tag == "Enemy")
			{
				filesScanned++;
				System.Random rnd = new System.Random();
				if (rnd.Next (101) > 25)
					badFiles++;
			}
			break;
		case 2:
			break;
		}
	}

	void UpdateUI()
	{
		totalScannedText.text = "Files Scanned: " + filesScanned;
		totalBadFoundText.text = "Bad Files: " + badFiles;
	}

	// Update is called once per frame
	void Update () {
		int i = 0;
		if (Input.GetKeyDown (KeyCode.Alpha1))
			i = 0;
		else
			if (Input.GetKeyDown (KeyCode.Alpha2))
			i = 1;
		else
			if (Input.GetKeyDown (KeyCode.Alpha3))
			i = 2;
		else
			i = -1;

		if (i != -1)
			ChangeScanner(i);

		Tick (currentScanner);
		UpdateUI ();
	}
}
