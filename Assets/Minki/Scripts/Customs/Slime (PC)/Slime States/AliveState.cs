using StatePattern;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    // 살아 있을 때의 상태 클래스; 지상 및 공중 상태의 부모 클래스
    public class AliveState : SlimeBaseState
    {
        // 슬라임(플레이어) 클래스 및 부착 컴포넌트
        protected readonly SlimeController _controller;
        protected readonly SlimeConfiguration _configuration;
        protected readonly Animator _animator;
        protected readonly Rigidbody _rigidbody;

        // 변수
        protected Vector2 _inputVector = Vector2.zero; // 움직임 관련
        protected readonly Transform _groundChecker; // 점프 관련
        protected bool _isDashing = false; // 대시 관련
        protected readonly Transform _objectChecker; // 들어올리기 / 내려놓기 관련
        protected readonly Transform _liftPosition;
        protected bool _isLifting = false;

        // 생성자
        public AliveState(SlimeController controller, Vector2 inputVector)
        {
            #region 변수 초기화

            _controller = controller;

            _configuration = _controller.Configuration;
            _groundChecker = _controller.GroundChecker;
            _objectChecker = _controller.ObjectChecker;
            _liftPosition = _controller.LiftPosition;

            _controller.TryGetComponent(out _animator);
            _controller.TryGetComponent(out _rigidbody);

            _inputVector = inputVector;

            #endregion 변수 초기화
        }

        #region 상태 패턴 인터페이스 함수

        // 상태에 들어갈 때,
        public override void Enter()
        {
            // 인풋 시스템에 대한 이벤트를 등록한다.
            _controller.BindInputCallback(true, OnInputCallback);
        }

        // 상태를 나갈 때,
        public override void Exit()
        {
            // 인풋 시스템에 대한 이벤트를 해제한다.
            _controller.BindInputCallback(false, OnInputCallback);
        }

        // 상태를 유지할 때, (Update)
        public override void Execute()
        {
            // 횡스크롤로서의 위치를 고정한다.
            _controller.transform.position = FixPositionToSideView(_controller.transform);
        }

        // 상태를 유지할 때, (FixedUpdate)
        public override void FixedExecute()
        {
            // 이동을 실행한다. Rigidbody의 속성을 사용하여 움직이므로, FixedUpdate에서 호출한다.
            if (_inputVector != Vector2.zero)
            {
                Move(_rigidbody, _inputVector, _configuration.MoveSpeed);
                _animator.SetBool(move_AnimatorHash, true);
            }
            else
            {
                _animator.SetBool(move_AnimatorHash, false);
            }
        }

        // 인풋 시스템으로부터 입력을 전달받아 행동을 실행한다.
        public override void OnInputCallback(InputAction.CallbackContext callbackContext)
        {
            // 움직임과 관련한 입력이 들어올 경우,
            if (callbackContext.action.name == Enum.GetName(typeof(InputName), InputName.MOVE).ToTitleCase())
            {
                // 입력 값을 Vector2로 읽어들인다.
                _inputVector = callbackContext.ReadValue<Vector2>();
            }

            // 점프와 관련한 입력이 들어올 경우,
            if (callbackContext.action.name == Enum.GetName(typeof(InputName), InputName.JUMP).ToTitleCase())
            {
                // 점프를 실행한다.
                Jump(_rigidbody, _configuration.JumpPower);
            }

            // 대시와 관련한 입력이 들어올 경우,
            if (callbackContext.action.name == Enum.GetName(typeof(InputName), InputName.DASH).ToTitleCase())
            {
                // 대시를 실행한다.
                Dash(_rigidbody, _configuration.DashPower);
            }

            // 들어올리기와 관련한 입력이 들어올 경우,
            if (callbackContext.action.name == Enum.GetName(typeof(InputName), InputName.LIFT).ToTitleCase())
            {
                // 들어올리기를 실행한다.
                Lift();
            }

            // 내려놓기와 관련한 입력이 들어올 경우,
            if (callbackContext.action.name == Enum.GetName(typeof(InputName), InputName.PUT).ToTitleCase())
            {
                // 내려놓기를 실행한다.
                Put();
            }
        }

        #endregion 상태 패턴 인터페이스 함수

        #region 커스텀 함수

        // 플레이어를 입력에 따라 움직이게 한다.
        private void Move(Rigidbody rigidbody, Vector2 inputVector, float moveSpeed)
        {
            // 플레이어를 입력 방향으로 회전시킨다.
            TurnToForward(rigidbody, inputVector);

            // 대시하고 있지 않을 때만,
            if (!_isDashing)
            {
                // 입력 방향으로 힘을 가해 이동을 구현한다.
                Vector3 moveVector = inputVector * moveSpeed;
                rigidbody.AddForce(moveVector, ForceMode.VelocityChange);

                // 이동에 속도를 제한한다.
                rigidbody.velocity = ClampVelocity(rigidbody, moveSpeed);
            }
        }

        // 플레이어를 입력에 따라 뛰어오르게 한다.
        private void Jump(Rigidbody rigidbody, float jumpPower)
        {
            // 땅에 닿아 있을 때만 점프할 수 있다.
            if (IsGround(_groundChecker))
            {
                // 위로 힘을 가해 점프를 구현한다.
                Vector3 jumpVector = Vector3.up * jumpPower;
                rigidbody.AddForce(jumpVector, ForceMode.VelocityChange);

                // 점프 애니메이션을 재생한다. (공중 상태에서 구현할 경우, 점프를 통한 진입이 아닐 때도 애니메이션이 재생되므로 어색하다.)
                _animator.SetBool(ground_AnimatorHash, false);
            }
        }

        // 플레이어를 대시시킨다.
        protected virtual void Dash(Rigidbody rigidbody, float dashPower)
        {
            if (!_isDashing)
            {
                _controller.StartCoroutine(IEDash(rigidbody, dashPower));
            }
        }

        // 상호작용이 가능한 오브젝트를 들어올린다.
        private void Lift()
        {
            // 이미 들어올린 오브젝트가 있다면,
            if (_isLifting)
            {
                // 그 오브젝트를 던진다.
                Throw();
            }
            // 아니라면,
            else
            {
                // 들어올리기 애니메이션을 재생한다.
                _animator.SetTrigger(special_AnimatorHash);

                // 앞에 상호작용이 가능한 오브젝트가 있는지 확인하고, 있을 경우 그 물체를 들어올린다.
                if (Physics.Raycast(origin: _objectChecker.position, direction: _controller.transform.forward, maxDistance: 1.0f, hitInfo: out RaycastHit hitInfo, layerMask: 1 << LayerMask.NameToLayer("Interactable")))
                {
                    hitInfo.transform.position = _liftPosition.position;
                    //hitInfo.transform.SetParent(_liftPosition.transform);
                    hitInfo.rigidbody.isKinematic = true;
                    //hitInfo.rigidbody.useGravity = false;
                }
            }
        }

        // 들어올린 오브젝트가 있을 경우, 그것을 던진다.
        private void Throw()
        {

        }

        // 들어올린 오브젝트를 내려놓는다.
        private void Put()
        {
            _animator.SetTrigger(special_AnimatorHash);
        }

        // 플레이어를 움직이는 방향으로 회전시킨다.
        private void TurnToForward(Rigidbody rigidbody, Vector2 inputVector)
        {
            // 플레이어는 기본적으로 오른쪽을 바라본다.
            if (inputVector.x < 0)
            {
                rigidbody.rotation = Quaternion.Euler(0, -90, 0);
            }
            else if (inputVector.x > 0)
            {
                rigidbody.rotation = Quaternion.Euler(0, 90, 0);
            }
        }

        // 좌우로의 움직임에 대한 최대 속도를 제한한다.
        private Vector3 ClampVelocity(Rigidbody rigidbody, float maxPower)
        {
            // Rigidbody의 속도 값을 받아, X축의 속도를 제한한다.
            Vector3 clampedVelocity = rigidbody.velocity;
            clampedVelocity.x = Mathf.Clamp(clampedVelocity.x, -maxPower, maxPower);

            // 그 결과 값을 반환한다.
            return clampedVelocity;
        }

        // 플레이어가 땅에 닿아 있는지를 판별한다.
        protected bool IsGround(Transform groundChecker)
        {
            float maxDistance = 0.35f; // Ray로 땅을 판별할 깊이
            int groundLayerMask = 1 << LayerMask.NameToLayer("Ground"); // 땅으로 설정한 레이어
            int interactableLayerMask = 1 << LayerMask.NameToLayer("Interactable"); // 상호 작용이 가능한 오브젝트들

            // Physics.Raycast(Vector3 origin, Vector3 direction, float maxDistance, int layerMask);
            // origin에서 direction으로 maxDistance 거리만큼 Ray를 쏘아, layerMask가 있는지를 검출한다.
            bool isGround = Physics.Raycast(groundChecker.position, Vector3.down, maxDistance, groundLayerMask);
            bool isInteractable = Physics.Raycast(groundChecker.position, Vector3.down, maxDistance, interactableLayerMask);
            Debug.DrawRay(groundChecker.position, Vector3.down, Color.red);

            // 그 결과 값을 반환한다. (어느 하나라도 있을 경우, 바닥으로 간주한다.)
            return isGround || isInteractable;
        }

        // 대시의 올바른 구현을 위한 코루틴 함수
        private IEnumerator IEDash(Rigidbody rigidbody, float dashPower)
        {
            _isDashing = true;

            Debug.Log("IEDash!");

            // 바라보고 있는 방향으로 순간적인 힘을 가한다.
            Vector3 dashVector = dashPower * rigidbody.transform.forward;
            rigidbody.velocity = Vector3.zero; // 이전의 물리 영향을 무시하고 대시한다.
            rigidbody.AddForce(dashVector, ForceMode.VelocityChange);

            // 일정 시간 동안만 대시 효과를 받는다.
            yield return new WaitForSeconds(0.2f);

            // 대시 후, 원래 속도로 돌아온다.
            rigidbody.velocity = ClampVelocity(rigidbody, _configuration.MoveSpeed);
            _isDashing = false;
        }

        // 횡스크롤 게임으로서, 종열의 위치를 벗어나지 않도록 고정한다.
        private Vector3 FixPositionToSideView(Transform transform)
        {
            Vector3 fixedPosition = new Vector3(transform.position.x, transform.position.y, 0);
            return fixedPosition;
        }

        #endregion 커스텀 함수
    }
}
