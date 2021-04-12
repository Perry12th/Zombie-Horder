using UnityEngine;

public class SpawnerStateMachine :StateMachine<SpawnerStateEnum>
{
}

public class SpawnerState : State<SpawnerStateEnum>
{
    protected ZombieSpawner Spawner;
    protected SpawnerState(ZombieSpawner spawner, SpawnerStateMachine stateMachine) : base(stateMachine)
    {
        Spawner = spawner;
    }

    protected void SpawnZombie()
    {
        GameObject zombieToSpawn = Spawner.ZombiePrefabs[Random.Range(0, Spawner.ZombiePrefabs.Length)];
        SpawnerVolumes spawnerVolumes = Spawner.SpawnerVolumes[Random.Range(0, Spawner.SpawnerVolumes.Length)];

        GameObject zombie = Object.Instantiate(zombieToSpawn, spawnerVolumes.GetPositionInBounds(), spawnerVolumes.transform.rotation);

        zombie.GetComponent<ZombieComponment>().FollowTarget = Spawner.TargetObject;
    }
}
