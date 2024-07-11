namespace StatePattern
{
    // 상태 클래스를 구현하는 인터페이스
    public interface IState
    {
        void Enter(); // 상태에 들어갈 때에 호출하는 함수
        void Execute(); // 상태를 유지할 때에 호출하는 함수
        void Exit(); // 상태를 나갈 때에 호출하는 함수
    }
}
