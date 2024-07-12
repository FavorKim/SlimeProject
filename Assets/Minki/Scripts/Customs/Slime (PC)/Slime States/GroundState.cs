using StatePattern;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    // 땅에 닿아 있을 때의 상태 클래스
    public class GroundState : SlimeBaseState
    {
        // 슬라임(플레이어) 클래스 및 부착 컴포넌트
        private readonly SlimeController _controller;
        private readonly Animator _animator;
        private readonly Rigidbody _rigidbody;

        // 변수
        private float _time; // Idle 애니메이션 전환 관련

        private Vector2 _inputVector; // 움직임 관련
        private float _movePower;

        private Transform _groundChecker; // 점프 관련
        private float _jumpPower;

        // 생성자
        public GroundState(SlimeController controller)
        {
            _controller = controller;
            controller.TryGetComponent(out _animator);
            controller.TryGetComponent(out _rigidbody);

            _movePower = _controller.MovePower;
            _jumpPower = _controller.JumpPower;
            _groundChecker = _controller.GroundChecker;
        }

        #region 상태 패턴 인터페이스 함수

        // 상태에 들어올 때,
        public override void Enter()
        {
            // Debug.Log("GroundState Enter!");

            // 인풋 시스템에 대한 이벤트를 등록한다.
            _controller.BindInputCallback(true, OnInputCallback);

            // Standby 애니메이션을 재생한다.
            // _animator.SetBool(standby_AnimatorHash, true);
        }

        // 상태를 유지할 때,
        public override void Execute()
        {
            // Debug.Log($"GroundState Execute!");

            // Standby 상태에서, 일정 시간 이상 아무것도 하지 않을 경우 Idle(AFK; Away From Keyboard) 애니메이션을 재생한다.
            // CheckToIdle(_time, _animator);
        }

        // 상태를 나갈 때,
        public override void Exit()
        {
            // Debug.Log("GroundState Exit!");

            // 인풋 시스템에 대한 이벤트를 해제한다.
            _controller.BindInputCallback(false, OnInputCallback);
        }

        public override void FixedExecute()
        {
            // Debug.Log("GroundState FixedExecute!");

            // 이동에 대한 입력 값을 읽어, 그 방향으로 힘을 가해 이동을 구현한다.
            Vector3 moveVector = _inputVector * _movePower;
            _rigidbody.AddForce(moveVector, ForceMode.VelocityChange);

            // 이동에 속도를 제한한다. 단, 이동의 입력이 있을 때만 제한하여 관성을 유지할 수 있도록 한다.
            if (moveVector != Vector3.zero)
            {
                _rigidbody.velocity = ClampMoveVelocity(_rigidbody, _movePower);
            }
        }

        // 인풋 시스템으로부터 입력을 전달받아 행동을 실행한다.
        public override void OnInputCallback(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("GroundState OnInputCallback!");

            if (callbackContext.action.name == Enum.GetName(typeof(InputName), InputName.Move).ToTitleCase())
            {
                Debug.Log("Move!");

                _inputVector = callbackContext.ReadValue<Vector2>();
            }
            
            if (callbackContext.action.name == Enum.GetName(typeof(InputName), InputName.Jump).ToTitleCase())
            {
                Debug.Log("Jump!");

                // 점프는 땅에 닿아 있을 때만 실행할 수 있다.
                if (IsGround(_groundChecker))
                {
                    // 위로 힘을 가해 점프를 구현한다.
                    Vector3 jumpVector = Vector3.up * _jumpPower;
                    _rigidbody.AddForce(jumpVector, ForceMode.VelocityChange);
                }
            }
        }

        #endregion 상태 패턴 인터페이스 함수

        #region 커스텀 함수

        // 현재 시간이 Idle 애니메이션을 재생할 만큼 지났는지를 확인한다.
        private void CheckToIdle(float time, Animator animator)
        {
            // 만약 Standby 애니메이션이 유지되지 않았다면,
            if (animator.GetBool(standby_AnimatorHash) == false)
            {
                // 시간을 재측정한다.
                _time = Time.time;
            }

            // Idle 애니메이션으로의 전환 시간; 약 10초로 지정한다.
            float standbyTime = 10.0f;

            // 대기 시간이 전환 시간을 초과했을 경우,
            if (Time.time > time + standbyTime)
            {
                // Idle 애니메이션을 재생한다.
                PlayIdleAnimation(animator);
            }
        }

        // Idle 애니메이션을 재생한다.
        // 비고: 다시 Standby 애니메이션으로 전환하는 것은 애니메이터에서 Exit Time을 사용하여 직접 관리한다.
        private void PlayIdleAnimation(Animator animator)
        {
            // Idle(AFK) 애니메이션을 재생하고,
            _animator.SetTrigger(idle_AnimatorHash);

            // 저장된 시간을 초기화한다.
            _time = Time.time;
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
