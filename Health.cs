using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    [Header("Private Components")]
    private BoxCollider2D playerHurtBox;
    private SpriteRenderer spriteRenderer;

    [Header("Stats")]
    public float health = 20f;
    public float maxHealth = 20f;
    public float iframes = 0.5f;
    public bool canTakeDamage = true;

    private void Start()
    {

        playerHurtBox = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;

    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        
        if (canTakeDamage == true)
        {

            health -= damage;

            spriteRenderer.color = new Color(255f, 0f, 0f, 1f);
            StartCoroutine("ResetColor");
            canTakeDamage = false;
        }
        

    }

    private void Die()
    {

        Debug.Log("Dead");

    }

    private IEnumerator ResetColor()
    {

        yield return new WaitForSeconds(iframes);
        canTakeDamage = true;
        float colorMultiplier = health / maxHealth;
        spriteRenderer.color = new Color(255f, 255f * colorMultiplier, 255f * colorMultiplier, 1f);

    }
}
