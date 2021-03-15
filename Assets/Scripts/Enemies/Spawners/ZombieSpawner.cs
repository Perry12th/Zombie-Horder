using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{

    [SerializeField]
    private int NumberOfZombiesToSpawn;
    [SerializeField]
    private GameObject[] ZombiePrefabs;
    [SerializeField]
    private SpawnerVolumes[] SpawnerVolumes;

    private GameObject FollowGameObject;
    // Start is called before the first frame update
    void Start()
    {
        FollowGameObject = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < NumberOfZombiesToSpawn; i++)
        {
            SpawnZombie();
        }
    }

    private void SpawnZombie()
    {
        GameObject zombieToSpawn = ZombiePrefabs[UnityEngine.Random.Range(0, ZombiePrefabs.Length)];
        SpawnerVolumes spawnerVolumes = SpawnerVolumes[UnityEngine.Random.Range(0, SpawnerVolumes.Length)];

        GameObject zombie = Instantiate(zombieToSpawn, spawnerVolumes.GetPositionInBounds(), spawnerVolumes.transform.rotation);

        zombie.GetComponent<ZombieComponment>().FollowTarget = FollowGameObject;
    }

   
}
