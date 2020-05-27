using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleControl : EnemyControl
{
    private Rigidbody2D rb;
    //private Animator anim;

    public Transform upPositon;
    public Transform downPosition;
    private float upY;
    private float downY;

    private bool up = true;
    public float speed;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        upY = upPositon.position.y;
        downY = downPosition.position.y;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (up)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (transform.position.y > upY)
            {
                up = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y < downY)
            {
                up = true;
            }
        }
    }
}
