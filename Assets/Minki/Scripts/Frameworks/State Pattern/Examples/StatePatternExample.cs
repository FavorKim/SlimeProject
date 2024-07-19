using UnityEngine;
using UnityEngine.InputSystem;

namespace StatePattern
{
    // 상태 클래스들의 최상위 추상 클래스
    public abstract class BaseStateExample : IState
    {
        // 상태 패턴을 사용하는 클래스
        private StatePatternExample _example;

        // 생성자
        public BaseStateExample(StatePatternExample example)
        {
            _example = example;
        }

        // 인터페이스 함수
        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();

        public virtual void FixedExecute() { }
        public virtual void OnInputCallback(InputAction.CallbackContext callbackContext) { }

        public virtual void OnTriggerEnter(Collider other) { }
        public virtual void OnCollisionEnter(Collision collision) { }
    }

    // 상태를 구현하는 클래스
    public class StatePatternExample : MonoBehaviour
    {
        // 상태 패턴 인터페이스
        private IState _state;

        // Awake()
        private void Awake()
        {
            // 시작할 때 첫 기본 상태를 부여해야 한다.
            // ChangeState(new ???());
        }

        // Update()
        private void Update()
        {
            // 지금 상태에 대한 행동을 실행한다.
            _state?.Execute();
        }

        // 상태를 바꾸는 함수; 각 상태 클래스에서 접근하여 호출한다.
        public void ChangeState(IState state)
        {
            _state?.Exit(); // 현재 상태를 나가고,
            _state = state; // 상태를 바꾸고,
            _state?.Enter(); // 바꾼 상태에 들어간다.
        }
    }
}
