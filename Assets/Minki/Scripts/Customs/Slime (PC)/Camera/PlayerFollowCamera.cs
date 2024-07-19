using UnityEngine;

namespace Player
{
    public class PlayerFollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform _player;
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void LateUpdate()
        {
            _mainCamera.transform.position = new Vector3(_player.position.x, _player.position.y, _mainCamera.transform.position.z);
        }
    }

}

