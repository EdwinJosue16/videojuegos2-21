using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int TimesToBreak {set;get;}
    private void OnCollisionEnter2D(Collision2D other){
        AudioManager.instance.PlaySfx("WoodBreak");
        Destroy(this.gameObject);
    }
}
