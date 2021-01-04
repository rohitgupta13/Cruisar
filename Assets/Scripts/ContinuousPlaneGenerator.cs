using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class ContinuousPlaneGenerator : MonoBehaviour
{
    private Queue<Vector3> obstacles = new Queue<Vector3>();
    private Queue<Vector3> droppingPositions = new Queue<Vector3>();
    private double drawDelta = 0;
    //public float drawDistanceZ = 200;
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
        //Debug.Log("DeltaZ :" + deltaZ);
        drawDelta += deltaZ;
        //Debug.Log("drawDelta :" + drawDelta);
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

        // int c = 0;
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
        // Debug.Log("Dropping :" + c +" were not in Range");
        Debug.Log("After Dropping :" + droppingPositions.Count);

    }

    private void Generate(float zStart, float drawUpto)
    {
        if (this.transform.position.z < 500)
        {
            lastZDraw = zStart + drawUpto;
            return;
        }
        Debug.Log("Generating at " + this.transform.position.z + " from " + zStart + " to " + (zStart + drawUpto));
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
            Debug.Log("CountOfX:" + countOfX);
        }
        lastZDraw = zStart + drawUpto;
        Debug.Log("Static :" + obstacles.Count);
        Debug.Log("Dropping :" + droppingPositions.Count);
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
    

    /*bool increasedBy(float value) {
        float d = Mathf.Abs(this.distanceTravelled - markingPosition);
        if( > 200)
    }*/


    // Update is called once per frame
}
