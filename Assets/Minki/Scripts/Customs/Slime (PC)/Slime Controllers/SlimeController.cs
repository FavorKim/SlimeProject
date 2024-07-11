using StatePattern;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    // 슬라임(플레이어)의 조작을 담당하는 클래스
    public class SlimeController : MonoBehaviour
    {
        #region 변수

        // 컴포넌트
        private Rigidbody _rigidbody;

        // 상태 인터페이스
        private IState _state;

        // 변수
        [SerializeField] private float _movePower;
        [SerializeField] private float _jumpPower;
        private Vector2 inputVector;
        private bool isJumping;

        #endregion 변수

        #region 생명 주기 함수

        // Awake()
        private void Awake()
        {
            TryGetComponent(out _rigidbody);

            // 첫 시작 시, Standby 상태에 들어간다.
            ChangeState(new StandbyState(this));
        }

        // Update()
        private void Update()
        {
            // 지금 상태에 따른 행동을 실행한다.
            _state.Execute();
        }

        // FixedUpdate(); 물리(Rigidbody)와 관련한 작업은 FixedUpdate()에서 할 것을 권장한다.
        private void FixedUpdate()
        {
            _rigidbody.AddForce(inputVector * _movePower);
        }

        #endregion 생명 주기 함수

        #region 인풋 시스템(Input System)

        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            inputVector = callbackContext.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                _rigidbody.AddForce(Vector3.up * _jumpPower);
            }
        }

        #endregion 인풋 시스템(Input System)

        // 상태를 바꾼다.
        public void ChangeState(IState state)
        {
            _state?.Exit(); // 이전의 상태를 나가고,
            _state = state; // 새로운 상태로 바꾸고,
            _state?.Enter(); // 바꾼 상태에 들어간다.
        }
    }
}
