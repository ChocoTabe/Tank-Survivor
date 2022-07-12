using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (GameManager.IsGameStarted)
        {
            transform.Translate(0, 0, 60 * Time.deltaTime);
            if (transform.position.x > Player.XBound || transform.position.x < -Player.XBound)
            {
                Destroy(gameObject);
            }
            else if (transform.position.z > Player.ZBound || transform.position.z < -Player.ZBound)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
