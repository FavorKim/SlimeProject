using BehaviourTree;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourTreeExample : MonoBehaviour
{
    // 행동 트리를 사용하는 클래스 (예: 몬스터 클래스)
    // private ??? _???;

    // 뿌리(Root) 노드
    private Node rootNode;

    // Awake()
    private void Awake()
    {
        // 행동 트리를 사용하는 클래스를 이 클래스가 부착된 게임 오브젝트의 컴포넌트 목록에서 찾아 참조한다.
        // TryGetComponent(out _???);

        // 행동 트리를 작성한다.
        rootNode = SetBehaviourTree();
    }

    // Update()
    private void Update()
    {
        // 매 프레임마다 상태를 평가한다.
        rootNode?.Evaluate();
    }

    // 행동 트리를 작성한다.
    private Node SetBehaviourTree()
    {
        // 0. 뿌리 노드(Selector)
        Node node = new Selector(new List<Node>
            {
                // Selector
                new Selector(new List<Node>()
                {
                    // Sequence
                    new Sequence(new List<Node>()
                    {
                        // ??? 노드; 생성자의 매개변수로 행동 트리를 사용하는 클래스를 전달한다.
                        // new ???(_???);
                    })
                })
            });

        // 작성한 노드를 반환한다.
        return node;
    }
}
