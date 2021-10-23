using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{
    // Translate -> no funciona con físicas
    // AddForce
    // velocity
    // movePosition
    [SerializeField] float speed = 1;
    public Rigidbody rigidbody;


    [SerializeField] private Transform _face;
    [SerializeField] private Transform _lookingAt;
    [SerializeField] private Transform _player;

    [SerializeField] private Vector2 _inputMovement;

    [SerializeField] private Vector3 _playerPosition;

    


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();   
        _face = transform.Find("Face").transform;
        _lookingAt = transform.Find("LookingAt").transform;
        _player = transform.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
      // _face.localPosition = new Vector3(_inputMovement.x*0.5f, 0.5f, _inputMovement.y*0.5f);
       
    }

    void FixedUpdate(){
        _lookingAt.localPosition = new Vector3(_inputMovement.x * 2.5f,0,_inputMovement.y);
        float rad = Mathf.Atan2(_inputMovement.y, _inputMovement.x);
        float deg = rad * (180/Mathf.PI);

        //_face.transform.rotation = Quaternion.Euler(0,deg,0);
        //_lookingAt.localPosition = _face.localPosition + _face.transform.right * 1.5f;
        _playerPosition = new Vector3(_inputMovement.x, 0, _inputMovement.y);
        
        _player.transform.rotation = Quaternion.Euler(0,0,0);
        _player.Translate(_playerPosition * speed * Time.deltaTime);
        _player.transform.rotation = Quaternion.Euler(0,deg,0);
       

    }

    void Move(Vector3 direction){
        transform.Translate(direction * speed * Time.deltaTime);   
        //rigidbody.AddForce(direction * speed);
        //rigidbody.velocity = direction * speed;
        //rigidbody.MovePosition(rigidbody.position + direction * speed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context){
        if(context.performed){
            Debug.Log("Context" + context);
            _inputMovement = context.ReadValue<Vector2>();
        }
        else if(context.canceled){
            _inputMovement = Vector2.zero;
        }
    }
}
