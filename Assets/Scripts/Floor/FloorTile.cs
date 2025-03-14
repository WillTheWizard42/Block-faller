using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTile : FloorTileEmpty
{
    public GameObject redTile;
    public Player player;
    Transform levelTransform;

    private void Start()
    {
        levelTransform = GameObject.Find("Level").transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 6)
        {
            player.score++;
            Instantiate(redTile, transform.position, transform.rotation, levelTransform);
            Destroy(gameObject);
        }
    }
}
