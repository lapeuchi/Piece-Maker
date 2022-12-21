using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   

enum State
{
    None,
    Landing,
    Moving,
    Attack,
    Die
}

public class EnemyController : MonoBehaviour
{
    State State
    {
        get
        {
            return _state;
        }
        set
        {
            _state = value;

            switch(State)
            {
                case State.Moving:
                    anim.CrossFade("Moving", 0.1f);
                    break;
                case State.Attack:
                    anim.CrossFade("Attack", 0.1f);
                    break;
                case State.Die:
                    anim.CrossFade("Die", 0.1f);
                    break;
            }
        }
    }

    [SerializeField] State _state;

    Animator anim;
    NavMeshAgent nav;
    Transform target;
    
    [Header("Stat")]
    [SerializeField] float hp;
    [SerializeField] float damage;
    [SerializeField] float moveSpeed;
    [SerializeField] float attackRange;

    public GameObject bulletPrefab;
    public GameObject healPrefab;
    [SerializeField] Transform FirePos;

    private void Start()
    {
        hp = 100f;
        moveSpeed = 4f;
        
        int r = Random.Range(0, 3);
        if(r == 0)
        {
            attackRange = 7;
        }
        else if (r == 1)
        {
            attackRange = 15;
        }
        else
        {
            attackRange = 20;
        }
       

        anim = GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;

        nav.speed = moveSpeed;
        State = State.Moving;
    }

    private void Update()
    {
        switch(State)
        {
            case State.Moving:
                UpdateMoving();
                break;
            case State.Attack:
                UpdateAttack();
                break;
        }
    }

    void UpdateMoving()
    {
        // 목표가 사거리 내에 있을 때 -> 공격
        if((target.position - transform.position).magnitude < attackRange-1f)
        {
            State = State.Attack;
            nav.SetDestination(transform.position);
            return;
        }
        nav.SetDestination(target.position);
        
    }

    void UpdateAttack()
    {
        if ((target.position - transform.position).magnitude >= attackRange)
            State = State.Moving;

        Quaternion lookPosition = Quaternion.LookRotation(target.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookPosition, 20 * Time.deltaTime);
    }

    public void SummonBullet()
    {
        Instantiate(bulletPrefab, FirePos.position, FirePos.rotation);
    }
    public void SummonHeal()
    {
        Instantiate(healPrefab, transform.position + Vector3.up, transform.rotation);
    }

    public void OnDamaged(float damage)
    {
        nav.SetDestination(transform.position);
        hp -= damage;

        if (hp <= 0)
            State = State.Die;
    }

    public void OnHit()
    {   
        if(target == null) return;
        if ((target.position - transform.position).magnitude >= attackRange)
            State = State.Moving;
        else
            State = State.Attack;
    }

    public void Die()
    {
        GameManager.instance.cnt--;
        Destroy(gameObject);
    }
}
