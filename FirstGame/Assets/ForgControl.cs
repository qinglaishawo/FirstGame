using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForgControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    public Transform leftPositon;
    public Transform rightPosition;
    private float leftX;
    private float rightX;
    private bool faceLeft=true;
    private bool rest;
    private float cumulativeTime;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        leftX = leftPositon.position.x;
        rightX = rightPosition.position.x;
        rest = true;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (!rest)
        {
            if (faceLeft)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
                if (transform.position.x < leftX)
                {
                    transform.transform.localScale = new Vector3(-1, 1, 1);
                    faceLeft = false;
                }
            }
            else
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
                if (transform.position.x > rightX)
                {
                    transform.transform.localScale = new Vector3(1, 1, 1);
                    faceLeft = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        cumulativeTime = cumulativeTime + Time.deltaTime;
        if (rest)
        {
            if (cumulativeTime >=1)
            {
                cumulativeTime = 0;
                rest = false;
                anim.SetBool("idle",false);
            }
        }
        else
        {
            if (cumulativeTime >= 0.25f)
            {
                cumulativeTime = 0;
                rest = true;
                anim.SetBool("idle", true);
            }
        }
    }
}
