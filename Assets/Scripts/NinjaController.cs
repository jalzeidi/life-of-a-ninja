using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaController : MonoBehaviour
{
    public float speed;

    Rigidbody2D rigidbody2d;
    float horizontalDirection;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        horizontalDirection = 0;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalDirection = Input.GetAxis("Horizontal");
    }

    void FixedUpdate() 
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontalDirection *  Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
}
