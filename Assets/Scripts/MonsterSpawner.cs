using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] monsterReference;//we add Monster game objects to this array via the unity engine, this is why it is serialized.
    [SerializeField]
    private Transform leftPostion, rightPosition;// we access the Transfrom props of our left and right spawner game objs, vars initialized via unity engine.

    private GameObject spawnedMonster;

    private int randomIndex;
    private int randomSide;
    // Start is called before the first frame update
    void Start()
    {
        //Our coroutine is called at random intervals
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while (true) //we use a while loop to continually execute this logic, as long as game runs we will spawn monsters, 
        //if we didnt have a while loop, it would only be executed once, since we are calling it in the start func
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            randomIndex = Random.Range(0, monsterReference.Length);
            randomSide = Random.Range(0, 2);

            //instantiate creates a copy of passed in game obj
            //the passed in gameobject is a random monster in our gameObject array monsterReference which has been set via the seriized field in the unity engine.
            spawnedMonster = Instantiate(monsterReference[randomIndex]);


            //left side
            if (randomSide == 0)
            {
                spawnedMonster.transform.position = leftPostion.position;
                spawnedMonster.GetComponent<Monster>().speed = Random.Range(4, 10);
            }
            //right side
            else
            {
                spawnedMonster.transform.position = rightPosition.position;
                //we use a negative value as our speed because the monster is spawned on the right side of mapp, we want to push the monster towards the player
                // remember the speed var is used in this expression monsterBody.velocity = new Vector2(speed, monsterBody.velocity.y) in monster class
                //assigning a negative value to velocity on the x-axis makes the game Obj move in <----------- direction.
                //a positive value moves game Obj in ---------> direction.
                spawnedMonster.GetComponent<Monster>().speed = -Random.Range(4, 10);
                //change monster orientation
                spawnedMonster.transform.localScale = new Vector3(-1f, 1f, 1f);

            }
        }
    }
}
