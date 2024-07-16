using StatePattern;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    // 공중에 떠 있을 때의 상태 클래스
    public class AirState : AliveState
    {
        // 변수
        private int _airDashCount = 0;

        // 생성자
        public AirState(SlimeController controller, Vector2 inputVector) : base(controller, inputVector) { }

        #region 상태 패턴 인터페이스 함수

        // 상태에 들어올 때,
        public override void Enter()
        {
            base.Enter();
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

            // 만약 지금 땅에 닿아 있다면,
            if (IsGround(_groundChecker))
            {
                // 지상 상태에 들어간다.
                _controller.ChangeState(new GroundState(_controller, _inputVector));
            }
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

        // 플레이어를 대시시킨다.
        protected override void Dash(Rigidbody rigidbody, float dashPower)
        {
            // 공중 상태에서는 대시 횟수에 제한이 있다.
            if (_airDashCount < _configuration.AirDashMaxCount)
            {
                base.Dash(rigidbody, dashPower);
                _airDashCount++;
            }
        }

        #endregion 커스텀 함수
    }

}
