using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAgent : Agent
{
    // Reference to BirdController script
    private BirdController birdController;
    // Reference to PipeSpawner script
    private PipeSpawner pipeSpawner;


    private void Start()
    {
        birdController = GetComponent<BirdController>();
        birdController.OnScored += HandleScoring;
        pipeSpawner = GetComponent<PipeSpawner>();
        //Debug.Log("BirdAgent Started");
    }

    private void Update()
    {

    }

    public override void OnEpisodeBegin()
    {
        birdController.ResetGame();
        birdController.StartGame();
        //Debug.Log("Episode Began");
    }

    public void HandlePipeCollision()
    {
        AddReward(-1.0f);
        EndEpisode();
        //Debug.Log("Pipe Collision Handled");
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        Vector2[] rayDirections = {
            transform.right, // forward
            (transform.right + transform.up).normalized, // up-forward
            (transform.right - transform.up).normalized, // down-forward
            transform.up, // up
            -transform.up // down
        };

        float rayLength = 10f;
        List<float> observations = new List<float>();

        foreach (Vector2 rayDirection in rayDirections)
        {
            Vector2 rayStart = transform.position;
            Vector2 rayEnd = rayStart + rayDirection * rayLength;

            Debug.DrawLine(rayStart, rayEnd, Color.red, 0.02f);
            
            //Gizmos.DrawLine(rayStart, rayEnd);
            
            RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, rayLength);

            if (hit.collider != null && (hit.collider.CompareTag("Pipe") || hit.collider.CompareTag("Ground")))
            {
                sensor.AddObservation(hit.distance);
                sensor.AddObservation(hit.collider.transform.position.y);
                observations.Add(hit.distance);
                observations.Add(hit.collider.transform.position.y);
            }
            else
            {
                sensor.AddObservation(-1f);
                sensor.AddObservation(0f);
                observations.Add(-1f);
                observations.Add(0f);
            }
        }
        float birdVelocityY = birdController.GetRigidbody2D().velocity.y;
        sensor.AddObservation(birdVelocityY);
        observations.Add(birdVelocityY);

        Debug.Log($"Observations: {string.Join(", ", observations)}");
    }


    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        int action = Mathf.FloorToInt(actionBuffers.DiscreteActions[0]);
        //Debug.Log($"Action Received: {action}");

        if (action == 1)
        {
            birdController.Jump();
        }

        // Reward for being alive
        AddReward(0.01f);
    }

    private void HandleScoring()
    {
        AddReward(0.25f); // Reward for passing through pipes. I kept the number low so that it does not prioritize scoring points  
        //Debug.Log("Score Handled");
    }


    
}

