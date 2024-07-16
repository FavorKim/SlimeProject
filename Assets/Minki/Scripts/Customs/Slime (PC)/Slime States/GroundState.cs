using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    // 땅에 닿아 있을 때의 상태 클래스
    public class GroundState : AliveState
    {
        // 변수
        private float _time = 0; // Idle 애니메이션 전환 관련

        // 생성자
        public GroundState(SlimeController controller, Vector2 inputVector) : base(controller, inputVector) { }

        #region 상태 패턴 인터페이스 함수

        // 상태에 들어올 때,
        public override void Enter()
        {
            base.Enter();

            // Standby 애니메이션을 재생한다.
            // _animator.SetBool(standby_AnimatorHash, true);
        }

        // 상태를 나갈 때,
        public override void Exit()
        {
            base.Exit();
        }

        // 상태를 유지할 때(Update()),
        public override void Execute()
        {
            base.Execute();

            // 만약 지금 땅에 닿아 있지 않다면,
            if (!IsGround(_groundChecker))
            {
                // 공중 상태에 들어간다.
                _controller.ChangeState(new AirState(_controller, _inputVector));
            }

            // Standby 상태에서, 일정 시간 이상 아무것도 하지 않을 경우 Idle(AFK; Away From Keyboard) 애니메이션을 재생한다.
            // CheckToIdle(_time, _animator);
        }

        // 상태를 유지할 때(FixedUpdate()),
        public override void FixedExecute()
        {
            base.FixedExecute();
        }

        // 인풋 시스템으로부터 입력을 전달받아 행동을 실행한다.
        public override void OnInputCallback(InputAction.CallbackContext callbackContext)
        {
            base.OnInputCallback(callbackContext);
        }

        #endregion 상태 패턴 인터페이스 함수

        #region 커스텀 함수

        // 현재 시간이 Idle 애니메이션을 재생할 만큼 지났는지를 확인한다.
        private void CheckToIdle(float time, Animator animator)
        {
            // 만약 Standby 애니메이션이 유지되지 않았다면,
            if (!animator.GetBool(standby_AnimatorHash))
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

        #endregion 커스텀 함수
    }
}
