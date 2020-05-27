using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FinalMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;

    public float speed, jumpForce;
    public Transform groundCheck;
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

        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
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
            jumpCount = 2;
            isJump = false;
        }
        if (jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--;
            jumpPressed = false;
        }
        if (!isGround)
        {
            jumpPressed = false;
        }

    }

    void SwitchAnim()
    {
        if (isJump)
        {
            anim.SetBool("jumping", true);
            anim.SetBool("falling", false);
        }
        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        if (anim.GetBool("falling"))
        {
            if (isGround)
            {
                anim.SetBool("idle", true);
                anim.SetBool("falling", false);
            }
        }
        else if (isHurt)
        {
            
            anim.SetBool("hurting", true);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                anim.SetBool("hurting", false);
                anim.SetBool("idle", true);
                anim.SetFloat("running", 0);
            }
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
                }
                if (collision.name.Contains("gem"))
                {
                    gemCount++;
                    gemText.text = gemCount.ToString();
                }
                preCollectionName = collision.name;
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyControl ec = collision.gameObject.GetComponent<EnemyControl>();
            if (rb.velocity.y<0)
            {            
                anim.SetBool("jumping", true);
                anim.SetBool("falling", false);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                //Destroy(collision.gameObject);
                ec.JumpOn();              
            }
            else if (transform.position.x < collision.transform.position.x)
            {
                isHurt = true;
                rb.velocity = new Vector2(-5f, rb.velocity.y);
            }
            else if (transform.position.x > collision.transform.position.x)
            {
                isHurt = true;
                rb.velocity = new Vector2(5f, rb.velocity.y);
            }
        }
    }
}
