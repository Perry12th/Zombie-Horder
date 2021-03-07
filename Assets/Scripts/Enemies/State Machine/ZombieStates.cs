public class ZombieStates : State
{
    protected ZombieComponment OwnerZombie;
    public ZombieStates(ZombieComponment zombie, StateMachine stateMachine) : base(stateMachine)
    {
        OwnerZombie = zombie;
    }
}

public enum ZombieStateType
{ 
    Idle,
    Attack,
    Follow,
    Dead
}



