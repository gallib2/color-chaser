using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatforDestroyer : MonoBehaviour
{
    public GameObject platformDestractionPoint;


    // Start is called before the first frame update
    void Start()
    {
        platformDestractionPoint = GameObject.Find("PlatformDestructionPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < platformDestractionPoint.transform.position.x)
        {
            //Destroy(gameObject);

            gameObject.SetActive(false);
        }
    }
}
