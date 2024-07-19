using UnityEngine;
using UnityEngine.InputSystem;

namespace StatePattern
{
    // 인풋 시스템의 입력 값 목록
    public enum InputName
    {
        MOVE, JUMP, DASH, LIFT, PUT,
    }

    // 상태 클래스를 구현하는 인터페이스
    public interface IState
    {
        void Enter(); // 상태에 들어갈 때에 호출하는 함수
        void Execute(); // 상태를 유지할 때에 호출하는 함수
        void Exit(); // 상태를 나갈 때에 호출하는 함수

        void FixedExecute(); // Execute()와 비슷하나, FixedUpdate()에서 호출하기 위한 함수
        void OnInputCallback(InputAction.CallbackContext callbackContext); // 인풋 시스템의 입력을 받아오기 위한 함수

        void OnTriggerEnter(Collider other);
        void OnCollisionEnter(Collision collision);
    }

    // 문자열을 제목 형식(APPLE → Apple; 첫 글자는 대문자, 이후는 소문자)으로 변환해 주는 확장 메서드
    public static class StringExtensions
    {
        public static string ToTitleCase(this string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                input = char.ToUpperInvariant(input[0]) + input.Substring(1).ToLowerInvariant();
            }

            return input;
        }
    }
}
