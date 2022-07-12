using UnityEngine;

public class SpanwerCenter : MonoBehaviour
{
    internal float RotateSpeed { get; set; } = 180f;

    void Update()
    {
        if (GameManager.IsGameStarted)
        {
            if (Random.Range(1, 5) == 1)
            {
                RotateSpeed *= -1;
            }
            if (Random.Range(1, 7) == 1)
            {
                RotateSpeed = Random.Range(180, 361);
            }
            transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
        }
    }
}
