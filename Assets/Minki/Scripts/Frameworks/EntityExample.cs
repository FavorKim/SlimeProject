using BehaviourTree;
using StatePattern;
using System.Collections.Generic;
using UnityEngine;

public class EntityExample : MonoBehaviour
{
    // 상태 패턴, 행동 트리 등의 디자인을 사용하는 예제 클래스

    #region 생명 주기 함수

    // Awake()
    private void Awake()
    {
        // 1. 상태 패턴
        // 시작할 때 첫 기본 상태를 부여해야 한다.
        // ChangeState(new ???());


        // 2. 행동 트리
        // 생성 시 자신의 ???(를 상속받는 클래스) 스크립트를 컴포넌트에서 참조한다.
        TryGetComponent(out _entity);

        // 행동 트리를 작성한다.
        rootNode = SetBehaviourTree();
    }

    // Update()
    private void Update()
    {
        // 1. 상태 패턴
        // 지금 상태에 대한 행동을 실행한다.
        _state?.Execute();


        // 2. 행동 트리
        // 매 프레임마다 상태를 평가한다.
        rootNode.Evaluate();
    }

    #endregion 생명 주기 함수

    #region 상태 패턴

    // 상태 패턴 인터페이스
    private IState _state;

    // 상태를 바꾸는 함수; 각 상태 클래스에서 접근하여 호출한다.
    public void ChangeState(IState state)
    {
        _state?.Exit(); // 현재 상태를 나가고,
        _state = state; // 상태를 바꾸고,
        _state?.Enter(); // 바꾼 상태에 들어간다.
    }

    #endregion 상태 패턴

    #region 행동 트리

    // 행동 트리를 사용하는 클래스
    private EntityExample _entity;

    // 뿌리(Root) 노드
    private Node rootNode;

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
                        // new ???(_entity);
                    })
                })
            });

        // 작성한 노드를 반환한다.
        return node;
    }

    #endregion 행동 트리

}
