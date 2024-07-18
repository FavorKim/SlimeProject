using UnityEngine;

public class TestController : MonoBehaviour
{
    private Animator _animator;

    [SerializeField] private Transform _groundChecker;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _animator.SetTrigger("Jump");
        }

        Debug.Log(IsGround());
    }

    private bool IsGround()
    {
        bool groundHit = Physics.Raycast(origin: _groundChecker.position, direction: Vector3.down, hitInfo: out RaycastHit hitInfo, maxDistance: 0.2f, layerMask: 1 << LayerMask.NameToLayer("Ground"));
        RaycastHit raycastHit = hitInfo;

        return groundHit;
    }
}
