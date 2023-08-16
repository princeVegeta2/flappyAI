using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BirdController bird = collision.gameObject.GetComponent<BirdController>();
            if (bird != null)
            {
                bird.ScorePoint(); // Increment the score
                Destroy(gameObject); // Destroy the trigger so it doesn't score again
            }
        }
    }
}
