using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    //Component references
    Rigidbody2D _rb;
    SpriteRenderer _sr;

    //Private fields
    private Vector2 _startPosition;

    //Serialized fields
    [SerializeField]
    int _force = 500;
    [SerializeField]
    float _maxDragDistance = 5;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb.isKinematic = true;

        _startPosition = _rb.position;
    }

    private void OnMouseDown()
    {
        _sr.color = Color.red;
    }

    private void OnMouseUp()
    {
        Vector2 currentPos = _rb.position;
        Vector2 direction = _startPosition - currentPos;

        direction.Normalize();

        _rb.isKinematic = false;
        _rb.AddForce(direction * _force);

        _sr.color = Color.white;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 desiredPos = mousePos;

        float distance = Vector2.Distance(desiredPos, _startPosition);
        if(distance > _maxDragDistance)
        {
            Vector2 direction = desiredPos - _startPosition;
            direction.Normalize();
            desiredPos = _startPosition + (direction * _maxDragDistance);
        }

        if (desiredPos.x > _startPosition.x)
            desiredPos.x = _startPosition.x;

        _rb.position = desiredPos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _rb.position = _startPosition;
        _rb.isKinematic = true;
        _rb.velocity = Vector2.zero;
    }

}
