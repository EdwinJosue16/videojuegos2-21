using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    [SerializeField] public int Lives {set; get;}

    void Awake()
    {
        if(instance == null)
            instance = this;
        else if(instance != this)
            Destroy(gameObject);
    }

    void Start(){
        Lives = 3;
    }

    void Update(){
        if(Lives == 0){
            Debug.Log("Perdió :(");
        }
    }

}
