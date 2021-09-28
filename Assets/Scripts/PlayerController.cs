using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    void Start()
    {
        physics =  gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
      if(Input.GetKey(KeyCode.D)){
          physics.AddForce(new Vector2(forceX * Time.deltaTime, 0));
          animator.SetBool("isPlayerRunning", true);  
          direction = Vector3.zero;
      }
      else if(Input.GetKey(KeyCode.A)){
          physics.AddForce(new Vector2(-1* forceX * Time.deltaTime, 0));
          animator.SetBool("isPlayerRunning", true);
          direction = new Vector3(0,180,0);
      }
      else{
          animator.SetBool("isPlayerRunning", false);
      }

      Quaternion rotationTarget = Quaternion.Euler(direction);
      transform.rotation = Quaternion.Lerp(transform.rotation, rotationTarget, Time.deltaTime * 4);
    }

    void FixedUpdate(){
        jumping = !Physics2D.Raycast(transform.position, Vector2.down, groundLength,groundLayer);
    }

    public void Jump(InputAction.CallbackContext context){
        if(context.performed){
            if(!jumping){
                Debug.Log("Está brincando: " + context.phase);
                physics.AddForce(Vector2.up* 6f,  ForceMode2D.Impulse); //(0,1) -> (0,5)
            }
           
        }
     

    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundLength);
    }

}
