using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(DestroyItems());
    }

    IEnumerator DestroyItems()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }
    }
}
