using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   [SerializeField] public GameObject BlockPrefab;
   [SerializeField] public GameObject ParentContainer;

   void Start(){
       GameObject block = GameObject.Instantiate(BlockPrefab);
       block.name = "BlockPrefab";
       block.transform.position = new Vector2(1f,2f);
       block.transform.parent = ParentContainer.transform;

   }
}
