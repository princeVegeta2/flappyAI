using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float _velocity = 1.5f;
    [SerializeField] private float _rotationSpeed = 10f;

    private Rigidbody2D _rb;
    private bool _gameStarted = false; // Flag to determine if the game has started

    public int Score {get; private set; } = 0;
    public delegate void ScoreHandler();
    public event ScoreHandler OnScored;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0; // Disable gravity so the bird stays still until the first jump
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (_gameStarted)
        {
            transform.rotation = Quaternion.Euler(0, 0, _rb.velocity.y * _rotationSpeed);
        }
    }

    public void ScorePoint()
    {
        Score++;
        OnScored?.Invoke();
    }

    public int GetScore()
    {
        return Score;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Pipe") || collider.gameObject.CompareTag("Ground"))
        {
            ResetGame();
            GetComponent<BirdAgent>().HandlePipeCollision();
        }
    }

    public void ResetGame()
    {
        Score = 0;
        OnScored?.Invoke();

        transform.position = new Vector2(1.2689f, 0.0444f);
        _rb.velocity = Vector2.zero;
        _rb.gravityScale = 0;
        _gameStarted = false;

        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Pipe");
        foreach (GameObject pipe in pipes)
        {
            Destroy(pipe);
        }
    }

    public void Jump()
    {
        Debug.Log("Jump method called."); 
        if (!_gameStarted)
        {
            _gameStarted = true;
            _rb.gravityScale = 1;
        }
        
        _rb.velocity = Vector2.up * _velocity;
    }
    
    public Rigidbody2D GetRigidbody2D()
    {
        return _rb;
    }

    // For AI to start jumping straight away
    public void StartGame()
    {
        _gameStarted = true;
        _rb.gravityScale = 1;
    }

}


