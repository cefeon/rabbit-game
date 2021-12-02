using System;
using UnityEngine;

public class Slingshot:MonoBehaviour
{
    private Vector3 _originalPosition;
    private Vector3 _draggedPosition;
    private Rigidbody2D _rigidbody2D;
    public CharacterController2D controller;

    [SerializeField]
    private int _shootForce;
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    
    private void OnMouseDown()
        {
            try
            {
                _originalPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            catch (NullReferenceException error)
            {
                Debug.Log("No camera set " + error);
            }

            Debug.Log("original:" + _originalPosition);
        }

        private void OnMouseDrag()
        {
            try
            {
                _draggedPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            } 
            catch (NullReferenceException error)
            {
                Debug.Log("No camera set" + error);
            }

            Debug.Log("dragged:" + _draggedPosition);
        }

        private void OnMouseUp()
        {
            var shootVector = _originalPosition - _draggedPosition;
            controller.Move(shootVector * _shootForce * 10);
        }
}