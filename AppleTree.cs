using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour{
    [Header("Set in Inspector")]
    //Prefab for instantiating apples
    public GameObject applePrefab;
    //Prefab for instantiating PineNuts
    public GameObject PineNutPrefab;
    public float speed = 1f;
    public float leftAndRightEdge = 10f;
    public float chanceToChangeDirections = 0.1f;
    public float secondsBetweenAppleDrops = 1f;
    public float secondsBetweenPineNutDrops = 10f;

    // Start is called before the first frame update
    void Start(){
        //Dropping apples every second
        Invoke("DropApple", 2f);
        //Dropping PineNuts every seconds BetweenPineNutDrops
        InvokeRepeating("DropPineNut", 10f, 10f);
    }

    void DropApple(){
        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke("DropApple", secondsBetweenAppleDrops);
    }

    void DropPineNut(){
        GameObject PineNut = Instantiate<GameObject>(PineNutPrefab);
        PineNut.transform.position = transform.position;
        InvokeRepeating("DropPineNut", 10f, 10f);
    }

    // Update is called once per frame
    void Update(){
        //Basic Movement 
        Vector3 pos = transform.position;
        pos.x += speed *Time.deltaTime;
        transform.position = pos;
        //Changing Direction
        if(pos.x <- leftAndRightEdge){
            speed = Mathf.Abs(speed); //Move Right
        } else if(pos.x >leftAndRightEdge){
            speed = -Mathf.Abs(speed); //Move Left
        }
    }

    void FixedUpdate(){
        //Changing Direction Randomly is now time-based because of FixedUpdate()
        if (Random.value < chanceToChangeDirections){
            speed *= -1; //Change Direction
        }
    }
}

