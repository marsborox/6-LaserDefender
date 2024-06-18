using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinging : MonoBehaviour
{
    EnemySpawner enemySpawner;
    WaveConfigSO waveConfig;
    List<Transform>  waypoints;
    int waypointIndex = 0;


    private void Awake()
    {
       enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void Start()
    {
        waveConfig = enemySpawner.GetCurrentWave();
        waypoints =waveConfig.GetWaypoints();
        //move - transform.position is waypoint at waypoint index.position
        transform.position = waypoints[waypointIndex].position;
    }
    void Update()
    {
        FollowPath();
    }
    void FollowPath()
    {//if we are not on last waypoint
        if (waypointIndex < waypoints.Count)
        //we want to move towards the next waypoint in list
        {//need to know where we are, where are we going and how fast

            //where we are, vector must be 3 bcs we are in 3D
            Vector3 targetPosition = waypoints[waypointIndex].position;

            //we move each fram, to make frame independent *Time.deltaTime
            //from waveConfig we get speed
            float delta = waveConfig.GetMoveSpeed() * Time.deltaTime;

            //vector Current, Vector target, Max distance
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, delta);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else 
        {//if he is indexer the end of path
            Destroy(gameObject);
        }
    }
}
