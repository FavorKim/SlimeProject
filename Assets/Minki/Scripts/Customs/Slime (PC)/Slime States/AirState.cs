using StatePattern;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    // 공중에 떠 있을 때의 상태 클래스
    public class AirState : SlimeBaseState
    {
        // 슬라임(플레이어) 클래스 및 부착 컴포넌트
        private readonly SlimeController _controller;
        private readonly Animator _animator;
        private readonly Rigidbody _rigidbody;

        // 변수
        private Vector2 _inputVector; // 움직임 관련
        private float _movePower;

        private Transform _groundChecker; // 공중 여부

        // 생성자
        public AirState(SlimeController controller)
        {
            _controller = controller;
            _controller.TryGetComponent(out _animator);
            _controller.TryGetComponent(out _rigidbody);

            // 세밀한 조정이 필요한 변수들은 Inspector 창에서 조절할 수 있게 컨트롤러의 값을 받는 방식으로 구현한다.
            _movePower = _controller.MovePower;
            _groundChecker = _controller.GroundChecker;
        }

        #region 상태 패턴 인터페이스 함수

        // 상태에 들어올 때,
        public override void Enter()
        {
            Debug.Log("AirState Enter!");

            // 인풋 시스템에 대한 이벤트를 등록한다.
            _controller.BindInputCallback(true, OnInputCallback);
        }

        // 상태를 유지할 때(Update()),
        public override void Execute()
        {
            // Debug.Log("AirState Execute!");

            // 만약 지금 땅에 닿아 있다면,
            if (IsGround(_groundChecker))
            {
                // 지상 상태에 들어간다.
                _controller.ChangeState(new GroundState(_controller));
            }
        }

        // 상태를 나갈 때,
        public override void Exit()
        {
            Debug.Log("AirState Exit!");

            // 인풋 시스템에 대한 이벤트를 해제한다.
            _controller.BindInputCallback(false, OnInputCallback);
        }

        // 상태를 유지할 때(FixedUpdate()),
        public override void FixedExecute()
        {
            // Debug.Log("AirState FixedExecute!");

            // 플레이어를 입력에 따라 움직이게 한다. (Rigidbody의 속성을 사용하므로, FixedUpdate()에서 호출한다.)
            Move(_rigidbody, _inputVector, _movePower);
        }

        // 인풋 시스템으로부터 입력을 전달받아 행동을 실행한다.
        public override void OnInputCallback(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("AirState OnInputCallback!");

            // 움직임과 관련한 입력이 들어올 경우,
            if (callbackContext.action.name == Enum.GetName(typeof(InputName), InputName.MOVE).ToTitleCase())
            {
                Debug.Log("Move!");

                // 입력 값을 Vector2로 읽어들인다.
                _inputVector = callbackContext.ReadValue<Vector2>();
            }
        }

        #endregion 상태 패턴 인터페이스 함수

        #region 커스텀 함수

        // 플레이어를 입력에 따라 움직이게 한다.
        private void Move(Rigidbody rigidbody, Vector2 inputVector, float movePower)
        {
            // 이동에 대한 입력 값을 읽어, 그 방향으로 힘을 가해 이동을 구현한다.
            Vector3 moveVector = inputVector * movePower;
            rigidbody.AddForce(moveVector, ForceMode.VelocityChange);

            // 이동에 속도를 제한한다. 단, 이동의 입력이 있을 때만 제한하여 관성을 유지할 수 있도록 한다.
            if (moveVector != Vector3.zero)
            {
                rigidbody.velocity = ClampMoveVelocity(rigidbody, movePower);
            }
        }

        // 움직임에 대한 최대 속도를 제한한다.
        private Vector3 ClampMoveVelocity(Rigidbody rigidbody, float maxPower)
        {
            // Rigidbody의 속도 값을 받아, X축의 속도를 제한한다.
            Vector3 clampedVelocity = rigidbody.velocity;
            clampedVelocity.x = Mathf.Clamp(clampedVelocity.x, -maxPower, maxPower);

            // 그 결과 값을 반환한다.
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

            // 그 결과 값을 반환한다.
            return isGround;
        }

        #endregion 커스텀 함수
    }

}
