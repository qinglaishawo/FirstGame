using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    protected Animator anim;
    protected AudioSource audioSource;
    protected Rigidbody2D rb;
    protected Collider2D bc;
    // Start is called before the first frame update
    protected virtual  void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpOn()
    {
        anim.SetTrigger("death");
        audioSource.Play();
        bc.enabled = false;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
