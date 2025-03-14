using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTileEmpty : MonoBehaviour
{
    public Transform playerTransform;

    private void Update()
    {
        if ((transform.position - playerTransform.position).magnitude >= 7)
        {
            Destroy(gameObject);
        }
    }
}
