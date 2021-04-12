using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeadState : ZombieStates
{
    private static readonly int MovementZHash = Animator.StringToHash("MovementZ");
    private static readonly int DeadHash = Animator.StringToHash("IsDead");

    public ZombieDeadState(ZombieComponment zombie, ZombieStateMachine stateMachine) : base(zombie, stateMachine)
    {
    }

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        OwnerZombie.ZombieNavMesh.isStopped = true;
        OwnerZombie.ZombieNavMesh.ResetPath();

        OwnerZombie.ZombieAnimator.SetFloat(MovementZHash, 0);
        OwnerZombie.ZombieAnimator.SetBool(DeadHash, true);

    }

    public override void Exit()
    {
        base.Exit();

        OwnerZombie.ZombieNavMesh.isStopped = false;

        OwnerZombie.ZombieAnimator.SetBool(DeadHash, false);
    }


}
