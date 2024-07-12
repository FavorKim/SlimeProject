
using System.Collections.Generic;

public class Sequence : Node
{
    private List<Node> children;

    public Sequence(List<Node> children)
    {
        this.children = children;
    }

    public NodeState Evaluate()
    {
        if (children != null && children.Count > 0)
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Success:
                        continue;

                    case NodeState.Failure:
                        return NodeState.Failure;

                    case NodeState.Running:
                        return NodeState.Running;
                }
            }
            return NodeState.Success;
        }
        return NodeState.Failure;
    }
}
