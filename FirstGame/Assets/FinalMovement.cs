﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class FinalMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    public float speed, jumpForce;
    public Transform groundCheck;
    public Transform topCheck;
    public LayerMask ground;

    public bool isGround, isJump;

    bool jumpPressed;
    int jumpCount;

    public Text cherryText;
    public Text gemText;
    public int cherryCount;
    public int gemCount;
    private string preCollectionName;
    private bool isHurt;

    public AudioSource jumpAudio, hitAudio, cherryAudio, gemAudio;
    public GameObject dialog1;
    private bool isCrouch;
    public Collider2D topBox;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHurt)
        {
            if (Input.GetButtonDown("Jump") && jumpCount > 0)
            {
                jumpPressed = true;
            }
            Crouch();
        }


    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.15f, ground);
        if (!isHurt)
        {
            GroundMovement();
            
        }
        Jump();
        SwitchAnim();
        
    }

    void GroundMovement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);

        if (horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
        anim.SetFloat("running", Mathf.Abs(horizontalMove));
    }

    void Jump()
    {
        if (isGround)
        {
            jumpCount = 1;
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
            jumpAudio.Play();
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
            jumpAudio.Play();
        }
        if (!isGround)
        {
            jumpPressed = false;
        }

    }

    void SwitchAnim()
    {
        if (isJump && !isHurt)
        {
           
            anim.SetBool("jumping", true);
            anim.SetBool("falling", false);
        }
        if (anim.GetBool("jumping") && !isHurt)
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        if (anim.GetBool("falling") && !isHurt)
        {
            if (isGround)
            {
                anim.SetBool("idle", true);
                anim.SetBool("falling", false);
            }
        }
        else if (isHurt)
        {
            print("213123123");
            anim.SetBool("hurting", true);
            anim.SetBool("jumping", false);
            anim.SetBool("falling", false);
            /*if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                anim.SetBool("hurting", false);
                anim.SetBool("idle", true);
                anim.SetFloat("running", 0);
                GetComponent<CircleCollider2D>().sharedMaterial.friction = 0.02f;
            }*/
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Collection")
        {
            if (preCollectionName != collision.name)
            {
                if (collision.name.Contains("Cherry"))
                {
                    cherryCount++;
                    cherryText.text = cherryCount.ToString();
                    cherryAudio.Play();
                }
                if (collision.name.Contains("gem"))
                {
                    gemCount++;
                    gemText.text = gemCount.ToString();
                    gemAudio.Play();
                }
                preCollectionName = collision.name;
                Destroy(collision.gameObject);
            }
        }
        if(collision.tag == "LevelCollider")
        {
            dialog1.SetActive(true);
        }
        if (collision.tag == "restart")
        {
            Invoke("Restart",1f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        dialog1.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyControl ec = collision.gameObject.GetComponent<EnemyControl>();
            //print("transform.position="+ transform.position.x);
            //print("collision.position=" + collision.transform.position.x);
            if (rb.velocity.y<0&&groundCheck.position.y> collision.transform.position.y)
            {            
                anim.SetBool("jumping", true);
                anim.SetBool("falling", false);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                //Destroy(collision.gameObject);
                //jumpAudio.Play();
                ec.JumpOn();              
            }
            else if (transform.position.x <=collision.transform.position.x)
            {
                hitAudio.Play();
                //GetComponent<CircleCollider2D>().sharedMaterial.friction = 10f;
                isHurt = true;
                rb.velocity = new Vector2(-5f, 0);
                //print("1111111111111");
            }
            else if (transform.position.x > collision.transform.position.x)
            {
                hitAudio.Play();
                //GetComponent<CircleCollider2D>().sharedMaterial.friction = 10f;
                isHurt = true;
                rb.velocity = new Vector2(5f, 0);
                //print("2222222222222");
            }
        }
    }

    void stopHurt()
    {
        isHurt = false;
        anim.SetBool("hurting", false);
        anim.SetBool("idle", true);
        anim.SetFloat("running", 0);
        GetComponent<CircleCollider2D>().sharedMaterial.friction = 0.02f;
        rb.velocity = new Vector2(0,0);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Crouch()
    {
        if (!Physics2D.OverlapCircle(topCheck.position, 0.1f, ground))
        {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("Crouching",true);
                topBox.enabled = false;
            }
            else
            {
                anim.SetBool("Crouching", false);
                topBox.enabled = true;
            }
        }
    }
}
