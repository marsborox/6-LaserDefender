using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

[CreateAssetMenu(menuName= "Wave COnfig", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] List<GameObject> enemyPrefabs;

    //this will have waypoints from prefab
    //its container not waypoints themselves
    [SerializeField] Transform pathPrefab;
    [SerializeField] float moveSpeed = 5f;
    //set spawn time
    [SerializeField] float timeBetweenEnemySpawns = 1f;
    //this is to randomise spawn time
    [SerializeField] float spawnTimeVariance = 1f;
    //this will make sure we wont go to negative numbers
    [SerializeField] float minimumSpawnTime = 0.2f;
    public Transform GetStartingWaypoint()
    { //this will return child in index0 in prefab
        return pathPrefab.GetChild(0);
    }
    public List<Transform> GetWaypoints()
    //will return list of waypoints
    { 
        //child is variable of type Transform in our parent
        List<Transform> waypoints = new List<Transform>();
        //we will loop through all of children and store them as Transforms
        foreach (Transform child in pathPrefab)
        { 
            waypoints.Add(child);
        }
        return waypoints;
    }
    public float GetMoveSpeed()
    {//this will return move speed to other class
        //we call this method in other class
        return moveSpeed;
    }
    //returns the number of enemies in enemyPrefabs List
    public int GetEnemyCount()
    { 
        return enemyPrefabs.Count;
    }
    // Return the enemy prefab at the given index
    public GameObject GetEnemyPrefab(int index)
    { 
        return enemyPrefabs[index];
    }
    public float GetRandomSpawnTime()
    { 
        //range is from sum to substract
        float spawnTime = Random.Range(timeBetweenEnemySpawns - spawnTimeVariance, timeBetweenEnemySpawns  + spawnTimeVariance);
        //float value, float min, float max
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
