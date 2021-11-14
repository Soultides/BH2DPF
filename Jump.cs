using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    //Code from Better Jumping in Unity With Four Lines of Code by Board To Bits Games: https://www.youtube.com/watch?v=7KiK0Aqtmzc&ab_channel=BoardToBitsGames

    [Header("Private Components")]
    private WallCheck coll;
    private Rigidbody2D rb;

    [Header("Stats")]
    public float jumpVelocity = 10f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    void Start()
    {
        coll = GetComponent<WallCheck>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && coll.onGround)
        {
            jumpFunction();
        }

        jumpCheck();

        /*
        if (!coll.onGround && !coll.onWall)
        {

            rb.gravityScale = 1;
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }
        */

    }

    private void jumpFunction()
    {
        rb.velocity = Vector2.up * jumpVelocity;
    }

    private void jumpCheck()
    {
        //increases the gravity on the player's rigidbody as they fall
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
