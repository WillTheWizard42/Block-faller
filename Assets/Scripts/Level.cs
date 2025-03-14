using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public float rotationx;
    public float eulerx;
    private float rotationz;
    public float eulerz;
    float maxAngle = 10f;
    float minStep;
    float maxStep;
    float step;
    int t;
    Quaternion old;
    Quaternion target;

    // Start is called before the first frame update
    void Start()
    {
        t = 0;
        minStep = Time.fixedDeltaTime / 4;
        maxStep = Time.fixedDeltaTime;
        rotationx = Random.Range(-maxAngle, maxAngle);
        rotationz = Random.Range(-maxAngle, maxAngle);
        eulerx = (rotationx + 360) % 360;
        eulerz = (rotationz + 360) % 360;
        step = Random.Range(minStep, maxStep);
        old = Quaternion.identity;
        target = Quaternion.Euler(rotationx, 0, rotationz);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        t++;
        if (Mathf.Abs(transform.eulerAngles.x - eulerx) <= 0.1)
        {
            rotationx = Random.Range(-maxAngle, maxAngle);
            eulerx = (rotationx + 360) % 360;
            target = Quaternion.Euler(rotationx, 0, rotationz);
            step = Random.Range(minStep, maxStep);
            old = transform.rotation;
            t = 0;
        }
        if (Mathf.Abs(transform.eulerAngles.z - eulerz) <= 0.1)
        {
            t = 0;
            rotationz = Random.Range(-maxAngle, maxAngle);
            eulerz = (rotationz + 360) % 360;
            target = Quaternion.Euler(rotationx, 0, rotationz);
            step = Random.Range(minStep, maxStep);
            old = transform.rotation;
        }
        transform.rotation = Quaternion.Slerp(old, target, t* step);
    }
}
