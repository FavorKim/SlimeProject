using UnityEngine;

namespace Player
{
    /// <summary>
    /// 슬라임(플레이어)의 설정 값을 관리하는 클래스입니다. 난이도 설계자가 간편하게 인스펙터(Inspector) 창에서 설정할 수 있도록 별도의 클래스로 분리합니다.
    /// </summary>
    public class SlimeConfiguration : MonoBehaviour
    {
        [Header("슬라임의 설정")]

        [Space(10), Header("스테이터스(Status)")]
        [SerializeField, Range(0, 999999), Tooltip("최대 체력")] private int _maxHealthPoint;
        [SerializeField, Range(0.0f, 100.0f), Tooltip("이동 속도")] private float _moveSpeed;
        [SerializeField, Range(0.0f, 100.0f), Tooltip("점프 힘")] private float _jumpPower;
        [SerializeField, Range(0.0f, 100.0f), Tooltip("대시 힘")] private float _dashPower;
        [SerializeField, Range(0, 999999), Tooltip("공중 대시 횟수")] private int _airDashMaxCount;

        //[Space(10), Header("사망 및 부활")]
        //[SerializeField, Range(0, 999999), Tooltip("슬라임이 죽을 수 있는 최대 횟수")] private int _maxDeathCount;
        //[SerializeField, Range(0.0f, 100.0f), Tooltip("피격 후 사망 상태로 전환되기까지의 시간")] private float _timeToDie;
        //[SerializeField, Range(0.0f, 100.0f), Tooltip("부활 상태에서 리스폰되는 플랫폼과의 높이")] private float _heightToRevive;
        [SerializeField, Range(0.0f, 100.0f), Tooltip("사망 상태에서 부활 상태로 넘어가기까지의 시간")] private float _timeToRevive;
        //[SerializeField, Range(0.0f, 100.0f), Tooltip("부활 상태에서 부활 종료로 넘어가기까지의 시간")] private float _invincibleTime;



        #region 프로퍼티 (Get)

        public int MaxHealthPoint => _maxHealthPoint;
        public float MoveSpeed => _moveSpeed;
        public float JumpPower => _jumpPower;
        public float DashPower => _dashPower;
        public int AirDashMaxCount => _airDashMaxCount;
        public float TimeToRevive => _timeToRevive;

        #endregion 프로퍼티 (Get)
    }
}
