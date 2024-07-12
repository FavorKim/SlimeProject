using UnityEngine;

namespace Player
{
    public class LocomotionState : SlimeBaseState
    {
        // 슬라임(플레이어) 클래스 및 부착 컴포넌트
        private readonly SlimeController _controller;
        private readonly Animator _animator;

        // 생성자
        public LocomotionState(SlimeController controller)
        {
            _controller = controller;
            controller.TryGetComponent(out _animator);
        }



        public override void Enter()
        {

        }

        public override void Execute()
        {

        }

        public override void Exit()
        {

        }




        // 다른 상태로 전환할 수 있는 조건?
        // 이동 중 이동, 대시, 공중에 놓임, 들어올림, 사망 및 부활이 가능하다.
        // 대시 중 이동, 대시는 무시되며 공중에 놓임, 들어올림, 사망 및 부활이 가능하다.


    }
}
