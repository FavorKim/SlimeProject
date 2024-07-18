using StatePattern;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    // 슬라임(플레이어)의 상태를 표현하는 최상위 추상 클래스
    public abstract class SlimeBaseState : IState
    {
        // 애니메이터의 매개변수
        protected static int move_AnimatorHash = Animator.StringToHash("isMove");
        protected static int ground_AnimatorHash = Animator.StringToHash("isGround");
        protected static int special_AnimatorHash = Animator.StringToHash("Special");
        protected static int hit_AnimatorHash = Animator.StringToHash("Hit");
        protected static int revive_AnimatorHash = Animator.StringToHash("Revive");
        protected static int trapIndex_AnimatorHash = Animator.StringToHash("TrapIndex");


        // 상태 패턴의 인터페이스 함수
        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();

        public virtual void FixedExecute() { }
        public virtual void OnInputCallback(InputAction.CallbackContext callbackContext) { }

        public virtual void OnTriggerEnter(Collider other) { }
        public virtual void OnCollisionEnter(Collision collision) { }
    }
}
