using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    

    public GameObject objectToFollow;
    public Vector2 offsetSize;
    public Vector2 threshold; // Umbral que vamos a manejar para mover la cámara

    public Rigidbody2D rigidbody2D; 
    [SerializeField] float speed = 3f;

    [SerializeField] Vector3 offsetPosition;

    // Start is called before the first frame update
    void Start()
    {
       threshold =  CalculateCameraThreshold();
       rigidbody2D = objectToFollow.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        Vector3 follow = objectToFollow.transform.position - offsetPosition;
        float xDiff = Vector2.Distance(Vector2.right * transform.position.x, Vector2.right * follow.x);
        float yDiff = Vector2.Distance(Vector2.up * transform.position.y, Vector2.up * follow.y);
        Vector3 newPosition = transform.position;
        if(Mathf.Abs(xDiff) >= threshold.x){
            newPosition.x = follow.x;
        }
        if(Mathf.Abs(yDiff) >= threshold.y){
            newPosition.y = follow.y;
        }

        //Brinco Abrupto
        //transform.position = newPosition;

        float playerSpeed = rigidbody2D.velocity.magnitude > speed ? rigidbody2D.velocity.magnitude : speed;
        transform.position = Vector3.MoveTowards(transform.position,newPosition,playerSpeed*Time.deltaTime);
    }

    private Vector3 CalculateCameraThreshold(){
        Rect screenAspect = Camera.main.pixelRect;
        float cameraSize = Camera.main.orthographicSize;
        Vector2 threshold = new Vector2(cameraSize * screenAspect.width / screenAspect.height,cameraSize);
        return threshold - offsetSize;
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Vector2 rect = CalculateCameraThreshold();
        Gizmos.DrawWireCube(transform.position + offsetPosition, new Vector3(rect.x*2, rect.y*2, 1));
    }
}
