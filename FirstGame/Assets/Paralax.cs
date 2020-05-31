using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public Transform cam;
    public float moveRate;
    public bool isLockY;
    private float startPositionX, startPositionY;
    // Start is called before the first frame update
    void Start()
    {
        startPositionX = transform.position.x;
        startPositionY= transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLockY)
        {
            this.transform.position = new Vector2(startPositionX+cam.position.x*moveRate,transform.position.y);
        }
        else
        {
            this.transform.position = new Vector2(startPositionX + cam.position.x * moveRate, startPositionY + cam.position.y * moveRate);
        }
    }
}
