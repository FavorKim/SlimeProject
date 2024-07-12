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
        [SerializeField] private float _movePower; // 좌우로 움직이는 힘
        private Vector2 inputVector; // 이동에 대한 입력 값

        [SerializeField] private float _jumpPower; // 점프하는 힘
        [SerializeField] private Transform _groundChecker; // 땅에 닿아 있는지를 판별하기 위한 빈 게임 오브젝트

        #endregion 변수

        #region 생명 주기 함수

        // Awake()
        private void Awake()
        {
            // 컴포넌트를 초기화한다.
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


            // 이동에 대한 입력 값을 읽어, 그 방향으로 힘을 가해 이동을 구현한다.
            Vector3 moveVector = inputVector * _movePower;
            _rigidbody.AddForce(moveVector, ForceMode.VelocityChange);

            // 이동에 속도를 제한한다. 단, 이동의 입력이 있을 때만 제한하여 관성을 유지할 수 있도록 한다.
            if (moveVector != Vector3.zero)
            {
                _rigidbody.velocity = ClampMoveVelocity(_rigidbody, _movePower);
            }
        }

        #endregion 생명 주기 함수

        #region 인풋 시스템(Input System)

        // 방향 키(A/D, ←/→ 등)를 눌러, 플레이어를 좌우로 움직이게 한다.
        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            inputVector = callbackContext.ReadValue<Vector2>();
        }

        // 점프 키(Space)를 눌러, 플레이어를 뛰어오르게 한다.
        public void OnJump(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                // 점프는 땅에 닿아 있을 때만 실행할 수 있다.
                if (IsGround(_groundChecker))
                {
                    // 위로 힘을 가해 점프를 구현한다.
                    Vector3 jumpVector = Vector3.up * _jumpPower;
                    _rigidbody.AddForce(jumpVector, ForceMode.VelocityChange);
                }
            }
        }

        #endregion 인풋 시스템(Input System)

        #region 커스텀 함수

        // 상태를 바꾼다.
        public void ChangeState(IState state)
        {
            _state?.Exit(); // 이전의 상태를 나가고,
            _state = state; // 새로운 상태로 바꾸고,
            _state?.Enter(); // 바꾼 상태에 들어간다.
        }

        // 움직임에 대한 최대 속도를 제한한다.
        private Vector3 ClampMoveVelocity(Rigidbody rigidbody, float maxPower)
        {
            // Rigidbody의 속도 값을 받아, X축의 속도를 제한한다.
            Vector3 clampedVelocity = rigidbody.velocity;
            clampedVelocity.x = Mathf.Clamp(clampedVelocity.x, -maxPower, maxPower);
            
            // 그 값을 반환한다.
            return clampedVelocity;
        }

        // 플레이어가 땅에 닿아 있는지를 판별한다.
        private bool IsGround(Transform groundChecker)
        {
            float maxDistance = 0.2f; // Ray로 땅을 판별할 깊이
            int groundLayerMask = 1 << LayerMask.NameToLayer("Ground"); // 땅으로 설정한 레이어

            // Physics.Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask);
            // origin에서 direction으로 maxDistance 거리만큼 Ray를 쏘아, layerMask가 있는지를 검출한다.
            bool isGround = Physics.Raycast(groundChecker.position, Vector3.down, maxDistance, groundLayerMask);

            return isGround;
        }

        #endregion 커스텀 함수
    }
}
