namespace StatePattern
{
    // 상태 클래스를 구현하는 인터페이스
    public interface IState
    {
        void Enter(); // 상태에 들어갈 때에 호출하는 함수
        void Execute(); // 상태를 유지할 때에 호출하는 함수
        void Exit(); // 상태를 나갈 때에 호출하는 함수
    }

    // 상태 클래스들의 최상위 추상 클래스
    public abstract class BaseState : IState
    {
        // 상태 패턴을 사용하는 클래스
        private EntityExample _entity;

        // 생성자
        public BaseState(EntityExample entity)
        {
            _entity = entity;
        }

        // 인터페이스 함수
        public abstract void Enter();
        public abstract void Execute();
        public abstract void Exit();
    }
}
