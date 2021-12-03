using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBolt : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    public float speed;
    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        _rigidbody2D.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D thingHitted)
    {
        Debug.Log(thingHitted.name);
        Destroy(gameObject);
    }
}
