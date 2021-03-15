using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Health;

[RequireComponent(typeof(StateMachine))]
public class ZombieHealthComponent : HealthCompoment
{
    private StateMachine ZombieStateMachine;
    // Start is called before the first frame update
    void Awake()
    {
        ZombieStateMachine = GetComponent<StateMachine>();
    }

    public override void Destroy()
    {
        ZombieStateMachine.ChangeState(ZombieStateType.Dead);
    }
}
