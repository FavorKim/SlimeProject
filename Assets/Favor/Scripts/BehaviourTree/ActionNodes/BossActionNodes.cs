using System;

public class Dead : BossNodeBase
{
    public Dead(NukeFistBoss ow, Action onDead) : base(ow) { OnDead = onDead; }

    private Action OnDead;

    public override NodeState Evaluate()
    {
        OnDead.Invoke();
        return NodeState.Success;
    }
}

public class Hit : BossNodeBase
{
    public Hit(NukeFistBoss ow, Action onhit) : base(ow) { OnHit = onhit; }

    private Action OnHit;

    public override NodeState Evaluate()
    {
        OnHit.Invoke();
        return NodeState.Success;
    }
}

public class AttackWait : BossNodeBase
{
    public AttackWait(NukeFistBoss owner, Func<bool> check) : base(owner) { this.check = check; }
    private Func<bool> check;
    public override NodeState Evaluate()
    {
        return check.Invoke() == true ? NodeState.Running : NodeState.Success;
    }
}

public class Attack : BossNodeBase
{
    public Attack(NukeFistBoss ow, Action onAtk) : base(ow) { OnAtk = onAtk; }
    private Action OnAtk;

    public override NodeState Evaluate()
    {
        OnAtk.Invoke();
        return NodeState.Success;
    }
}

public class Move : BossNodeBase
{
    public Move(NukeFistBoss ow, Action onMove) : base(ow) {OnMove = onMove; } 
    private Action OnMove;
    public override NodeState Evaluate()
    {
        OnMove.Invoke();
        return NodeState.Success;
    }
}

public class Charge : BossNodeBase
{
    public Charge (NukeFistBoss ow, Action onCharge) : base(ow) { this.onCharge = onCharge; }
    private Action onCharge;
    public override NodeState Evaluate()
    {
        onCharge.Invoke();
        return NodeState.Success;
    }
}

public class SetFollowDest : BossNodeBase
{
    public SetFollowDest(NukeFistBoss ow, Action onSetFollowDest) : base(ow)
    {
        OnSetFollowDest = onSetFollowDest;
    }
    private Action OnSetFollowDest;

    public override NodeState Evaluate()
    {
        OnSetFollowDest.Invoke();
        return NodeState.Success;
    }
}