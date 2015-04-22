using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScannerScript : MonoBehaviour {
	
	public int cost = 0;
	public float pingTime = 8.0f;
	public float scanRadius = 25;
	public float timer = 0.0f;
    public float rps = 0.05f;
    public Material detectedMaterial;

	//Current scanner version index
	private int currentScanner = 0;

	//Multipliers for affecting the player
	private float moveMult = 1f;
	private float attackMult = 1f;
	private int playerMoney  = 0;

	//Current scan state - true so the player can scan right away.
	private bool isScanning = true;

	//Public so we can update the UI
	public bool isActive = false;
	
	private int filesScanned = 0;
	private int badFiles = 0;

    private Vector3 originalSize;
    private float originalSphereSize;
	
	
	// Use this for initialization
	void Start () 
    {
        originalSize = transform.localScale;
        originalSphereSize = this.GetComponent<SphereCollider>().radius;
	}

	void Update () 
    {
		//Update internal variables
		playerMoney = GameObject.FindGameObjectWithTag ("Core").GetComponent<CoreScript>().money;

		//Methods to invoke
		HandleInput ();
		Tick ();
		UpdateUI ();

		//Update external variables
		GameObject.FindGameObjectWithTag ("Core").GetComponent<CoreScript>().money = playerMoney;

		PlayerScript pScript = GameObject.FindGameObjectWithTag ("Player").GetComponent (typeof(PlayerScript)) as PlayerScript;
		pScript.attackMultiplier = attackMult;
		pScript.moveMultiplier = moveMult;
	}

	void UpdateUI()
	{
		if (isActive) 
        {
			//totalScannedText.text = "Files Scanned: " + filesScanned;
			//totalBadFoundText.text = "Bad Files: " + badFiles;
		}            
            //Update core health
        PlayerScript pScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerScript> ();
        GameObject.Find ("ScanText").GetComponent<Text>().text =
            "Scanned\t:\t" + filesScanned.ToString() + "\nDetected\t:\t" + badFiles.ToString() + "\nDestroyed\t:\t" + pScript.destroyed.ToString() + "\n";
	}
	
	void ChangeScanner(int index)
	{
		Deactivate();
		
		currentScanner = index;
		Activate ();
	}

	void Activate()
	{
		isActive = true;
		
		switch (currentScanner)
		{
			//Freeware 
			case 0:
				attackMult = 0.5f;
				moveMult = 0.5f;
				break;
			default:
				break;
		}
	}
	
	void Deactivate()
	{
		isScanning = false;
		isActive = false;
		attackMult = 1f;
		moveMult = 1f;

        transform.localScale = originalSize;
        this.GetComponent<SphereCollider>().radius = originalSphereSize;
	}

	void Tick()
	{
		if (isActive) 
		{

			switch (currentScanner) 
			{

				//Freeware Scanner
				case 0:
					if (isScanning) 
					{
						Scan ();
					}
					else 
					{
						timer += Time.deltaTime;
						if (timer >= pingTime) 
						{
							isScanning = true;
							badFiles = 0;
							filesScanned = 0;
						}
					}
					break;

                case 1:
                    if (isScanning) 
                    {
                        Scan ();
                    }
                    else 
                    {
                        timer += Time.deltaTime;
                        if (timer >= pingTime) 
                        {
                            isScanning = true;
                            badFiles = 0;
                            filesScanned = 0;
                        }
                    }
                    break;
                    
                    
                default:
                    break;
				}
		} 

		else 
		{

		}
	}

	public void Scan()
	{
        float rIncrease = rps * Time.deltaTime;

		this.GetComponent<SphereCollider> ().radius += rIncrease;
		transform.localScale = new Vector3( transform.localScale.x + rIncrease, 
                                            transform.localScale.y + rIncrease, 
                                            transform.localScale.z + rIncrease);
		
		if (this.GetComponent<SphereCollider> ().radius >= scanRadius) 
		{
			this.GetComponent<SphereCollider> ().radius = 0.2f;
			transform.localScale = new Vector3(1, 1, 1);
			isScanning = false;
			timer = 0;
		}
	}
	
	void OnTriggerEnter(Collider col)
	{
        Debug.Log("Found: " + col.name);

		if (isActive) {
			switch (currentScanner) 
            {

    			//Freeware scanner
    			case 0:
    				//Bit detected
    				if (col.tag == "Bit")
    					filesScanned++;

    				//Virus detected
    				if (col.tag == "Virus") 
                    {
    					filesScanned++;
    					System.Random rnd = new System.Random ();
                        
    					if (rnd.Next (100) < 50) 
                        {

    						//Successful detection, set condition here.
                            col.gameObject.GetComponent<MeshRenderer>().material = detectedMaterial;
    						badFiles++;
    					}
    				}
    				break;

                case 1:
                    //Bit detected
                    if (col.tag == "Bit")
                        filesScanned++;
                    
                    //Virus detected
                    if (col.tag == "Virus") 
                    {
                        filesScanned++;
                        System.Random rnd = new System.Random ();
                        
                        if (rnd.Next (100) < 75) 
                        {
                            
                            //Successful detection, set condition here.
                            col.gameObject.GetComponent<MeshRenderer>().material = detectedMaterial;
                            badFiles++;
                        }
                    }
                    break;
                    
                default:
                    break;
            }
		}
	}

	void HandleInput()
	{
		if (Input.GetButton ("TScanner")) 
        {
			if(isActive) Deactivate();
			else Activate ();
		}
	}

}
