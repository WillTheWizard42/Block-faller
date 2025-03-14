using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] Vector3 offset;
    [SerializeField] Transform target;

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset;
    }
}
