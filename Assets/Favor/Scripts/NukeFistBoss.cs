using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class NukeFistBoss : MonoBehaviour
{
    [SerializeField] private AttackObjectBase obstacle;
    private NukeFistBossBTRunner btRunner;
    private Animator animator;

    [SerializeField] LayerMask atkLayer;

    [SerializeField] private GameObject followDest;

    [SerializeField] private bool isHit;
    public bool IsHit
    {
        get { return isHit; }
        private set
        {
            if(isHit!=value)
            {
                isHit = value;
            }
        }
    }
    public bool IsDead { get; private set; }

    [SerializeField] private int hp = 80;
    public int HP
    {
        get { return hp; }
        set
        {
            if (hp != value)
            {
                hp = value;
            }
        }
    }
    [SerializeField] private int atk = 40;
    public int Atk
    {
        get { return atk; }
        set
        {
            if (atk != value)
            {
                atk = value;
            }
        }
    }

    [SerializeField] float moveSpeed = 2;
    public float MoveSpeed
    {
        get { return moveSpeed; }
        set
        {
            if (moveSpeed != value)
            {
                moveSpeed = value;
            }
        }
    }

    [SerializeField] float atkSpeed = 2;
    [SerializeField] float delayedAtkTime = 0;

    [SerializeField] float atkRange = 2.0f;

    void Start()
    {
        btRunner = new NukeFistBossBTRunner(this);
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        OnDead += OnDead_;
        OnHit += OnHit_GetDamageByObstacle;
        OnAttack += OnAttack_;
        OnMove += OnMove_;
    }
    private void OnDisable()
    {
        OnMove -= OnMove_;
        OnAttack -= OnAttack_;
        OnHit -= OnHit_GetDamageByObstacle;
        OnDead -= OnDead_;
    }

    void Update()
    {
        btRunner.RunBT();
    }




    private event Action OnHit;
    private event Action OnDead;
    private event Action OnAttack;
    private event Action OnMove;

    public void InvokeOnHit() { OnHit?.Invoke(); }
    public void InvokeOnDead() { OnDead?.Invoke(); }
    public void InvokeOnAttack() { OnAttack?.Invoke(); }
    public void InvokeOnMove() { OnMove?.Invoke(); }

    public bool CheckIsAttacking()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        if (clipInfo.Length > 0)
        {
            AnimationClip clip = clipInfo[0].clip;
            if (clip != null && clip.name == "Attack")
            {
                delayedAtkTime = 0;
                return true;
            }
        }
        return false;
    }

    public bool CheckAttackWait()
    {

        if (delayedAtkTime >= atkSpeed)
        {
            delayedAtkTime = 0;
            return false;
        }
        else
        {
            delayedAtkTime += Time.deltaTime;
            return true;
        }
    }
    public bool CheckIsInRange()
    {
        Collider[] hit = Physics.OverlapBox(transform.position + new Vector3(0, 0.5f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f), Quaternion.identity, atkLayer);

        if (hit.Length > 0)
        {
            Debug.Log("범위 내의 PC 발견");
            return true;
        }
        else
        {
            delayedAtkTime = 0;
            Debug.Log("범위 내의 PC 못 찾음");
            return false;
        }
    }

    private void OnHit_GetDamageByObstacle()
    {
        HP -= obstacle.atk;
        IsHit = false;
        obstacle = null;
        Debug.Log("피격");
    }

    private void OnAttack_()
    {
        Debug.Log("타격");
        delayedAtkTime = 0;
        animator.SetTrigger("Attack");
    }

    private void OnDead_()
    {
        IsDead = true;
        Debug.Log("사망");
    }

    private void OnMove_()
    {
        if(followDest.transform.position.x > transform.position.x)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            transform.Translate(-transform.right * moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
            transform.Translate(transform.right * moveSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent(out AttackObjectBase obs))
        {
            obstacle = obs;
            IsHit = true;
        }
    }

}
