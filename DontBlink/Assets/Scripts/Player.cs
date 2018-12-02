using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{

    private Rigidbody2D myRigidBody;

    [SerializeField]
    private float movSpeed;

    [SerializeField]
    private float blinkSpeed;


    [SerializeField]
    private bool isReady = false;

    private string axisX;

    private string axisY;

    private Animator anim;

    private bool playerMoving;

    public bool playerBlinking;

    private Vector2 lastMove;

    private Vector2 lastBlink;

    private Vector2 WorldMousePos;

    private Vector2 direction;

    private bool isHit;

    public int coolDown;

    public int hitCount;

    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1f;
        coolDown = 0;
        hitCount = 0;
        anim = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.tag == "Player")
        {

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetMouseButtonDown(0))
            {
                HandleInput();
            }

            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.RightArrow))
            {
                playerMoving = false;
                anim.SetBool("PlayerMoving", playerMoving);

            }
            if (Input.GetMouseButtonUp(0))
            {
                playerBlinking = false;
                anim.SetBool("PlayerBlinking", playerBlinking);
            }

            if(coolDown == 0){
                isReady = true;
                CancelInvoke("ResetCoolDown");
            }
        }

        if (this.gameObject.tag == "Nazi")
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                HandleInput();
            }

            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                playerMoving = false;
                anim.SetBool("PlayerMoving", playerMoving);

            }
        }
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if(isReady == true)
            {
                HandleBlink();
            }
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            HandleMovement();
        }

    }

    private void HandleMovement()
    {

        if (this.gameObject.tag == "Player")
        {
            axisX = "Horizontal";
            axisY = "Vertical";
        }
        else if (this.gameObject.tag == "Nazi")
        {
            axisX = "Horizontal1";
            axisY = "Vertical1";
        }

        if (Input.GetAxisRaw(axisX) > 0.5f || Input.GetAxisRaw(axisX) < -0.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw(axisX) * movSpeed * Time.deltaTime, 0f, 0f));
            playerMoving = true;
            lastMove = new Vector2(Input.GetAxisRaw(axisX), 0f);
        }

        if (Input.GetAxisRaw(axisY) > 0.5f || Input.GetAxisRaw(axisY) < -0.5f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw(axisY) * movSpeed * Time.deltaTime, 0f));
            playerMoving = true;
            lastMove = new Vector2(0f, Input.GetAxisRaw(axisY));
        }

        anim.SetFloat("MoveX", Input.GetAxisRaw(axisX));
        anim.SetFloat("MoveY", Input.GetAxisRaw(axisY));
        anim.SetBool("PlayerMoving", playerMoving);
        anim.SetFloat("LastMoveX", lastMove.x);
        anim.SetFloat("LastMoveY", lastMove.y);

    }

    private void HandleBlink()
    {

        playerBlinking = true;
        WorldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        direction = new Vector2(WorldMousePos.x - transform.position.x, WorldMousePos.y - transform.position.y);
        myRigidBody.AddForce(direction.normalized * blinkSpeed, ForceMode2D.Impulse);

        anim.SetFloat("LastMoveX", direction.x);
        anim.SetFloat("LastMoveY", direction.y);
        anim.SetBool("PlayerBlinking", playerBlinking);

        Invoke("ResetVelocity", 0.2f);
        coolDown += 3;
        isReady = false;
        InvokeRepeating("ResetCoolDown", 0, 1.0f);
        
    }
   
    private void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag.Equals("Player") == true)
        {
            if (Math.Sqrt (Math.Pow(this.myRigidBody.velocity.x, 2) + Math.Pow(this.myRigidBody.velocity.y, 2)) > 4)
            {

                isHit = true;
                hitCount += 1;
                anim.SetBool("IsHit", isHit);
                Invoke("ResetBool", 0.2f);
            }
        }
    }

    private void ResetCoolDown()
    {
        if (coolDown == 0)
        {
           
        }
        else
        {
            coolDown -= 1;
        }
    }

    private void ResetBool()
    {

        if (this.gameObject.tag == "Nazi")
        {
            isHit = false;
            anim.SetBool("IsHit", isHit);
        }
    }
    private void ResetVelocity()
    {
        myRigidBody.velocity = Vector2.zero;
    }

    private float GetMovSpeed()
    {
        return this.movSpeed;
    }
}
