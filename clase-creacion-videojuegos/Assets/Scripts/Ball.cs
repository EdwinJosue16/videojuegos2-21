using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    // Bandera
    // Tener la referencia a la paleta
    [SerializeField] Pad paddle;
    [SerializeField] Vector2 velocity = new Vector2(1f,4f);
    bool playing = false;
    Vector2 paddleToBallDistance; 
    // Start is called before the first frame update
    void Start()
    {
        paddleToBallDistance = transform.position - paddle.transform.position;
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
            GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }

    void LockToPaddle(){
        if(!playing){
            Vector2 paddleRef = paddle.transform.position;
            Vector2 paddlePos = new Vector2(paddleRef.x,paddleRef.y);
            transform.position = paddlePos + paddleToBallDistance;
        }
    }

}
