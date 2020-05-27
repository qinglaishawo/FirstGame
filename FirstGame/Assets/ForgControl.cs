using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgControl : EnemyControl
{
    private Rigidbody2D rb;
    //private Animator anim;

    public Transform leftPositon;
    public Transform rightPosition;
    private float leftX;
    private float rightX;
    private bool faceLeft = true;

    public float speed;
    public float jumpForce;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        leftX = leftPositon.position.x;
        rightX = rightPosition.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (anim.GetBool("fall") || anim.GetBool("jump"))
        {
            if (faceLeft)
            {
                rb.velocity = new Vector2(-speed, jumpForce);
                if (transform.position.x < leftX)
                {
                    transform.transform.localScale = new Vector3(-1, 1, 1);
                    faceLeft = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(speed, jumpForce);
                if (transform.position.x > rightX)
                {
                    transform.transform.localScale = new Vector3(1, 1, 1);
                    faceLeft = true;
                }
            }
        }
    }

    void Fall()
    {
        anim.SetBool("jump",false);
        anim.SetBool("fall", true);
        jumpForce = -jumpForce;
    }

    void idle()
    {
        anim.SetBool("jump", false);
        anim.SetBool("fall", false);
        jumpForce = -jumpForce;
    }

    void jump()
    {
        anim.SetBool("jump", true);
    }
    private void FixedUpdate()
    {

    }
}
