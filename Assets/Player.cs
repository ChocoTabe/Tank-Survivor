using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] internal GameObject bullet;
    [SerializeField] internal float MoveSpeed { get; set; } = 30f;
    [SerializeField] internal static float ShotSpeed { get; set; } = 0.1f;
    internal static float XBound { get; private set; } = 41.2f;
    internal static float ZBound { get; private set; } = 19.7f;
    internal float RotateSpeed { get; private set; } = 300f;
    internal float PlayerHP { get; set; } = 100f;
    internal bool IsTripleUsed { get; set; } = false;

    void Start()
    {
        PlayerHP = 100f;
        StartCoroutine(ShotBullet());
    }

    void Update()
    {
        if (GameManager.IsGameStarted && PlayerHP > 0)
        {
            var greaterThanXBound = transform.position.x > -XBound;
            var lessThanXBound = transform.position.x < XBound;
            var greaterThanZBound = transform.position.z > -ZBound;
            var lessThanZBound = transform.position.z < ZBound;

            if (MoveLeft() && greaterThanXBound)
            {
                transform.Translate(-MoveSpeed * Time.deltaTime, 0, 0, Space.World);
            }
            if (MoveRight() && lessThanXBound)
            {
                transform.Translate(MoveSpeed * Time.deltaTime, 0, 0, Space.World);
            }
            if (MoveDown() && greaterThanZBound)
            {
                transform.Translate(0, 0, -MoveSpeed * Time.deltaTime, Space.World);
            }
            if (MoveUp() && lessThanZBound)
            {
                transform.Translate(0, 0, MoveSpeed * Time.deltaTime, Space.World);
            }
            if (TurnLeft())
            {
                transform.Rotate(0, -RotateSpeed * Time.deltaTime, 0);
            }
            if (TurnRight())
            {
                transform.Rotate(0, RotateSpeed * Time.deltaTime, 0);
            }
        }
        if (PlayerHP <= 0)
        {
            StopAllCoroutines();
        }
    }
    IEnumerator ShotBullet()
    {
        while (true)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            yield return new WaitForSeconds(ShotSpeed);
        }
    }
    IEnumerator TripleShotBullet()
    {
        while (true)
        {
            Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, 25, 0));
            Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, -25, 0));
            yield return new WaitForSeconds(ShotSpeed);
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ShotSpeedUp"))
        {
            Destroy(other.gameObject);
            ShotSpeed -= 0.001f;
            TextManager.ScoreText++;
        }
        if (other.CompareTag("TripleShot"))
        {
            Destroy(other.gameObject);
            TextManager.ScoreText += 30;
            if (IsTripleUsed == false)
            {
                StartCoroutine(TripleShotBullet());
                IsTripleUsed = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && PlayerHP > 0)
        {
            PlayerHP -= 1f;
        }
    }

    static bool MoveLeft() => Input.GetKey(KeyCode.A);
    static bool MoveRight() => Input.GetKey(KeyCode.D);
    static bool MoveUp() => Input.GetKey(KeyCode.W);
    static bool MoveDown() => Input.GetKey(KeyCode.S);
    //static bool PressSpace() => Input.GetKeyDown(KeyCode.Space);
    static bool TurnLeft() => Input.GetKey(KeyCode.LeftArrow);
    static bool TurnRight() => Input.GetKey(KeyCode.RightArrow);
}
