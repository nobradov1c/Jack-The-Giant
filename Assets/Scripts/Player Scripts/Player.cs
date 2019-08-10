using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 8f, maxVelocity = 4f;

    private Rigidbody2D myBody;
    private Animator animator;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    // FixedUpdate is called every couple of frames, best used for physics calculations 
    void FixedUpdate()
    {
        PlayerMoveKeyboard();
    }

    void PlayerMoveKeyboard()
    {
        float forceX = 0f;
        float vel = Mathf.Abs(myBody.velocity.x);

        float h = Input.GetAxisRaw("Horizontal");

        if(h > 0)
        {
            if (vel < maxVelocity)
                forceX = speed;

            Vector3 tmp = transform.localScale;
            tmp.x = 1.3f;
            transform.localScale = tmp;
            animator.SetBool("Walk", true);
        } else if(h < 0)
        {
            if (vel < maxVelocity)
                forceX = -speed;

            Vector3 tmp = transform.localScale;
            tmp.x = -1.3f;
            transform.localScale = tmp;
            animator.SetBool("Walk", true);
        } else animator.SetBool("Walk", false);

        myBody.AddForce(new Vector2(forceX, 0));
    }
}
