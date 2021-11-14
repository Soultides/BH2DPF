using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyReflect : MonoBehaviour
{
    [Header("Private Components")]
    private Rigidbody2D bulletRB;
    private EdgeCollider2D aimCol;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer bulletSprite;

    [Header("Stats")]
    public float lengthOfAttack = 0.1f;
    public float swingForce = 5f;
    public float forceCap = 50f;
    public float reflectCoolDown = 2f;

    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        aimCol = GetComponent<EdgeCollider2D>();

    }

    //Alwyas collides with bulelts because collider is on when not reflecting

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("playerBullet"))
        {
            bulletRB = collision.gameObject.GetComponent<Rigidbody2D>();

            Vector2 colNormal = collision.contacts[0].normal;

            if (Mathf.Abs(bulletRB.velocity.x) < forceCap || Mathf.Abs(bulletRB.velocity.y) < forceCap)
            {

                bulletRB.AddForce(Vector2.Reflect(bulletRB.velocity * swingForce, colNormal.normalized));

            }
            else
            {

                bulletRB.AddForce(Vector2.Reflect(bulletRB.velocity, colNormal).normalized);

            }

            //Change bullet to friendly
            bulletSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            bulletSprite.color = new Color(255f, 0f, 0f, 1f);
            collision.gameObject.tag = "enemyBullet";

            aimCol.enabled = false;
            spriteRenderer.color = new Color(255f, 0f, 0f, 0.25f);
            StartCoroutine("ResetReflect");

        }
    }

    private IEnumerator ResetReflect()
    {
        yield return new WaitForSeconds(reflectCoolDown);
        spriteRenderer.color = new Color(255f, 0f, 0f, 0.5f);
        aimCol.enabled = true;
    }
}
