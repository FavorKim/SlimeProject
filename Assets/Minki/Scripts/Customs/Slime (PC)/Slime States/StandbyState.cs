using UnityEngine;

namespace Player
{
    // 기본(Standby) 상태를 표현하는 클래스
    public class StandbyState : SlimeBaseState
    {
        // 슬라임(플레이어) 클래스 및 부착 컴포넌트
        private readonly SlimeController _controller;
        private readonly Animator _animator;

        // 변수
        private float _time;

        // 생성자
        public StandbyState(SlimeController controller)
        {
            _controller = controller;
            controller.TryGetComponent(out _animator);
        }

        // 상태에 진입 시,
        public override void Enter()
        {
            // Debug.Log("StandbyState: Enter!");

            // Standby 애니메이션을 재생한다.
            // _animator.SetBool(standby_AnimatorHash, true);

            // 진입 시 현재 시간을 저장해 둔다.
            _time = Time.time;
        }

        // 상태를 유지 시,
        public override void Execute()
        {
            // Debug.Log("StandbyState: Execute!");

            // [TODO]: 이동, 점프, 피격 등의 반응이 있는지 확인하고, 전환을 실행한다.
            CheckStateChange();

            // 전환하지 않았다면, Idle(AFK; Away From Keyboard) 애니메이션의 재생 여부를 확인한다.
            // PlayIdleAnimation();
        }

        // 상태를 탈출 시,
        public override void Exit()
        {
            // Debug.Log("StandbyState: Exit!");

            // Standby 애니메이션을 정지한다.
            // _animator.SetBool(standby_AnimatorHash, false);
        }

        private void CheckStateChange()
        {

        }

        // Idle(AFK; Away From Keyboard) 애니메이션의 재생 여부를 확인 및 재생한다.
        // 비고: 다시 Standby 애니메이션으로 전환하는 것은 애니메이터에서 Exit Time을 사용하여 직접 관리한다.
        private void PlayIdleAnimation()
        {
            // Idle 애니메이션으로의 전환 시간; 약 10초로 지정한다.
            float standbyTime = 10.0f;

            // 대기 시간이 전환 시간을 초과했을 경우,
            if (Time.time > _time + standbyTime)
            {
                // Idle(AFK) 애니메이션을 재생하고,
                _animator.SetTrigger(idle_AnimatorHash);

                // 저장된 시간을 초기화한다.
                _time = Time.time;
            }
        }
    }
}
