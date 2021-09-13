using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    
    // Bandera
    // Tener la referencia a la paleta
    [SerializeField] Pad paddle;
    bool playing = false;
    Vector2 paddleToBallDistance; 

    [SerializeField] private float xVelocity;
    [SerializeField] private float yVelocity;

    [SerializeField] private float xMultiplier;

    private Rigidbody2D _ballRigidBody;

    [SerializeField] float collisionFloat = 0.45f;


    // Start is called before the first frame update
    void Start()
    {
        paddleToBallDistance = transform.position - paddle.transform.position;
        _ballRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LockToPaddle();
        LaunchOnClick();
        
    }
    void LaunchOnClick(){
        if(Input.GetMouseButtonDown(0)){
            playing = true;
            _ballRigidBody.velocity = new Vector2(xVelocity,yVelocity);
        }
    }

    void LockToPaddle(){
        if(!playing){
            Vector2 paddleRef = paddle.transform.position;
            Vector2 paddlePos = new Vector2(paddleRef.x,paddleRef.y);
            transform.position = paddlePos + paddleToBallDistance;
        }
    }

    void OnCollisionEnter2D (Collision2D  collision)
    {
        string collisionTag = collision.gameObject.tag;
        
        if (collisionTag == Constants.HORIZONTAL_WALL ){
            OnHorizontalCollision();
        }
        if(collisionTag == Constants.VERTICAL_WALL){
            OnVerticalCollision();
        }
        if(collisionTag == Constants.PADDLE){
            OnPaddleCollision(collision);
        }
        if(collisionTag == Constants.BLOCK){
            OnBlockCollision(collision);
        }
        if(collisionTag == Constants.LOST){
            OnPlayerLost();
        }
    }

    void OnPlayerLost(){
        playing = false;
        GameManager.instance.Lives = GameManager.instance.Lives - 1; 
    }

    void OnBlockCollision(Collision2D block){
        Vector2 collision = block.contacts[0].point;
        float xCollisionPoint = collision.x - block.transform.position.x;
        float yCollisionPoint = collision.y - block.transform.position.y;
        //Debug.Log("Block Collision X: " + xCollisionPoint + " Y: " + yCollisionPoint);
        if (Mathf.Abs(yCollisionPoint) > collisionFloat){
            yVelocity *= -1;
        }
        else if (Mathf.Abs(xCollisionPoint) > collisionFloat){
            xVelocity *= -1;
        }
        _ballRigidBody.velocity = new Vector2(xVelocity,yVelocity);
    }

    void OnPaddleCollision(Collision2D collision){
        float xCollisionPoint = collision.contacts[0].point.x - collision.transform.position.x;
        xVelocity = xCollisionPoint * xMultiplier;
        yVelocity *=-1;
        _ballRigidBody.velocity = new Vector2(xVelocity,yVelocity);
    }
    

    void OnHorizontalCollision(){
        yVelocity *= -1;
        _ballRigidBody.velocity = new Vector2(xVelocity,yVelocity);
    }

    void OnVerticalCollision(){
        xVelocity *= -1;
        _ballRigidBody.velocity = new Vector2(xVelocity, yVelocity);
    }
    
}
