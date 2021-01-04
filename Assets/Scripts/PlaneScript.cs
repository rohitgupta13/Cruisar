using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlaneScript : MonoBehaviour {

	
	public GameObject icePrefab;
	
    public Transform myPlay;
    public Transform myBuddy;
	public Transform basePlane;
	//List<GameObject> gameObjects;
	List<Vector3> gameObjects;
	Queue<Vector3> droppingPositions;
	List<GameObject> droppingObjects;
	List<Vector3> emptySlots;
	public float dropHeight = 100F;
    
	private int planeNumber = 0;
	private bool dropped;
	
	// Use this for initialization
    void Start () {
		//QualitySettings.SetQualityLevel (0);
		init();
    }



	private void init()
    {
		planeNumber += 1;
		dropped = false;
		//gameObjects = new List<GameObject>();
		gameObjects = new List<Vector3>();
		emptySlots = new List<Vector3> ();
		float initX = ((basePlane.transform.localScale.x / 2) * 10) - 2.5F;
		float initZ = ((basePlane.transform.localScale.z / 2) * 10) - 2.5F;
        float xPos = transform.position.x - initX;
        float zPos = transform.position.z - initZ;
		//int iterSizeZ = (int) basePlane.transform.localScale.z * 2;
		int iterSizeX = (int) basePlane.transform.localScale.x * 2;
		int iterSizeZ = 40;
		for (int zCount = 0; zCount < iterSizeZ; zCount++) {
			int probabilityOfObstacle = Random.Range(2, 10);
            bool createObstacle = false;
            if (probabilityOfObstacle == 3 || probabilityOfObstacle == 7) { // || probabilityOfObstacle == 5) {
                createObstacle = true;
            }
            xPos = transform.position.x - initX;
            for (int xCount = 0; xCount < iterSizeX; xCount++)
            {
				int probability = Random.Range(1, 28);
                if ( (probability == 3 || probability == 9 || probabilityOfObstacle == 7 && probabilityOfObstacle == 13) && createObstacle )
				{
					// Set positions where pillars will be placed from the queue
					gameObjects.Add(new Vector3(xPos, -1.94F, zPos));
                }
                else {
					// Set empty positions
					emptySlots.Add (new Vector3(xPos, dropHeight, zPos));
				}
				xPos += 5F;
            }
            zPos += 15;
		}
		Debug.Log("Created static: " + gameObjects.Capacity);
		//Globals.totalObjects += gameObjects.Capacity;
	}

	// Update is called once per frame
	void Update() {
		if (transform.position.z + 305 < myPlay.position.z)
		{
			generateNew();
		}
		placeAndRemove();

		if (transform.position.z - 305 > myPlay.position.z)
		{
			// if(planeNumber % 3 == 0 && dropped == false){
			if (dropped == false) {
				droppingObjects = new List<GameObject>();
				droppingPositions = new Queue<Vector3>();
				foreach (Vector3 emptyPosition in emptySlots)
				{
					int dropProbability = Random.Range(1, 25);
					if (dropProbability == 2) {
						// Debug.Log ("Plane Numer = " + planeNumber);
						// Debug.Log ("Probability = " + dropProbability);
						droppingPositions.Enqueue(emptyPosition);
						//drop (emptyPosition);			
					}
				}
				dropped = true;
				//Debug.Log("Falling: " + droppingPositions.Count);
				//Globals.totalObjects += droppingPositions.Count;
			}
		}

		for (int i = 0; i < droppingPositions.Count; i++) {
			Vector3 pos = droppingPositions.Dequeue();
			if (myPlay.position.z + 200 > pos.z)
			{
				drop(pos);
			}
			else {
				droppingPositions.Enqueue(pos);
			}
		}
	}

	private GameObject drop(Vector3 position)
	{
		Globals.totalObjects++;
		//float height = Random.Range(15F, 25F);
		float height = Random.Range(40F, 50F);
		position.y = height;
		GameObject ice = Instantiate(icePrefab, position, Quaternion.identity);
		// ice.transform.localScale = new Vector3(1.5F, 4F, 1.5F);
		float rotation = Random.Range(0F, 360F);
		float angle = Random.Range(0, 2) * 180;
		//Debug.Log(angle);
		Rigidbody rb = ice.transform.GetChild(1).GetComponent<Rigidbody>();

		if (myPlay.position.z > 500)
		{
			float randomVelocity = Random.Range(68F, 75F);
			rb.velocity = new Vector3(0, -1 * randomVelocity, 0);
		}
		else
		{
			Destroy(rb);
		}
		ice.transform.Rotate(0, rotation, angle, Space.Self);
		droppingObjects.Add(ice);
		ice.SetActive(true);
		return ice;
	}

	private void placeAndRemove()
	{
		int count = 0;
		foreach (Vector3 pos in gameObjects)
		{
			// Place
			if (pos.z - myPlay.position.z < 190)
			{
				if (pos.z - myPlay.position.z > -5) {
					count++;
					GameObject p = ObjectPool.pillars.Dequeue();
					if (p == null)
					{
						Debug.Log("Pool is empty " + ObjectPool.pillars.Count + ", creating more!");
						p = ObjectPool.createPillar();
					}
					p.transform.position = pos;
					p.SetActive(true);
					ObjectPool.pillars.Enqueue(p);
				}
			}
		}
		//Debug.Log("Placed: " + count);
	}

	void generateNew(){
		transform.position = new Vector3(0 , 0, myBuddy.position.z + 600);
		init();
	}
}