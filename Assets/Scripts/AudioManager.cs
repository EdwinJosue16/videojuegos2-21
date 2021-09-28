using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound{
    [Header("General")]
    public string Name;
    public AudioClip Clip;
    private AudioSource Source;

    [Header("Properties")]
    [Range(0f,1f)]
    public float Volume = 0.5f;

    [Range(0f,2f)]
    public float Pitch = 1f;

    public bool Loop = false;

    public void SetSource(AudioSource _source){
        Source = _source;
        Source.clip = Clip;
    }

    public void Play(){
        Source.pitch = Pitch;
        Source.volume = Volume;
        Source.loop = Loop;
        Source.Play();
    }
}


public class AudioManager : MonoBehaviour
{
   public static AudioManager instance;
   
   [SerializeField] private Sound[] Sfx;

   
   [SerializeField] private Sound[] Music; 


   void Awake(){
       if(instance == null){
           instance = this;
           GameObject.DontDestroyOnLoad(this);
        }
       else if(instance != this){
           Destroy(gameObject);
        }

        for(int i = 0; i < Sfx.Length; i++){
            GameObject _go = new GameObject("SFX_" + i + "_" + Sfx[i].Name);
            _go.transform.parent = transform;
            Sfx[i].SetSource(_go.AddComponent<AudioSource>());
        }

        for(int i = 0; i < Music.Length; i++){
            GameObject _go = new GameObject("SFX_" + i + "_" + Music[i].Name);
            _go.transform.parent = transform;
            Music[i].SetSource(_go.AddComponent<AudioSource>());
        }
   }

   public void PlaySfx(string name){
       Sound audio = SearchSound(name, Sfx);
       if(audio != null){
           audio.Play();
       }
   }
   public void PlayMusic(string name){
        Sound audio = SearchSound(name, Music);
        if(audio != null){
           audio.Play();
        }
   }

   private Sound SearchSound(string name, Sound [] audio){
       for(int i = 0 ; i < audio.Length; i++){
           if(audio[i].Name == name){
               return audio[i];
           }
       }
       Debug.LogError("AudioManager: audio" + name + " not found");
       return null;
   }



}
