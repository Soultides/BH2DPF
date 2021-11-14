using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Code from Celeste's Movement by Mix and Jam: https://www.youtube.com/watch?v=STyY26a_dPY&ab_channel=MixandJam

    [Header("Private Components")]
    private WallCheck coll;
    private Rigidbody2D rb;
    private BoxCollider2D playerHurtBox;

    [Header("Stats")]
    public float speed = 10f;
    public float slideSpeed = 3f;

    [Header("Booleans")]
    public bool wallGrab;
    public bool wallSlide;

    void Start()
    {

        coll = GetComponent<WallCheck>();
        rb = GetComponent<Rigidbody2D>();
        playerHurtBox = GameObject.FindGameObjectWithTag("PlayerHurtBox").GetComponent<BoxCollider2D>();

    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);

        if (coll.onWall && Input.GetButton("Fire3"))
        {
            wallGrab = true;
            wallSlide = false;
        }

        //gravityScale is not set back after wall grabbing
        if (wallGrab)
        {
            rb.gravityScale = 0;
            if (x > .2f || x < -.2f)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }

            float speedModifier = y > 0 ? .5f : 1;

            rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
        }

        if (coll.onWall && !coll.onGround)
        {
            if (x != 0 && !wallGrab)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
        {
            wallSlide = false;
        }

    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }

    private void WallSlide()
    {

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }
        float push = pushingWall ? 0 : rb.velocity.x;

        rb.velocity = new Vector2(push, -slideSpeed);
    }

}
