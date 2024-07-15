
using System;

public class CheckIsDead : BossNodeBase
{
    public CheckIsDead(NukeFistBoss owner) : base(owner) { }

    public override NodeState Evaluate()
    {
        return owner.HP <= 0 ? NodeState.Success : NodeState.Failure;
    }
}

public class CheckIsHit : BossNodeBase
{
    public CheckIsHit(NukeFistBoss nukePistBoss) : base(nukePistBoss) { }

    public override NodeState Evaluate()
    {
        return owner.IsHit ? NodeState.Success : NodeState.Failure;
    }
}

public class CheckIsInRange : BossNodeBase
{
    public CheckIsInRange(NukeFistBoss owner, Func<bool> isInRange) : base(owner) { this.isInRange = isInRange; }

    private Func<bool> isInRange;

    public override NodeState Evaluate()
    {
        return isInRange.Invoke() == true ? NodeState.Success : NodeState.Failure;
    }
}

public class CheckCanMove : BossNodeBase
{
    public CheckCanMove(NukeFistBoss owner, Func<bool> canMove) : base(owner) {this.canMove = canMove; } 
    private Func<bool> canMove;
    public override NodeState Evaluate()
    {
        return canMove.Invoke() == true ? NodeState.Success : NodeState.Failure;
    }
}