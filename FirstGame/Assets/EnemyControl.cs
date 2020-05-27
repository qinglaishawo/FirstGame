using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    protected Animator anim;
    
    // Start is called before the first frame update
    protected virtual  void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpOn()
    {
        anim.SetTrigger("death");
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
