using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTile : MonoBehaviour
{
    public GameObject redTileFalling;
    int timer;
    Transform levelTransform;


    // Start is called before the first frame update
    void Start()
    {
        timer = (int)(3 / Time.fixedDeltaTime);
        levelTransform = GameObject.Find("Level").transform;
    }

    private void FixedUpdate()
    {
        if (timer > 0)
        {
            timer--;
        }
        else
        {
            GameObject newRedTileFalling = Instantiate(redTileFalling, transform.position, transform.rotation, levelTransform);
            Destroy(gameObject);
        }
    }
}
