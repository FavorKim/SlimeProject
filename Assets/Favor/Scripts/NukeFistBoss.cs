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
    [SerializeField] private BoxCollider handCollider;

    [SerializeField] LayerMask atkLayer;

    [SerializeField] private GameObject followDest;

    [SerializeField] private bool isHit;
    public bool IsHit
    {
        get { return isHit; }
        private set
        {
            if (isHit != value)
            {
                isHit = value;
            }
        }
    }
    public bool IsDead { get; private set; }
    [SerializeField] bool isInvincible = false;

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

    int isInRangeHash = Animator.StringToHash("isInRange");


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

    [SerializeField] float atkSpeed = 2.8f;
    //[SerializeField] float delayedAtkTime = 0;

    [SerializeField] float atkRange = 2.0f;

    [SerializeField] float invincibleTime = 2.0f;

    void Start()
    {
        btRunner = new NukeFistBossBTRunner(this);
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        OnDead += OnDead_;

        OnHit += OnHit_GetDamageByObstacle;
        OnHit += OnHit_StartInvincible;
        OnHit += OnHit_SetAnimator;

        OnAttack += OnAttack_;


        OnMove += OnMove_;
    }
    private void OnDisable()
    {
        OnMove -= OnMove_;


        OnAttack -= OnAttack_;

        OnHit -= OnHit_SetAnimator;
        OnHit -= OnHit_StartInvincible;
        OnHit -= OnHit_GetDamageByObstacle;

        OnDead -= OnDead_;
    }

    void Update()
    {
        if (!IsDead)
            btRunner.RunBT();
    }




    private event Action OnHit;
    private event Action OnDead;
    private event Action OnAttack;
    private event Action OnMove;
    private event Action OnCharge;

    public void InvokeOnHit() { OnHit?.Invoke(); }
    public void InvokeOnDead() { OnDead?.Invoke(); }
    public void InvokeOnAttack() { OnAttack?.Invoke(); }
    public void InvokeOnMove() { OnMove?.Invoke(); }
    public void InvokeOnCharge() { OnCharge?.Invoke(); }

    public bool CheckCanMove()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);

        if (clipInfo.Length > 0)
        {
            AnimationClip clip = clipInfo[0].clip;
            if (clip != null)
            {
                if (clip.name == "Attack" || clip.name == "AttackWait" || clip.name == "Hit")
                    return false;
            }
        }
        return true;
    }

    public bool CheckIsInRange()
    {
        RaycastHit temphit;
        Ray ray = new Ray();
        if(Physics.Raycast(ray, out temphit))
        {
            temphit.transform.position = transform.position;
        }


        Collider[] hit = Physics.OverlapBox(transform.position + transform.forward*0.1f, new Vector3(2f, 2.0f, 2.0f), Quaternion.identity, atkLayer);
        if (hit.Length > 0)
        {
            if (animator.GetBool(isInRangeHash) == false)
                animator.SetBool(isInRangeHash, true);
            Debug.Log("범위 내의 PC 발견");
            animator.SetBool("Charge", true);
            return true;
        }
        else
        {
            //delayedAtkTime = 0;
            animator.SetBool(isInRangeHash, false);
            Debug.Log("범위 내의 PC 못 찾음");
            animator.SetBool("Charge", false);
            return false;
        }

        
    }

    IEnumerator CorInvincible()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    private void OnHit_GetDamageByObstacle()
    {
        HP -= obstacle.atk;
        IsHit = false;
        obstacle = null;
        Debug.Log("피격");
    }
    private void OnHit_StartInvincible()
    {
        StartCoroutine(CorInvincible());
    }
    private void OnHit_SetAnimator()
    {
        animator.SetTrigger("Hit");
    }

    private void OnAttack_()
    {
        Debug.Log("타격");
        //delayedAtkTime = 0;
    }

    private void OnDead_()
    {
        IsDead = true;
        animator.SetBool("isDead", true);
        Debug.Log("사망");
    }

    private void OnMove_()
    {
        if (followDest.transform.position.x > transform.position.x)
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

    private void OnAttack_SetCollider(bool onOff)
    {
        handCollider.enabled = onOff;
    }

    public void AnimEvent_ColliderOn()
    {
        OnAttack_SetCollider(true);
    }
    public void AnimEvent_ColliderOff()
    {
        OnAttack_SetCollider(false);
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.TryGetComponent(out AttackObjectBase obs))
        {
            if (!isInvincible)
            {
                obstacle = obs;
                IsHit = true;
            }
        }
    }

}
