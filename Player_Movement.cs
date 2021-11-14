using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player_Movement : MonoBehaviour
{
    private Transform _transform;

    [Header ("Stats")]
    [SerializeField]
    private float speed = 20f;
    [SerializeField]
    private float rotateSpeed = 10f;
    private bool _canDash = default;
    private bool _canMove = default;
    [SerializeField]
    private float coolDown = 2f;
    [SerializeField]
    private float dashLength = 10f;
    [SerializeField] 
    private float dashTime = 2f;
    private float _t = default;

    void Start()
    {

        _transform = GetComponent<Transform>();
        
        _canDash = true;
        _canMove = true;

    }


    void Update()
    {
        if (_canMove == true)
        {
            Movement(); 
        }

        Attack();
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        
        _transform.Rotate(0.0f, 0.0f, -x * rotateSpeed);
        _transform.position += _transform.right * y;
    }

    void Attack()
    {
        if (Input.GetButtonDown("Fire1") && _canDash == true)
        {
            _canMove = false;
            _t += Time.deltaTime / dashTime;
            _transform.position = Vector3.Lerp(_transform.position, transform.right * dashLength, _t);
            _canMove = true;
            _canDash = false;
            StartCoroutine("ResetDash");
            
        }
    }

    private IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(coolDown);
        _canDash = true;
    }

    /*
    private IEnumerator Dash()
    {
        yield return;
    }
    */
}
