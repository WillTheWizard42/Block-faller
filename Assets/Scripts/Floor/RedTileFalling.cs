using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedTileFalling : MonoBehaviour
{
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (transform.position.y < -30)
        {
            Destroy(gameObject);
        }
    }
}
