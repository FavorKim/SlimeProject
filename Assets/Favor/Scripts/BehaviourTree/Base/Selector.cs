using System.Collections.Generic;

public class Selector : Node
{
    private List<Node> children;

    public Selector(List<Node> children)
    {
        this.children = children;
    }

    public NodeState Evaluate()
    {
        if (children.Count > 0 && children != null)
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Success:
                        return NodeState.Success;
                    case NodeState.Failure:
                        continue;
                    case NodeState.Running:
                        return NodeState.Running;
                }
            }
        }
        return NodeState.Failure;
    }
}
