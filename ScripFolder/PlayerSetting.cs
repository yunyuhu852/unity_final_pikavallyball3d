using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;//for showing Text(coins)
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]//make transition between idle and run
public class PlayerSetting : MonoBehaviour
{
    //control setting
    public bool P1;
    public bool P2;
    public Vector3 startPosition;
    public Vector3 jumpDirection;
    public float movingSpeed;
    public float weight;
    public Vector3 forceDirection;
    public float forceAmount;
    bool floor = true;


    //rules: scores
    int scores;
    bool serve;


    //anmiation setting 
    Animator animator;
    bool run;
    bool jump;
    bool inair;
    bool hit;

    //audio setting
    public AudioSource hitBallSound;
    public AudioSource runningSound;

    //private properties
    private Rigidbody playerRb;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerRb.mass = weight;
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movementDir = new Vector3(horizontalInput, 0, verticalInput);

        if (P1)
        {
            //WASD, space+ Z
            if (Input.GetKey(KeyCode.W))
            {

                run = true;
                transform.forward = new Vector3(1, 0, 0);
                transform.localPosition += transform.forward * Time.deltaTime*movingSpeed;
            }
            if (Input.GetKey(KeyCode.A))
            {

                run = true;
                transform.forward = new Vector3(0, 0, 1);
                transform.localPosition += transform.forward * Time.deltaTime * movingSpeed;
            }
            if (Input.GetKey(KeyCode.S))
            {

                run = true;
                transform.forward = new Vector3(-1, 0, 0);
                transform.localPosition += transform.forward * Time.deltaTime * movingSpeed;
            }
            if (Input.GetKey(KeyCode.D))
            {

                run = true;
                transform.forward = new Vector3(0, 0, -1);
                transform.localPosition += transform.forward * Time.deltaTime * movingSpeed;
            }
            //jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (floor)
                {
                    playerRb.AddForce(forceDirection * forceAmount);
                    jump = true;
                    inair = true;

                    floor = false;
                }
            }
            //hit
            if (Input.GetKey(KeyCode.Z))
            {

            }

        }//p1
        else if (P2)
        {
            // ; 上下左右 L + enter
            //WASD, space+ Z
            if (Input.GetKey(KeyCode.UpArrow))
            {
                run = true;
                transform.forward = new Vector3(-1, 0, 0);
                transform.localPosition += transform.forward * Time.deltaTime * movingSpeed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                run = true;
                transform.forward = new Vector3(1, 0, 0);
                transform.localPosition += transform.forward * Time.deltaTime * movingSpeed;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                run = true;
                transform.forward = new Vector3(0, 0, -1);
                transform.localPosition += transform.forward * Time.deltaTime * movingSpeed;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                run = true;
                transform.forward = new Vector3(0, 0, 1);
                transform.localPosition += transform.forward * Time.deltaTime * movingSpeed;
            }
            //jump
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (floor)
                {
                    playerRb.AddForce(forceDirection * forceAmount);
                    jump = true;

                    inair = true;
                    floor = false;
                }
            }
            //hit
            if (Input.GetKey(KeyCode.L))
            {

            }
        }

        if (playerRb.velocity.y < 0)
        {
            playerRb.velocity += -Vector3.up *weight*0.01f;
        }

        animator.SetBool("Run", run);
        animator.SetBool("Jump", jump);

        animator.SetBool("InAir", inair);
        animator.SetBool("Hit", hit);
        run = false;
        jump = false;

    }

    public void OnCollisionEnter(Collision collision)
    {
            Debug.Log("collision detected");
        if (collision.gameObject.tag == "Floor")
        {
            floor = true;
            jump = false;
            inair = false;
        }
    }
}
