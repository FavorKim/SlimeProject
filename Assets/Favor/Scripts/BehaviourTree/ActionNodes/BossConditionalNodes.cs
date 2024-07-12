
using System;

public class CheckIsDead : BossNodeBase
{
    public CheckIsDead(NukeFistBoss owner) : base(owner) { }

    public override NodeState Evaluate()
    {
        return owner.HP<=0 ? NodeState.Success : NodeState.Failure;
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

public class CheckIsAttacking : BossNodeBase
{
    public CheckIsAttacking(NukeFistBoss nukeFistBoss, Func<bool> isAttacking) : base(nukeFistBoss) { this.isAttacking = isAttacking; }
    private Func<bool> isAttacking;

    public override NodeState Evaluate()
    {
        return isAttacking.Invoke() == true ? NodeState.Running : NodeState.Success;
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