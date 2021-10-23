using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float StartTime;
    private float TimeElapsed;
    public TextMeshProUGUI GuiTime;

    [SerializeField] public bool Active {set; get;}
    
    // Start is called before the first frame update


    void Start()
    {
        StartTime = Time.time;
        Active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Active){
            StartTime = Time.time;
            return;
        }
        float RestSeconds = Time.time - StartTime;
        TimeElapsed = RestSeconds;
        int roundedRestSeconds = Mathf.CeilToInt(RestSeconds);
        int displaySeconds = roundedRestSeconds % 60;
        int displayMinutes = roundedRestSeconds / 60;
        GuiTime.text = string.Format("{0:00}:{1:00}", displayMinutes, displaySeconds);
    }
}
