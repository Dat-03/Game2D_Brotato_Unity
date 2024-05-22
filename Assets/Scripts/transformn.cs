using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trsn : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = newPosition;
    }
}
