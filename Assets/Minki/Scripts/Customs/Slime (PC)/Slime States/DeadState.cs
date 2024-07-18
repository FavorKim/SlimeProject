using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    // 죽을 때의 상태 클래스
    public class DeadState : SlimeBaseState
    {
        // 슬라임(플레이어) 클래스 및 부착 컴포넌트
        protected readonly SlimeController _controller;
        protected readonly SlimeConfiguration _configuration;
        protected readonly Animator _animator;

        // 공격한 오브젝트의 타입
        protected bool _attackedObjectType;

        // 생성자
        public DeadState(SlimeController controller, ObjectBase obj)
        {
            #region 변수 초기화

            _controller = controller;
            _configuration = _controller.Configuration;

            _controller.TryGetComponent(out _animator);

            // _AttackedObjectType = obj.Pinable;

            #endregion 변수 초기화
        }

        #region 상태 패턴 인터페이스 함수

        // 상태에 들어갈 때,
        public override void Enter()
        {
            // 컨트롤러를 비활성화한다.
            SetActiveController(false, _controller);

            // 죽는 애니메이션을 재생한다.
            _animator.SetBool(trapIndex_AnimatorHash, _attackedObjectType);
            _animator.SetTrigger(hit_AnimatorHash);

            // 레이어를 상호 작용이 가능한 레이어로 바꾼다.
            /*
            foreach (Transform obj in _controller.GetComponentsInChildren<Transform>())
            {
                obj.gameObject.layer = LayerMask.NameToLayer("Interactable");
            }
            */

            // 부활에 돌입한다.
            _controller.StartCoroutine(IERevive(_controller, _configuration));


            // [TODO]: 슬라임의 표정을 X_X로 변경한다. 리스폰 지역으로 이동한다.
        }

        // 상태를 유지할 때,
        public override void Execute()
        {

        }

        // 상태를 나갈 때,
        public override void Exit()
        {

        }

        #endregion 상태 패턴 인터페이스 함수

        #region 유니티 이벤트 함수

        

        #endregion 유니티 이벤트 함수

        #region 커스텀 함수

        // 컨트롤러를 비활성화한다.
        private void SetActiveController(bool isActive, SlimeController controller)
        {
            // SetActive/activeSelf는 게임 오브젝트 자체를 (비)활성화한다면, enabled속성은 게임 오브젝트의 컴포넌트를 (비)활성화한다.
            // 슬라임이 죽어도, 조작만 불가능할 뿐 객체 자체는 살아서 게임 진행에 영향을 주어야 한다.
            controller.enabled = isActive;

            // Player Input 컴포넌트도 (비)활성화한다.
            controller.gameObject.TryGetComponent(out PlayerInput playerInput);
            playerInput.enabled = isActive;
        }

        // 일정 시간 후, 부활한다.
        private IEnumerator IERevive(SlimeController controller, SlimeConfiguration configuration)
        {
            // 부활하기까지의 대기 시간이 존재한다.
            yield return new WaitForSeconds(configuration.TimeToRevive);

            // 시체 프리팹을 생성한다.
            Object.Instantiate(configuration.DeadPrefab, controller.RigTransform.position, Quaternion.identity);

            Debug.Log($"Check Point = {CheckPointManager.Instance.GetNearestCheckPoint()}");

            // 본체를 가장 가까운 체크 포인트로 이동하고, 컨트롤러를 활성화하고, 살아 있는 상태로 바꾼다.
            controller.gameObject.transform.position = CheckPointManager.Instance.GetNearestCheckPoint();
            SetActiveController(true, controller);
            _animator.SetTrigger(revive_AnimatorHash);
            controller.ChangeState(new GroundState(controller, Vector2.zero));
        }

        #endregion 커스텀 함수
    }

}
