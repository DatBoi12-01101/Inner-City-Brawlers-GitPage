using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
public class PlayerMovement : MonoBehaviour
{

    public GameObject groundCheck;
    public bool isGrounded;
   
    public float jumpStrength;
    public float movementSpeed;

    public enum playerState{Grounded, Crouch, Jump, Block, SoftKnockdown, HardKnockdown , Immobile};

    
    public playerState currentPlayState;
    public Rigidbody2D myRB2D;

    //Rewired
    [SerializeField] public int playerID;
    [SerializeField] public Player player;
    [SerializeField] public float moveHorizontal;
    [SerializeField] public  float moveUp;

    public bool isDisabled;


    // Start is called before the first frame update
    void Start()
    {
        

        myRB2D = GetComponent<Rigidbody2D>();
        player = ReInput.players.GetPlayer(playerID);
        if (playerID == 0)
        {
            SetControllerMapsForCurrentModeP1();
        }

        if (playerID == 1)
        {
            SetControllerMapsForCurrentModeP2();
        }
    }

    public void SetControllerMapsForCurrentModeP1()
    {
        player.controllers.maps.LoadMap(ControllerType.Keyboard, playerID, "Default", "Player1", true);
        player.controllers.maps.LoadMap(ControllerType.Joystick, playerID, "Default", "Player1", true);
    }

    public void SetControllerMapsForCurrentModeP2()
    {
        player.controllers.maps.LoadMap(ControllerType.Keyboard, playerID, "Default", "Player2", true);
        player.controllers.maps.LoadMap(ControllerType.Joystick, playerID, "Default", "Player2", true);
    }

    // Update is called once per frame
    public void Update()
    {
        if(isDisabled != true)
        {
            playerMovement();
        }
    }

    public void playerMovement()
    {
        moveHorizontal = player.GetAxis("Move Horizontal");
        moveUp = player.GetAxis("Move Vertical");

        if (currentPlayState == playerState.Grounded)
        {
            if (-moveUp <= 1 && -moveUp > 0.2)
            {
                currentPlayState = playerState.Crouch;
            }

            if  (moveUp <= 1 && moveUp > 0.2)
            {
                JumpFunction(0.0012f);
                if ((moveHorizontal <= 1 && moveHorizontal > 0.5) && Mathf.Round(myRB2D.velocity.x) <= 6)
                {
                    myRB2D.AddForce(new Vector2(movementSpeed/2, 0), ForceMode2D.Impulse);
                }
                if ((-moveHorizontal <= 1 && -moveHorizontal > 0.5) && Mathf.Abs(myRB2D.velocity.x) <= 6)
                {
                    myRB2D.AddForce(new Vector2(-movementSpeed/2, 0), ForceMode2D.Impulse);
                }
            }

            if ((moveHorizontal <= 1 && moveHorizontal > 0.5) && Mathf.Round(myRB2D.velocity.x) <= 6)
            {
                myRB2D.AddForce(new Vector2(movementSpeed, 0), ForceMode2D.Impulse);
                if (moveUp <= 1 && moveUp > 0.2)
                {
                    JumpFunction(0.0012f);

                }
                if (-moveUp <= 1 && -moveUp > 0.2)
                {
                    currentPlayState = playerState.Crouch;
                    myRB2D.AddForce(new Vector2(0, 0), ForceMode2D.Impulse);

                }
            }
            if ((-moveHorizontal <= 1 && -moveHorizontal > 0.5) && Mathf.Abs(myRB2D.velocity.x) <= 6)
            {
                myRB2D.AddForce(new Vector2(-movementSpeed, 0), ForceMode2D.Impulse);
                if (moveUp <= 1 && moveUp > 0.2)
                {
                    JumpFunction(0.0012f);
                    Debug.Log("Current Player State: " + currentPlayState);
                }
                if (-moveUp <= 0.7 && -moveUp > 0.2)
                {
                    currentPlayState = playerState.Crouch;
                    myRB2D.AddForce(new Vector2(0, 0), ForceMode2D.Impulse);
                    Debug.Log("Current Player State: " + currentPlayState);
                }
                Debug.Log("LeftWalk");
            }
            else 
            {
                currentPlayState = playerState.Grounded;
                Debug.Log("Current Player State: " + currentPlayState);
            }
        }
        if (currentPlayState == playerState.Crouch)
        {
            if (-moveUp <= 1 && -moveUp > 0.2)
            {
                if (moveHorizontal <= 0.5 && moveHorizontal > 0)
                {
                    myRB2D.AddForce(new Vector2(0, 0), ForceMode2D.Impulse);
                    currentPlayState = playerState.Crouch;
                    Debug.Log("Current Player State: " + currentPlayState + " Right");
                }
                if (-moveHorizontal <= 0.5 && -moveHorizontal > 0)
                {
                    myRB2D.AddForce(new Vector2(0, 0), ForceMode2D.Impulse);
                    currentPlayState = playerState.Crouch;
                    Debug.Log("Current Player State: " + currentPlayState + " Left");
                }
            }
            else
            {
                currentPlayState = playerState.Grounded;
                Debug.Log("Current Player State: " + currentPlayState);
            }
        }
    }
    
    public void JumpFunction(float jumpLimiter)
    {
        if (isGrounded == true && Mathf.Abs(myRB2D.velocity.y) <= jumpLimiter)
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
