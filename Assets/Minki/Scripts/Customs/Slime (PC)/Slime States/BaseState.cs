using StatePattern;
using UnityEngine;

namespace Player
{
    // 슬라임(플레이어)의 상태를 표현하는 최상위 추상 클래스
    public abstract class SlimeBaseState : IState
    {
        // 애니메이터의 매개변수
        protected static int standby_AnimatorHash = Animator.StringToHash("Standby");
        protected static int idle_AnimatorHash = Animator.StringToHash("Idle");
        protected static int move_AnimatorHash = Animator.StringToHash("Move");
        protected static int moveSP_AnimatorHash = Animator.StringToHash("MoveSP");
        protected static int actSP_AnimatorHash = Animator.StringToHash("ActSP");
        protected static int hit_AnimatorHash = Animator.StringToHash("Hit");

        // 상태 패턴의 인터페이스 함수
        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();
    }
}
