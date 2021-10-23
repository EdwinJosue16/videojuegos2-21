using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{

    public static GameManager instance = null;

    [SerializeField] public int Lives {set; get;}

    [SerializeField] public TextMeshProUGUI guiLives;

    public Timer Timer;
    void Awake()
    {
        if(instance == null){
            instance = this;
            Timer = gameObject.GetComponent<Timer>();
        }
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
        guiLives.text = "Lives: " + Lives;
    }


    public void StartGame(){
        Timer.Active = true;
        guiLives.text = "Lives: " + Lives;
    }
}
