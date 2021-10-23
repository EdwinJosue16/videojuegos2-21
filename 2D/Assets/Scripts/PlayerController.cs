using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
public class PlayerController : MonoBehaviour
{

    private Rigidbody2D physics;
    [SerializeField] float forceX = 1000f;
    // Start is called before the first frame update
    public LayerMask groundLayer; 
    [SerializeField]private bool jumping = false;
    [SerializeField]public Animator animator;
    public Vector3 direction = Vector3.zero;
    public float groundLength = 0.55f;

    [Header("Physics Zone")]
    
    public float jumpSpeed = 6f;
    public float gravity = 1f;
    public float fallMultiplier = 5f;
    public float linearDrag;

    public float jumpDelay = 0.25f;
    public float jumpTimer;
    
    public float directionForce = 0;

    [Header("Buttons Pressed")]
    public bool jumpPressed = false;

    public Vector3 colliderOffset;

    
    


    void Start()
    {
        physics =  gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      if(directionForce > 0){
          animator.SetBool("isPlayerRunning", true);  
          direction = Vector3.zero;
      }
      else if(directionForce < 0){
          animator.SetBool("isPlayerRunning", true);
          direction = new Vector3(0,180,0);
      }
      else{
          animator.SetBool("isPlayerRunning", false);
      }

      
    }

    void FixedUpdate(){
        Quaternion rotationTarget = Quaternion.Euler(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotationTarget, Time.deltaTime * 4);
        jumping = !Physics2D.Raycast(transform.position+colliderOffset, Vector2.down, groundLength,groundLayer) || !Physics2D.Raycast(transform.position-colliderOffset, Vector2.down, groundLength,groundLayer);
        physics.AddForce(new Vector2(forceX * Time.deltaTime * directionForce, 0));
        ModifyPlayerPhysics();
        if(jumpTimer > Time.time && !jumping){
            Jump();
        }
    }

    void ModifyPlayerPhysics(){
        if(!jumping){ // Si está en el piso
            physics.gravityScale = 0;
        }
        else{
            physics.gravityScale = gravity;
            physics.drag = linearDrag * 0.15f;
            if(physics.velocity.y < 0){ // Vamos cayendo
                physics.gravityScale = gravity * fallMultiplier;
            }
            else if(physics.velocity.y > 0 && !jumpPressed){
                physics.gravityScale = gravity * (fallMultiplier/2);
            }
        }
    }

    public void Jump(InputAction.CallbackContext context){
        if(context.performed){
            jumpTimer = Time.time + jumpDelay; // Valor en el futuro
            jumpPressed = true;
        }
        else if(context.canceled){
            jumpPressed = false;
        }
    }

    public void Run(InputAction.CallbackContext context){
        if(context.performed){
            directionForce = context.ReadValue<float>();
        }
        else if(context.canceled){
            directionForce = 0;
        }
        Debug.Log("Context Run: " + directionForce);
    }

    public void Jump(){
        physics.velocity = new Vector2(physics.velocity.x,0);
        physics.AddForce(Vector2.up * jumpSpeed,  ForceMode2D.Impulse); //(0,1) -> (0,5)
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position+colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position-colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }

}
