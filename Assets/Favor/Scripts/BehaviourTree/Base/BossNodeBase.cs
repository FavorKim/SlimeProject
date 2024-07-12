public abstract class BossNodeBase : Node
{
    protected NukeFistBoss owner;

    public BossNodeBase(NukeFistBoss owner)
    {
        this.owner = owner;
    }

    public abstract NodeState Evaluate();
}
