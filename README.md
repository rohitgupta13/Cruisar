# Cruisar
<img src="https://blipthirteen.com/images/projects/cruisar/1.jpg" alt="screenshot" height="400" />

The game is an endless-runner and the terrain is generated infinitely in the forward direction(Z-axis), it does not enforce a strict bound across the X-axis. To ensure smooth performance the game uses object pooling.


ContinuousPlaneGenerator.cs
```
public class ContinuousPlaneGenerator : MonoBehaviour
{
    private Queue<Vector3> obstacles = new Queue<Vector3>();
    private Queue<Vector3> droppingPositions = new Queue<Vector3>();
    private double drawDelta = 0;
    private float lastZPosition;
    private float lastZDraw;
    public GameObject icePrefab;
    public GameObject plane;

    void Start()
    {
        // Adjust for players staring position
        this.lastZPosition = this.transform.position.z;
        Generate(0, 400);
    }

    void Update()
    {
        double deltaZ = this.transform.position.z - lastZPosition;
        lastZPosition = this.transform.position.z;
        
        drawDelta += deltaZ;
        if (drawDelta > 200d)
        {
            drawDelta = 0;
            Generate(lastZPosition + 200, 200);
            Vector3 planePosition = plane.transform.position;
            planePosition.x = this.transform.position.x;
            planePosition.z = this.transform.position.z + 200;
            plane.transform.position = planePosition;
        }
        
        RemoveStalePositions();
        PlaceAndRemove();

        for (int i = 0; i < droppingPositions.Count; i++)
        {
            Vector3 pos = droppingPositions.Dequeue();
            //if (true)
            if (this.transform.position.z + 150 > pos.z && inRange(this.transform.position, pos, 50))
            {
                drop(pos);
            }
            else
            {
                // add them back
                droppingPositions.Enqueue(pos);
            }
        }
    }

    private void Generate(float zStart, float drawUpto)
    {
        if (this.transform.position.z < 500)
        {
            lastZDraw = zStart + drawUpto;
            return;
        }
        for (float z = zStart; z <= zStart + drawUpto; z += 15)
        {
            Debug.Log("Generating for Z= " + z);
            int probabilityOfObstacle = Random.Range(2, 10);
            bool createObstacle = false;
            if (probabilityOfObstacle == 3 || probabilityOfObstacle == 7) { // || probabilityOfObstacle == 5) {
                createObstacle = true;
            }

            int countOfX = 0;
            for (float x = this.transform.position.x - 120; x < this.transform.position.x + 150; x += 5)
            {
                countOfX++;
                int probability = Random.Range(1, 28);
                if (countOfX < 2 || countOfX > 53)
                {
                    if (Random.Range(0, 3) == 1) {
                        obstacles.Enqueue(new Vector3(x, 0, z));
                    }
                }
                else
                if ((probability == 3 || probability == 9 || probabilityOfObstacle == 7 && probabilityOfObstacle == 13) && createObstacle)
                {
                    obstacles.Enqueue(new Vector3(x, 0, z));
                }
                else
                {
                    int dropProbability = Random.Range(1, 25);
                    if (dropProbability == 2)
                    {
                        droppingPositions.Enqueue(new Vector3(x, 0, z));
                    }
                }
            }
            
        }
        lastZDraw = zStart + drawUpto;
    }

    private GameObject drop(Vector3 position)
    {
        Globals.totalObjects++;
        float height = Random.Range(40F, 50F);
        position.y = height;
        GameObject ice = Instantiate(icePrefab, position, Quaternion.identity);
        
        float rotation = Random.Range(0F, 360F);
        float angle = Random.Range(0, 2) * 180;
        
        Rigidbody rb = ice.transform.GetChild(1).GetComponent<Rigidbody>();

        if (this.transform.position.z > 1000)
        {
            float randomVelocity = Random.Range(68F, 75F);
            rb.velocity = new Vector3(0, -1 * randomVelocity, 0);
        }
        else
        {
            Destroy(rb);
        }
        ice.transform.Rotate(0, rotation, angle, Space.Self);
        ice.SetActive(true);
        return ice;
    }

    private void RemoveStalePositions(){
        int count = 0;
        for (int i = 0; i < obstacles.Count; i++)
        {
            Vector3 pos = obstacles.Dequeue();
            if (pos.z + 5 < this.transform.position.z)
            {
                count++;
            }
            else { 
                obstacles.Enqueue(pos);
            }
        }
        if (count > 0) { 
            Debug.Log("Removed " + count);
        }
    }

        

    private void PlaceAndRemove()
    {
        int count = 0;
        foreach (Vector3 pos in obstacles)
        {
            // Place
            if (pos.z - this.transform.position.z < 300 && inRange(this.transform.position, pos, 150))
            {
                if (pos.z - this.transform.position.z > -5)
                {
                    count++;
                    GameObject p = ObjectPool.pillars.Dequeue();
                    if (p == null)
                    {
                        Debug.Log("Pool is empty " + ObjectPool.pillars.Count + ", creating more!");
                        p = ObjectPool.createPillar();
                    }
                    Vector3 newPos = pos;
                    newPos.y = -1.94F;
                    p.transform.position = newPos;
                    p.SetActive(true);
                    ObjectPool.pillars.Enqueue(p);
                }
            }
        }
        Debug.Log("Needed: " + count);
    }

    private bool inRange(Vector3 obj, Vector3 obstacle, int range) {
        if (Math.Abs(obj.x - obstacle.x) < range) {
            return true;
        }
        return false;
    }
}
```

ObjectPool.cs
```
public class ObjectPool : MonoBehaviour
{
	public static Queue<GameObject> pillars;
	public static GameObject myPrefab;
	public GameObject pillarPrefab;
	public GameObject player;
	public static int poolSize = 70;

	void Start()
	{
		myPrefab = pillarPrefab;
		initQueue();
	}

	// Update is called once per frame
	void Update()
	{
	
	}

	public static void initQueue()
	{
		pillars = new Queue<GameObject>();
		for (int i = 0; i < poolSize; i++)
		{
			pillars.Enqueue(createPillar());
		}
	}

	public static GameObject createPillar()
	{
		GameObject pillar = Instantiate(myPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		pillar.transform.localScale = new Vector3(2.5F, 2.5F, 2.5F);
		pillar.transform.Rotate(0, 135f, 0.0f, Space.Self);
		pillar.SetActive(false);
		return pillar;
	}
}
```
