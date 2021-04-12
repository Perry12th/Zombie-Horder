using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Health;

[RequireComponent(typeof(ZombieStateMachine))]
public class ZombieHealthComponent : HealthCompoment
{
    private ZombieStateMachine ZombieStateMachine;
    // Start is called before the first frame update
    void Awake()
    {
        ZombieStateMachine = GetComponent<ZombieStateMachine>();
    }

    public override void Destroy()
    {
        ZombieStateMachine.ChangeState(ZombieStateType.Dead);
    }
}
