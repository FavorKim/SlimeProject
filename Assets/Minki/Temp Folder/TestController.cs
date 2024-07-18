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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animator.SetBool("Idle", true);
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            _animator.SetBool("Idle", false);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _animator.SetTrigger("Die");
        }
    }

}
