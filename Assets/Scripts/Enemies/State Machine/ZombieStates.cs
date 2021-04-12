public class ZombieStates : State<ZombieStateType>
{
    protected ZombieComponment OwnerZombie;
    public ZombieStates(ZombieComponment zombie, ZombieStateMachine stateMachine) : base(stateMachine)
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



