using System;
using UnityEngine;

public class Slingshot:MonoBehaviour
{
    private Vector3 _originalPosition;
    private Vector3 _draggedPosition;
    private Rigidbody2D _rigidbody2D;
    public CharacterController2D controller;

    [SerializeField]
    private int shootForce;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void OnMouseDown()
        {
            if (Camera.main)
            {
                _originalPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            Debug.Log("original:" + _originalPosition);
        }

        private void OnMouseDrag()
        {
            if (Camera.main)
            {
                _draggedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            Debug.Log("dragged:" + _draggedPosition);
        }

        private void OnMouseUp()
        {
            var shootVector = _originalPosition - _draggedPosition;
            controller.Move(shootVector * shootForce * 10);
        }
}