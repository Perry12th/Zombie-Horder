using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnerStateMachine))]
public class ZombieSpawner : MonoBehaviour
{

    [SerializeField]
    private int NumberOfZombiesToSpawn;
    
    public GameObject[] ZombiePrefabs;

    public SpawnerVolumes[] SpawnerVolumes;

    public GameObject TargetObject => FollowGameObject;
    private GameObject FollowGameObject;

    private SpawnerStateMachine StateMachine;
    private void Awake()
    {
        StateMachine = GetComponent<SpawnerStateMachine>();
        FollowGameObject = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        ZombieWaveSpawnerState beinngerWave = new ZombieWaveSpawnerState(this, StateMachine)
        {
            ZombiesToSpawn = 5,
            NextState = SpawnerStateEnum.Complete
        };
        StateMachine.AddState(SpawnerStateEnum.Beginner, beinngerWave);

        StateMachine.Initialize(SpawnerStateEnum.Beginner);
    }

    private void SpawnZombie()
    {
        GameObject zombieToSpawn = ZombiePrefabs[UnityEngine.Random.Range(0, ZombiePrefabs.Length)];
        SpawnerVolumes spawnerVolumes = SpawnerVolumes[UnityEngine.Random.Range(0, SpawnerVolumes.Length)];

        GameObject zombie = Instantiate(zombieToSpawn, spawnerVolumes.GetPositionInBounds(), spawnerVolumes.transform.rotation);

        zombie.GetComponent<ZombieComponment>().FollowTarget = FollowGameObject;
    }

   
}

class ZombieWaveSpawnerState : SpawnerState
{
    public int ZombiesToSpawn = 5;
    public SpawnerStateEnum NextState;
    private int TotalZombiesKilled = 0;
    public ZombieWaveSpawnerState(ZombieSpawner spawner, SpawnerStateMachine stateMachine) : base(spawner, stateMachine)
    {

    }

    public override void Start()
    {
        base.Start();

        for (int i = 0; i < ZombiesToSpawn; i++)
        {
            SpawnZombie();
        }
    }
}
