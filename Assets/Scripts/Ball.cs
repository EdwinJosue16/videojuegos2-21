using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    // Bandera
    // Tener la referencia a la paleta
    [SerializeField] Pad paddle;
    [SerializeField] Vector2 velocity = new Vector2(1f, 4f);
    bool playing = false;
    Vector2 paddleToBallDistance;

    Rigidbody2D ball;

    void Start()
    {
        ball = GetComponent<Rigidbody2D>();
        paddleToBallDistance = transform.position - paddle.transform.position;
    }

    void Update()
    {
        LockToPaddle();
        LaunchOnClick();
    }

    void LaunchOnClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            playing = true;
            ball.velocity = velocity;
        }
    }

    void LockToPaddle()
    {
        if (!playing)
        {
            Vector2 paddleRef = paddle.transform.position;
            Vector2 paddlePos = new Vector2(paddleRef.x, paddleRef.y);
            transform.position = paddlePos + paddleToBallDistance;
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        var contactPoint = other.contacts[0];
        Bounce(contactPoint);
    }
    void Bounce(ContactPoint2D contact)
    {
        // Caso de la pared izquierda: 
        /*
            |    /  : reflection
            |   /
            |  /
            | /
            |---------- : normal
            | \
            |  \
            |   \
            |    \  : ballVelocity
        */
        // Los otros son analogos

        Vector2 ballVelocity = ball.velocity;
        Vector2 normal = contact.normal;
        Debug.Log(normal);
        Vector2 reflection = Vector2.Reflect(ballVelocity.normalized, normal);
        ball.velocity = reflection * velocity.magnitude;
    }

}
