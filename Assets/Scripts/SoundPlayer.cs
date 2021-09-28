using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{

    [SerializeField] private string SoundName;
    [SerializeField] public enum SoundType {Sfx, Music};
    [SerializeField] public SoundType Type;

    public bool Playing = false;
    void Update()
    {
       if(!Playing){
           Debug.Log("Playing sound: "+ SoundName);
           if(Type == SoundType.Music){
               AudioManager.instance.PlayMusic(SoundName);
           }
           else{
               AudioManager.instance.PlaySfx(SoundName);
           }
           Playing = true;
       }
    }
}
