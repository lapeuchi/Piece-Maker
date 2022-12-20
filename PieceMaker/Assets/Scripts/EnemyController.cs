using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

enum State
{
    Move,
    Attack,
    Hit,
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
                case State.Move:
                    anim.CrossFade("Move", 0.1f);
                    break;
                case State.Attack:
                    anim.CrossFade("Attack", 0.1f);
                    break;
                case State.Hit:
                    anim.CrossFade("Hit", 0.1f);
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
    
    //stat
    float hp;
    float damage;
    float moveSpeed;
    float attackRange;
    static int priority = 0;

    private void Start()
    {
        Init();
        nav.avoidancePriority = priority++;
    }

    private void Update()
    {
        switch(State)
        {
            case State.Move:
                UpdateMove();
                break;
            case State.Attack:
                UpdateAttack();
                break;
        }
    }

    void Init()
    {
        hp = 20f;
        damage = 5f;
        moveSpeed = 1.5f;
        attackRange = 3;
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;

        nav.speed = moveSpeed;
        State = State.Move;
    }

    void UpdateMove()
    {
        if((target.position - transform.position).magnitude < attackRange - 1f)
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
            State = State.Move;

        Quaternion qua = Quaternion.LookRotation(target.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, qua, 20 * Time.deltaTime);
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
        if ((target.position - transform.position).magnitude >= attackRange)
            State = State.Move;
        else
            State = State.Attack;
    }

    public void OnAttacked()
    {
        target.GetComponent<PlayerController>().Hit(damage);
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}
