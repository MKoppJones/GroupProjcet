using UnityEngine;
using System.Collections;

public class PFind : MonoBehaviour {

	public float moveSpeed = 1f;
	float approachDist = 0.01f;

	private int cGridC = 0, cGridR = 0; //Current grid position of this entity.
	private int[,] eGridCosts; //Used for cloning the grid costs of the MapLoad object.
	private bool isMoving = false;
	private Vector3 cPosition;
	private Vector3 tPosition;

	int moveDir = 0; //1 = right, 2 = left, 3 = down, 4 = up

	// Use this for initialization
	void Start () 
	{
        //Get the grid costs so the entity knows what to follow
		eGridCosts = GameObject.Find ("MapLoader").GetComponent<MapLoad> ().gridCosts;

		cGridC = (int)transform.position.x; cGridR = (int)transform.position.z;

	}

	public void ReloadMap()
	{
		eGridCosts = GameObject.Find ("MapLoader").GetComponent<MapLoad> ().gridCosts;
	}

	void Update()
	{
		CalculateNextMovement ();
		MoveEntityOnGrid ();

	}

	void CalculateNextMovement()
	{

		if (!isMoving) 
		{
			if(eGridCosts[cGridR, cGridC] == 1) ProcessCoreEntry();

			isMoving = true;
			cPosition = transform.position;
			int lowestCost = 10000;
			
			if( cGridC != eGridCosts.GetLength (1)-1) if(lowestCost > eGridCosts[cGridR, cGridC+1])
			{
				lowestCost = eGridCosts[cGridR, cGridC+1];
				moveDir = 1;
			}
			
			if( cGridC != 0) if(lowestCost > eGridCosts[cGridR, cGridC-1])
			{
				lowestCost = eGridCosts[cGridR, cGridC-1];
				moveDir = 2;
			}
			
			if( cGridR != eGridCosts.GetLength (0)-1) if(lowestCost > eGridCosts[cGridR+1, cGridC])
			{
				lowestCost = eGridCosts[cGridR+1, cGridC];
				moveDir = 3;
			}
			
			if( cGridR != 0) if(lowestCost > eGridCosts[cGridR-1, cGridC])
			{
				lowestCost = eGridCosts[cGridR-1, cGridC];
				moveDir = 4;
			}
		}
		//Debug.Log (cGridC.ToString () + ", " + cGridR.ToString ());
	}

	void MoveEntityOnGrid()
	{
		float speed  = moveSpeed * Time.deltaTime;


		if(moveDir == 1)
		{
			tPosition = new Vector3(cPosition.x+1, cPosition.y, cPosition.z);
			transform.position = Vector3.MoveTowards (transform.position, tPosition, speed);
		}

		if(moveDir == 2)
		{
			tPosition = new Vector3(cPosition.x-1, cPosition.y, cPosition.z);
			transform.position = Vector3.MoveTowards (transform.position, tPosition, speed);
		}

		if(moveDir == 3)
		{
			tPosition = new Vector3(cPosition.x, cPosition.y, cPosition.z+1);
			transform.position = Vector3.MoveTowards (transform.position, tPosition, speed);
		}

		if(moveDir == 4)
		{
			tPosition = new Vector3(cPosition.x, cPosition.y, cPosition.z-1);
			transform.position = Vector3.MoveTowards (transform.position, tPosition, speed);
		}

		if ((tPosition - transform.position).magnitude < approachDist) {
			isMoving = false;
			transform.position = tPosition;
			cGridC = (int)transform.position.x; cGridR = (int)transform.position.z;
		} 
	}

	void ProcessCoreEntry()
	{
		Destroy (this);
	}
}
