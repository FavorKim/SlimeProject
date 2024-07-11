using System.Collections.Generic;

namespace BehaviourTree
{
    /*
     * Sequence: 자식 노드들을 순회하는 Composite
     * 
     * Failure 상태인 노드를 만날 경우, 평가를 종료한다. (순회를 종료한다. 상위에 Selector 노드가 있다면, 그것의 다음 자식 노드로 이동한다.)
     * Running 상태인 노드를 만날 경우, 다음에도 이 노드를 평가한다. (순회를 종료한다. 상위에 Selector 노드가 있다면, 그것의 다음 자식 노드로 이동하지 않는다.) 
     * Success 상태인 노드를 만날 경우, 다음에는 다음 노드를 평가한다. (다음 노드로 순회를 계속한다.)
    */

    // Sequence 클래스
    public class Sequence : Node
    {
        // 자식 노드 목록
        private readonly List<Node> children;

        public Sequence(List<Node> children)
        {
            this.children = children;
        }

        // 평가 함수
        public override NodeState Evaluate()
        {
            // 만약 자식 노드가 없다면,
            if (children == null || children.Count == 0)
            {
                // Failure를 반환한다.
                return NodeState.FAILURE;
            }

            // 자식 노드를 순회하면서,
            foreach (Node child in children)
            {
                // 상태를 평가한다.
                switch (child.Evaluate())
                {
                    // Running일 경우, Running을 반환한다.
                    case NodeState.RUNNING:
                        return NodeState.RUNNING;

                    // Success일 경우, 다음 노드를 평가한다.
                    case NodeState.SUCCESS:
                        continue;

                    // Failure일 경우, Failure를 반환한다.
                    case NodeState.FAILURE:
                        return NodeState.FAILURE;
                }
            }

            // 전부 순회했을 경우, Success를 반환한다. (전부 실행했다는 뜻)
            return NodeState.SUCCESS;
        }
    }
}
