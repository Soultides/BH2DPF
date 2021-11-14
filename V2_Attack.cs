using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class V2_Attack : MonoBehaviour
{
//Code from Reflect, Ricochet Object | 2D Game in Unity 2019 Beginner Tutorial | Okay clone | Part 7 by VeryHotShark: https://www.youtube.com/watch?v=Vr-ojd4Y2a4&ab_channel=VeryHotShark

    [Header("Private Components")] private Rigidbody2D rb;
    private Rigidbody2D bulletRB;
    private EdgeCollider2D aimCol;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer bulletSprite;

    [Header("Stats")]
    public float lengthOfAttack = 0.1f;
    public float swingForce = 5f;

    private void Awake()
    {

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        aimCol = GetComponent<EdgeCollider2D>();
    }

    void Update()
    {
        
        Attack();

    }

    private void Attack()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            //Debug.Log("Detect");
            aimCol.enabled = true;
            spriteRenderer.color = new Color(255f, 0f, 0f, 0.5f);
            StartCoroutine("SetCollisionFalse");

        }
        
    }
    private IEnumerator SetCollisionFalse()
    {

        yield return new WaitForSeconds(lengthOfAttack);
        spriteRenderer.color = new Color(0f, 0f, 255f, 0.5f);
        aimCol.enabled = false;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("enemyBullet") || collision.gameObject.CompareTag("playerBullet"))
        {
            //Debug.Log("Deflect");
            bulletRB = collision.gameObject.GetComponent<Rigidbody2D>();
            
            //If moving with the bullet, push
            if (Vector2.Dot(rb.velocity.normalized, bulletRB.velocity.normalized) == 1)
            {
                //Push(Rigidbody2D bulletRB);
                bulletRB.velocity = new Vector2(bulletRB.velocity.x * swingForce, bulletRB.velocity.y * swingForce);

            }
            
            //If not moving with the bullet, relfect
            if (Vector2.Dot(rb.velocity.normalized, bulletRB.velocity.normalized) == -1)
            {
                //Reflect(Rigidbody2D bulletRB);
                bulletRB.velocity = new Vector2(bulletRB.velocity.x * -swingForce, bulletRB.velocity.y * -swingForce);
                
            }
            //In case of standing still or any case not considered, reflect
            else
            {
                //Reflect(Rigidbody2D bulletRB);
                bulletRB.velocity = new Vector2(bulletRB.velocity.x * -swingForce, bulletRB.velocity.y * -swingForce);

            }
            

            //Change bullet to friendly
            bulletSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            if (collision.gameObject.CompareTag("enemyBullet"))
            {

                //Debug.Log("Color change");
                bulletSprite.color = new Color(0f, 255f, 0f, 1f);
                collision.gameObject.tag = "playerBullet";

            }
            
        }
    }
    
    /*
    void Push(Rigidbody2D bulletRB)
    {
        
        bulletRB.velocity = new Vector2(bulletRB.velocity.x * swingForce, bulletRB.velocity.y * swingForce);
        
    }

    void Reflect(Rigidbody2D bulletRB)
    {
        
        bulletRB.velocity = new Vector2(bulletRB.velocity.x * -swingForce, bulletRB.velocity.y * -swingForce);
        
    }
    */
}
