
public enum NodeState
{
    Success,
    Failure,
    Running
}

public interface Node
{
    public NodeState Evaluate();
}
