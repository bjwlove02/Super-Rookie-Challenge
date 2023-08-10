using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterCtrl : MonoBehaviour
{
    [Header("Monster Stats")]
    public float maxHP = 100f;
    public float curHP;
    public float ATK = 10f;
    public float attackSpeed = 1f;
    public float attackRange = 1.5f;

    [Header("Audio Sound")]
    public AudioSource AttackSound;
    public AudioSource DeadSound;

    [HideInInspector] public bool isChase;
    [HideInInspector] public bool isAttack;
    [HideInInspector] public bool canAttack = true;
    [HideInInspector] public bool isDie = false;

    Rigidbody rgd;
    BoxCollider col;
    NavMeshAgent nav;
    Animator ani;

    GameObject player;
    float betweendist;

    private void Awake()
    {
        rgd = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        ChaseStart();
        curHP = maxHP;
     }

    private void Update()
    {
        ChaseStart();
        AttackStart();
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        curHP = maxHP;
        isDie = false;
        col.enabled = true;
        rgd.isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Weapon") && !isDie)
        {
            if (GameManager.instance.playerCtrl.isAttack && GameManager.instance.playerCtrl.monster == gameObject) 
            {
                if (GameManager.instance.playerCtrl.isHit)
                    return;
                else
                    curHP -= GameManager.instance.playerCtrl.ATK;

                GameManager.instance.playerCtrl.isHit = true;
            }
        }

        if(other.gameObject.CompareTag("Skill") && !isDie)
        {
            if (GameManager.instance.playerCtrl.isSkill)
            {
                curHP -= GameManager.instance.playerCtrl.ATK;
            }
        }

        if (curHP<= 0)
        {
            col.enabled = false;
            isDie = true;
            rgd.isKinematic = true;
            GameManager.instance.playerCtrl.isKill = true;
            ani.SetBool("isDead", true);
            GameManager.instance.soundManager.SFXPlay("Dead", DeadSound);
            GameManager.instance.GetExp();
        }
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }

    void FreezeVelocity()
    {
        rgd.velocity = Vector3.zero;
        rgd.angularVelocity = Vector3.zero;
    }

    void ChaseStart()
    {
        transform.LookAt(player.transform.position);
        nav.SetDestination(player.transform.position);
        isChase = true;
        ani.SetBool("isWalk", true);
    }

    void AttackStart()
    {
        betweendist = Vector3.Distance(transform.position, player.transform.position);

        if (betweendist <= attackRange)
        {
            if (canAttack)
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        ani.SetBool("isAttack", true);
        GameManager.instance.soundManager.SFXPlay("MonsterAttack", AttackSound);
        canAttack = false;

        yield return new WaitForSeconds(attackSpeed);

        isChase = true;
        isAttack = false;
        ani.SetBool("isAttack", false);
        canAttack = true;
    }
}
