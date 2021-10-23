using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
   [SerializeField] public GameObject BlockPrefab;
   [SerializeField] public Transform ParentContainer;

   [SerializeField] public GameObject FirstBlockGuide;


   private string [][,] LevelArray = new string[3][,];

   void Awake(){
       string [,] level = {
           {"X","X","X","X","X","X","X","X","X","X","X","X","X","X"},
           {"X","X","X","X","X","X","X"," ","X","X","X","X","X","X"},
           {"X"," ","X"," "," "," ","X","X","X"," "," "," ","X","X"}
       };
       LevelArray[0] = level;
   } 

   void Start(){
       /* GameObject block = GameObject.Instantiate(BlockPrefab);
          block.name = "BlockPrefab";
          block.transform.position = new Vector2(1f,2f);
          block.transform.parent = ParentContainer.transform;*/
        BuildLevel(0);
   }

   public void BuildLevel(int level){
        Vector2 guidePosition = FirstBlockGuide.transform.position;
        Vector2 currentPosition = guidePosition;
        int blockCount = 0;
        for(int f = 0; f < LevelArray[level].GetLength(0) ; f++){
            for(int c = 0 ; c < LevelArray[level].GetLength(1) ; c++){
                if(LevelArray[level][f,c] == "X"){
                    string name = "BlockPrefab" + (++blockCount);
                    InstantiateBlock(name, currentPosition, ParentContainer);
                }
                currentPosition = new Vector2(currentPosition.x + 1.15f, currentPosition.y);
            }
            currentPosition = new Vector2(guidePosition.x, currentPosition.y - 1.15f);
        }
    }

    GameObject InstantiateBlock(string name, Vector2 position, Transform parent){
        GameObject block = GameObject.Instantiate(BlockPrefab);
        block.name = name;
        block.transform.parent = parent;
        block.transform.position = position;
        return block;
    }
}
