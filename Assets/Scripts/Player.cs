using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int score = 0;

    [SerializeField] GameObject floorTile;
    [SerializeField] GameObject floorTileEmpty;
    [SerializeField] LayerMask playerMask;
    [SerializeField] Transform groundCheckTransform;
    [SerializeField] Transform level;
    [SerializeField] Transform checkTransform;
    private LayerMask floorMask;
    private float holePercent = 0.3f;
    float jumppower = 5;
    float horizontal_movement;
    float vertical_movement;

    Rigidbody Rigidbody;
    bool jump_button_pressed = false;
    [SerializeField] float movement_speed = 3f;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("score", score);
        Rigidbody = GetComponent<Rigidbody>();
        floorMask = LayerMask.GetMask("Default");
        floorMask |= (1 << LayerMask.NameToLayer("TransparentFX"));
    }

    private void generateGround(Vector3 position)
    {
        GameObject tileObject;
        FloorTileEmpty newTile;
        float random = Random.Range(0f, 1f);
        if (random > holePercent)
        {
            tileObject = Instantiate(floorTile, position, Quaternion.identity);
            tileObject.transform.SetParent(level, false);
            newTile = tileObject.GetComponent<FloorTile>();
            newTile.playerTransform = groundCheckTransform;
            ((FloorTile)(newTile)).player = this;
        }
        else
        {
            tileObject = Instantiate(floorTileEmpty, position, Quaternion.identity);
            tileObject.transform.SetParent(level, false);
            newTile = tileObject.GetComponent<FloorTileEmpty>();
            newTile.playerTransform = groundCheckTransform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -15)
        {
            Time.timeScale = 0f;
            SceneManager.LoadScene("EndGame");
        }

        //movement
        horizontal_movement = Input.GetAxis("Horizontal");
        vertical_movement = Input.GetAxis("Vertical");
        if (Physics.CheckSphere(groundCheckTransform.position, 0.01f, playerMask))
        {
            if (Input.GetButton("Jump"))
            {
                jump_button_pressed = true;
            }
        }
    }

    private void FixedUpdate()
    {
        int x = (int)groundCheckTransform.position.x;
        int z = (int)groundCheckTransform.position.z;
        //Terrain generation        

        for (int i = -4; i < 5; i++)
        {
            checkTransform.localPosition = new Vector3(x + 4, 0, z + i);
            if (!Physics.CheckSphere(checkTransform.position, 0.01f, floorMask))
            {
                generateGround(new Vector3(x + 4, 0, z + i));
            }
            checkTransform.localPosition = new Vector3(x - 4, 0, z + i);
            if (!Physics.CheckSphere(checkTransform.position, 0.01f, floorMask))
            {
                generateGround(new Vector3(x - 4, 0, z + i));
            }
        }
        for (int i = -3; i < 4; i++)
        {
            checkTransform.localPosition = new Vector3(x + i, 0, z + 4);
            if (!Physics.CheckSphere(checkTransform.position, 0.01f, floorMask))
            {
                generateGround(new Vector3(x + i, 0, z + 4));
            }
            checkTransform.localPosition = new Vector3(x + i, 0, z - 4);
            if (!Physics.CheckSphere(checkTransform.position, 0.01f, floorMask))
            {
                generateGround(new Vector3(x + i, 0, z - 4));
            }
        }

        if (jump_button_pressed)
        {
            Rigidbody.AddForce(jumppower * Vector3.up, ForceMode.VelocityChange);
            jump_button_pressed = false;
        }

        //Movement speed cap
        if (Rigidbody.velocity.magnitude < 5)
        {
            Rigidbody.AddForce(new Vector3(horizontal_movement * movement_speed, 0, vertical_movement * movement_speed));
        }
    }

    void OnDisable()
    {
        PlayerPrefs.SetInt("score", score);
        int highscore = PlayerPrefs.GetInt("highscore", 0);
        if (score > highscore)
        {
            PlayerPrefs.SetInt("highscore", score);
        }
    }
}
