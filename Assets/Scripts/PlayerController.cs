using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float forwardInput;
    private float rightInput;
    public GameObject Player;
    public bool isSprint = false;

    private Animator anim;
    private Rigidbody rb;
    private float animForward = 0.5f, animRight = 0.5f;
    private bool battingEnabled = false;
    private bool battingShot = false;
    private bool isGrounded = false;

    public Transform cameraSocket;
    public float WalkSpeed = 2f;
    public float RunSpeed = 4f;
    private float speed;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
        speed = WalkSpeed;
        //WalkSpeed = isSprint ? RunSpeed : WalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        AddMovementInput();
        running();
        battingMode();
        setGrounded();
        jump();
        if (isGrounded && !battingShot)
        {
            transform.Translate(new Vector3(rightInput * Time.deltaTime * speed, 0, forwardInput * Time.deltaTime * speed));
            anim.SetFloat("Forward", forwardInput * animForward);
            anim.SetFloat("Right", rightInput * animRight);
        }

    }

    public void AddMovementInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        rightInput = Input.GetAxis("Horizontal");
        if(forwardInput!=0 || rightInput != 0)
        {
            if (battingEnabled)
            {
                battingEnabled = false;
            }
        }
    }

    public void setGrounded()
    {
        //Debug.DrawRay(Player.transform.position + new Vector3(0,1f,0), Vector3.up * -1.1f, Color.red);
        isGrounded = Physics.Raycast(Player.transform.position + new Vector3(0, 1f, 0), Vector3.down, 1.3f);
    }
    public void running()
    {
        if (Input.GetKey(KeyCode.LeftShift) || isSprint)
        {
            animForward = Mathf.Lerp(animForward, 1f, Time.deltaTime * 5f);
            animRight = Mathf.Lerp(animRight, 1f, Time.deltaTime * 5f);
            speed = RunSpeed;
        }
        else
        {
            animForward = Mathf.Lerp(animForward, 0.5f, Time.deltaTime * 5f);
            animRight = Mathf.Lerp(animRight, 0.5f, Time.deltaTime * 5f);
            speed = WalkSpeed;
        }
    }

    public void battingMode()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            battingShot = true;
            StartCoroutine(finishiBattingShot());
        }
        anim.SetBool("BatShot", battingShot);
        Debug.Log("batshot");
    }

    public void jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
        }
        anim.SetBool("Flying", !isGrounded);
    }
    IEnumerator finishiBattingShot()
    {
        yield return new WaitForSeconds(0.54f);
        battingShot = false;
    }
}
