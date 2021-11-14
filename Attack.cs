using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //Code from Reflect, Ricochet Object | 2D Game in Unity 2019 Beginner Tutorial | Okay clone | Part 7 by VeryHotShark: https://www.youtube.com/watch?v=Vr-ojd4Y2a4&ab_channel=VeryHotShark

    [Header("Private Components")]
    private Rigidbody2D bulletRB;
    private EdgeCollider2D aimCol;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer bulletSprite;

    [Header("Stats")]
    public float lengthOfAttack = 0.1f;
    public float swingForce = 5f;
    public float forceCap = 50f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        aimCol = GetComponent<EdgeCollider2D>();
    }

    void Update()
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

            Vector2 colNormal = collision.contacts[0].normal;

            if (Mathf.Abs(bulletRB.velocity.x) < forceCap || Mathf.Abs(bulletRB.velocity.y) < forceCap)
            {

                bulletRB.AddForce(Vector2.Reflect(transform.position * swingForce, colNormal.normalized));

            }
            else
            {

                bulletRB.AddForce(Vector2.Reflect(transform.position, colNormal).normalized);

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
}
