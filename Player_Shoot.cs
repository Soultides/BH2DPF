using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shoot : MonoBehaviour
{
    
    [SerializeField] private GameObject projectile;
    private float fireRate = default;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetButtonDown(("Fire1")))
        {
            
            
            
        }
    }
    
}
