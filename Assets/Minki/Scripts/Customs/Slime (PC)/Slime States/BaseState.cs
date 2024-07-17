using StatePattern;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    // 슬라임(플레이어)의 상태를 표현하는 최상위 추상 클래스
    public abstract class SlimeBaseState : IState
    {
        // 애니메이터의 매개변수
        // protected static int standby_AnimatorHash = Animator.StringToHash("Standby");
        // protected static int idle_AnimatorHash = Animator.StringToHash("Idle");
        protected static int move_AnimatorHash = Animator.StringToHash("isMove");
        // protected static int moveSP_AnimatorHash = Animator.StringToHash("MoveSP");
        // protected static int actSP_AnimatorHash = Animator.StringToHash("ActSP");
        protected static int hit_AnimatorHash = Animator.StringToHash("Hit");
        // protected static int die_AnimatorHash = Animator.StringToHash("Die");
        protected static int ground_AnimatorHash = Animator.StringToHash("isGround");
        protected static int lift_AnimatorHash = Animator.StringToHash("Lift");

        // 상태 패턴의 인터페이스 함수
        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();

        public virtual void FixedExecute() { }
        public virtual void OnInputCallback(InputAction.CallbackContext callbackContext) { }
    }
}
