using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementKeyBoard : MonoBehaviour
{
    public GameObject groundCheck;
    public bool isP1;
    public bool isP2;
    public bool isGrounded;

    public float jumpStrength;
    public float movementSpeed;

    public enum playerState {Grounded, Crouch, Jump, Block, SoftKnockdown, HardKnockdown};

    public Rigidbody2D myRB2D;
    playerState currentPlayState;

    // Start is called before the first frame update
    void Start()
    {
        myRB2D = GetComponent<Rigidbody2D>();


        if (isP1 == true)
        {
            currentPlayState = playerState.Grounded;
        }
        if (isP2 == true)
        {
            currentPlayState = playerState.Grounded;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isP1 == true)
        {
            P1Movement();
        }
        if (isP2 == true)
        {
            P2Movement();
        }
    }

    public void P1Movement()
    {
        if (currentPlayState == playerState.Grounded)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                JumpFunction();
                Debug.Log("Jump " + currentPlayState);
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                currentPlayState = playerState.Crouch;
                Debug.Log("Crouch2 " + currentPlayState);
            }
            if (Input.GetKey(KeyCode.D))
            {
                myRB2D.AddForce(new Vector2(movementSpeed, 0), ForceMode2D.Force);
                Debug.Log("RightWalk");
            }
            if (Input.GetKey(KeyCode.A))
            {
                myRB2D.AddForce(new Vector2(-movementSpeed, 0), ForceMode2D.Force);
                Debug.Log("LeftWalk");
            }
        }
    }

    public void P2Movement()
    {
        if (currentPlayState == playerState.Grounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                JumpFunction();
                Debug.Log("Jump " + currentPlayState);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentPlayState = playerState.Crouch;
                Debug.Log("Crouch2 " + currentPlayState);

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                myRB2D.AddForce(new Vector2(movementSpeed, 0), ForceMode2D.Force);
                Debug.Log("RightWalk2");
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                myRB2D.AddForce(new Vector2(-movementSpeed, 0), ForceMode2D.Force);
                Debug.Log("LeftWalk2");
            }
        }
    }

    public void JumpFunction()
    {
        if (isGrounded == true)
        {
            myRB2D.AddForce(new Vector2(0, jumpStrength), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            isGrounded = true;
            currentPlayState = playerState.Grounded;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            isGrounded = false;
            currentPlayState = playerState.Jump;
        }
    }
}
