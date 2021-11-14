using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicBullet : MonoBehaviour
{
    [Header("Private Components")]
    private Rigidbody2D rb;
    private GameObject target;

    [Header("Stats")]
    public float speed = 20f;
    public float damage = 1f;
    public float bounces = 10f;

    //Make the bullets gradually pick up speed
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");

        AimAtPlayer();
        rb.AddForce(transform.up * speed);

    }

    void Update()
    {
        if (bounces <= 0)
        {

            Debug.Log("Bullet Destroy");
            //Destroy(gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerHurtBox") && gameObject.CompareTag("enemyBullet"))
        {

            collision.gameObject.GetComponent<Health>().TakeDamage(damage);
            Destroy(gameObject);

        }
        if (collision.gameObject.CompareTag("EnemyHurtBox") && gameObject.CompareTag("playerBullet"))
        {

            collision.gameObject.GetComponent<enemyHealth>().TakeDamage(damage);
            Destroy(gameObject);

        }

        if (collision.gameObject.layer == 8)
        {
            bounces -= 1;
        }
    }

    void AimAtPlayer()
    {

        Vector3 aimDirection = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(angle, 0, 0);
    }
}
