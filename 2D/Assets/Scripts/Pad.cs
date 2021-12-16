using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pad : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float screenSizeUnits = 17.76f;
    [SerializeField] float minX = 0;
    [SerializeField] float maxX = 16.76f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float paddlePos = Input.mousePosition.x/Screen.width * screenSizeUnits;
        //transform.position = new Vector2(paddlePos, transform.position.y);
        Vector2 position = new Vector2(transform.position.x, transform.position.y);
        position.x = Mathf.Clamp(paddlePos, minX, maxX);
        transform.position = position;
    }

    
}
