using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    // ???의 행동 트리(BT; Behaviour Tree) 클래스
    public class EntityBehaviourTree : MonoBehaviour
    {
        // ??? 클래스
        // private ??? _???;

        // 뿌리(Root) 노드
        private Node rootNode;

        // Awake()
        private void Awake()
        {
            // 생성 시 자신의 ???(를 상속받는 클래스) 스크립트를 컴포넌트에서 참조한다.
            // TryGetComponent(out _???);

            // 행동 트리를 작성한다.
            rootNode = SetBehaviourTree();
        }

        // Update()
        private void Update()
        {
            // 매 프레임마다 상태를 평가한다.
            rootNode.Evaluate();
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
                        // 1. ??? 노드
                        // new ???(_???);
                    })
                })
            });

            // 작성한 노드를 반환한다.
            return node;
        }
    }
}