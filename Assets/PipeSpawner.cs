using UnityEngine;
using System.Collections;
using System.Collections.Generic;   

public class PipeSpawner : MonoBehaviour
{
    public GameObject topPipePrefab;
    public GameObject bottomPipePrefab;
    public float spawnRate = 1f;
    public float gapSize = 3f; // The gap size between the top and bottom pipes
    public GameObject scoreTriggerPrefab;
    
    public float topPipeMinY = -0.5f; 
    public float topPipeMaxY = 1.5f; 
    public float bottomPipeMinY = -1.5f; 
    public float bottomPipeMaxY = 0.5f; 
    

    public List<GameObject> pipes = new List<GameObject>(); 

    void Start()
    {
        InvokeRepeating("SpawnPipe", 0, spawnRate);
    }

    void SpawnPipe()
    {
        
        float topVerticalPosition = Random.Range(topPipeMinY, topPipeMaxY);
        float bottomVerticalPosition = Random.Range(bottomPipeMinY, bottomPipeMaxY);
        

        while (Mathf.Abs(topVerticalPosition - bottomVerticalPosition) < gapSize)
        {
            topVerticalPosition = Random.Range(topPipeMinY, topPipeMaxY);
            bottomVerticalPosition = Random.Range(bottomPipeMinY, bottomPipeMaxY);
        }

        GameObject topPipe = Instantiate(topPipePrefab, new Vector3(10, topVerticalPosition, 0), Quaternion.identity);
        GameObject bottomPipe = Instantiate(bottomPipePrefab, new Vector3(10, bottomVerticalPosition, 0), Quaternion.identity);

        pipes.Add(topPipe);
        pipes.Add(bottomPipe);

        GameObject scoreTrigger = Instantiate(scoreTriggerPrefab, topPipe.transform.position, Quaternion.identity);
        scoreTrigger.transform.SetParent(topPipe.transform);

        Destroy(topPipe, 10f);
        Destroy(bottomPipe, 10f);
    }

    /*public (GameObject topPipe, GameObject bottomPipe) FindNextPipes(Transform birdTransform)
    {
        float closestDistance = float.PositiveInfinity;
        GameObject closestTopPipe = null;
        GameObject closestBottomPipe = null;

        for (int i = 0; i < pipes.Count; i += 2)
        {
            GameObject topPipe = pipes[i];
            GameObject bottomPipe = pipes[i + 1];

            float distance = topPipe.transform.position.x - birdTransform.position.x;

            if (distance > 0 && distance < closestDistance)
            {
                closestDistance = distance;
                closestTopPipe = topPipe;
                closestBottomPipe = bottomPipe;
            }
        }

        var result = (closestTopPipe, closestBottomPipe);
        Debug.Log($"Finding next pipes: {result.Item1?.name}, {result.Item2?.name}");
        return result;
    }
    */
}




