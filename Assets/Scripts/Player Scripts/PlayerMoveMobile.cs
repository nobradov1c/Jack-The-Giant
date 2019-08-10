using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMobile : MonoBehaviour
{
    public float speed = 8f, maxVelocity = 4f;

    private Rigidbody2D myBody;
    private Animator animator;

    private bool moveLeft, moveRight;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft)
        {
            MoveLeft();
        } else if (moveRight)
        {
            MoveRight();
        }
    }

    public void SetMoveLeft(bool moveLeft)
    {
        this.moveLeft = moveLeft;
        this.moveRight = !moveLeft;

    }

    public void StopMoving()
    {
        moveLeft = moveRight = false;
        animator.SetBool("Walk", false);
    }

    void MoveLeft()
    {
        float forceX = 0f;
        float vel = Mathf.Abs(myBody.velocity.x);

        if (vel < maxVelocity)
            forceX = -speed;


        Vector3 tmp = transform.localScale;
        tmp.x = -1.3f;
        transform.localScale = tmp;
        animator.SetBool("Walk", true);
        myBody.AddForce(new Vector2(forceX, 0));
    }

    void MoveRight()
    {
        float forceX = 0f;
        float vel = Mathf.Abs(myBody.velocity.x);

        if (vel < maxVelocity)
            forceX = speed;

        Vector3 tmp = transform.localScale;
        tmp.x = 1.3f;
        transform.localScale = tmp;
        animator.SetBool("Walk", true);

        myBody.AddForce(new Vector2(forceX, 0));
    }
}
