using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goldtrigger : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("is destroyed");
            Destroy(gameObject);
        }
    }
}
