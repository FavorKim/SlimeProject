using StatePattern;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    // 슬라임(플레이어)의 조작을 담당하는 클래스
    public class SlimeController : MonoBehaviour
    {
        #region 변수

        // 슬라임의 필요 변수들을 관리하는 클래스
        [SerializeField] private SlimeConfiguration _configuration;
        public SlimeConfiguration Configuration => _configuration;

        // 슬라임이 지상에 있는지를 판별하기 위한 Transform 변수
        [SerializeField] private Transform _groundChecker;
        public Transform GroundChecker => _groundChecker;

        // 슬라임이 물체를 들어올리기 위한 Transform 변수
        [SerializeField] private Transform _objectChecker;
        [SerializeField] private Transform _liftPosition;
        public Transform ObjectChecker => _objectChecker;
        public Transform LiftPosition => _liftPosition;

        // 슬라임의 부활 위치를 정확하게 하기 위한 Transform 변수
        [SerializeField] private Transform _rigTransform;
        public Transform RigTransform => _rigTransform;

        // 상태 인터페이스
        private IState _state;

        // 인풋 시스템 함수의 호출을 다른 상태 클래스에 전달하기 위한 이벤트(Event)
        private event Action<InputAction.CallbackContext> InputCallback;

        private int _healthPoint;
        public int HealthPoint { get; set; }

        #endregion 변수

        #region 생명 주기 함수

        // Awake()
        private void Awake()
        {
            _healthPoint = _configuration.MaxHealthPoint;

            // 첫 시작 시, Ground 상태에 들어간다.
            ChangeState(new GroundState(this, Vector2.zero));
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
            // 지금 상태에 따른 행동을 실행한다.
            _state.FixedExecute();
        }

        #endregion 생명 주기 함수

        #region 유니티 이벤트 함수

        // 피격당했을 경우,
        private void OnTriggerEnter(Collider other)
        {
            _state.OnTriggerEnter(other);
        }

        private void OnCollisionEnter(Collision collision)
        {
            _state.OnCollisionEnter(collision);
        }

        #endregion 유니티 이벤트 함수

        #region 인풋 시스템(Input System)

        // 방향 키(A/D, ←/→ 등)를 눌러, 플레이어를 좌우로 움직이게 한다.
        public void OnMove(InputAction.CallbackContext callbackContext)
        {
            // InputCallback.Invoke(callbackContext);

            _state.OnInputCallback(callbackContext);
        }

        // 점프 키(Space)를 눌러, 플레이어를 뛰어오르게 한다.
        public void OnJump(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                InputCallback.Invoke(callbackContext);
            }
        }

        // 대시 키(Shift)를 눌러, 플레이어를 순간적으로 힘을 가해 움직이게 한다.
        public void OnDash(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                InputCallback.Invoke(callbackContext);
            }
        }

        // 들어올리기 키(Z)를 눌러, 플레이어의 앞에 있는 상호 작용이 가능한 물체를 들어올린다.
        public void OnLift(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                InputCallback.Invoke(callbackContext);
            }
        }

        // 내려놓기 키(X)를 눌러, 플레이어가 들어올린 물체를 내려놓는다.
        public void OnPut(InputAction.CallbackContext callbackContext)
        {
            if (callbackContext.performed)
            {
                InputCallback.Invoke(callbackContext);
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

        // 상태 클래스가 인풋 시스템 함수에 접근하기 위해 이벤트에 등록/해제한다.
        public void BindInputCallback(bool isBind, Action<InputAction.CallbackContext> callbackContext)
        {
            if (isBind)
            {
                InputCallback += callbackContext;
            }
            else
            {
                InputCallback -= callbackContext;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawCube(_groundChecker.position, new Vector3(0.6f, 0.1f, 0.6f));
        }

        #endregion 커스텀 함수
    }
}
