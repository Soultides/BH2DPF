using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ricochet : MonoBehaviour
{
    [Header("Private Components")]
    private Rigidbody2D rb;

    [Header("Stats")]
    public float ricochetForce = 10f;
    public float bounceCD = 1f;
    public bool canBounce = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //groundLayer is 8
        if (collision.gameObject.layer == 8 && canBounce)
        {
            canBounce = false;
            Debug.Log("Ricochet");
            Vector2 normal = collision.contacts[0].normal;
            Vector2 direction = Vector2.Reflect(rb.velocity, normal/2).normalized;

            rb.velocity = direction * ricochetForce;
            StartCoroutine("ResetBounceCD");
        }
    }

    IEnumerator ResetBounceCD()
    {
        yield return new WaitForSeconds(bounceCD);
        canBounce = true;
    }
}
