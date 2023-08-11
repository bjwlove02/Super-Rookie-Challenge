using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCtrl : MonoBehaviour
{
    [Header ("Player Stats")] 
    public float maxHP = 100f;
    public float curHP;
    public float ATK = 10f;
    public float attackSpeed = 1f;
    public float attackRange = 1f;
    public float skill2Range = 2f;

    [Header("Audio Sound")]
    public AudioSource AttackSound;

    [HideInInspector] public bool isChase;
    [HideInInspector] public bool isAttack;
    [HideInInspector] public bool isKill = false;
    [HideInInspector] public bool isHit = true;
    [HideInInspector] public bool isSkill = false;
    [HideInInspector] public bool canAttack = true;
    [HideInInspector] public bool canSkill1 = false;
    [HideInInspector] public bool canSkill2 = false;

    public GameObject monster;
    [Header("")]
    public List<SkillManager> skills = new List<SkillManager>();

    Rigidbody rgd;
    NavMeshAgent nav;
    [HideInInspector] public Animator ani;

    int curSkillIndex = 0;
    float betweendist;    

    private void Awake()
    {
        rgd = GetComponent<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
        ani = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        curHP = maxHP;
        monster = GameManager.instance.poolManager.pool[0];
    }

    private void Update()
    {
        StartCoroutine(Chase());
        curHP = GameManager.instance.hp;
        if (curHP > maxHP)
            curHP = maxHP;

        if (betweendist <= attackRange)
            canSkill1 = true;
        else
            canSkill1 = false;

        if (betweendist <= skill2Range)
            canSkill2 = true;
        else
            canSkill2 = false;
    }

    private void FixedUpdate()
    {
        FreezeVelocity();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Monster"))
            return;

            GameManager.instance.hp -= GameManager.instance.monsterCtrl.ATK;

        if (GameManager.instance.hp < 0)
            GameManager.instance.hp = 0;
    }

    IEnumerator Chase()
    {
        if (isKill)
        {
            GetCloseMonster();
            yield return new WaitForSeconds(0.2f);
            isKill = false;
        }

        yield return null;
        monster.transform.LookAt(monster.transform.position);
        nav.SetDestination(monster.transform.position);
        isChase = true;
        ani.SetBool("isMove", true);

        betweendist = Vector3.Distance(transform.position, monster.transform.position);

        if (betweendist <= attackRange)
        {
            if (canAttack && !isSkill) 
            {
                StartCoroutine(Attack());
            }
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        LookAtMonster();
        ani.SetBool("isAttack1", true);
        canAttack = false;

        yield return new WaitForSeconds(0.3f);

        GameManager.instance.soundManager.SFXPlay("PlayerAttack", AttackSound);
        isHit = false;

        yield return new WaitForSeconds(attackSpeed);
        if (isSkill) 
            yield return new WaitForSeconds(0.2f);

        isChase = true;
        isAttack = false;
        ani.SetBool("isAttack1", false);
        canAttack = true;
    }

    public IEnumerator Skill1()
    {
        isChase = false;
        isSkill = true;
        ani.SetBool("isAttack1", false);
        yield return new WaitForSeconds(0.1f);
        LookAtMonster();
        ani.SetBool("isAttack1", true);

        yield return new WaitForSeconds(attackSpeed);

        isChase = true;
        isSkill = false;
        ani.SetBool("isAttack1", false);
        isHit = false;
    }

    public IEnumerator Skill2()
    {
        isChase = false;
        isSkill = true;
        ani.SetBool("isAttack1", false);
        ani.SetBool("isAttack2", true);

        yield return new WaitForSeconds(attackSpeed);

        isChase = true;
        isSkill = false;
        ani.SetBool("isAttack2", false);
    }

    private GameObject GetCloseMonster()
    {
        float shortdist = Mathf.Infinity;

        foreach (GameObject found in GameManager.instance.poolManager.pool)
        {
            MonsterCtrl monsterCtrl = found.GetComponent<MonsterCtrl>();
            float dist = Vector3.Distance(transform.position, found.transform.position);

            if (dist < shortdist && !monsterCtrl.isDie)
            {
                shortdist = dist;
                monster = found; 
            }
        }
        return monster;
    }

    void FreezeVelocity()
    {
        rgd.velocity = Vector3.zero;
        rgd.angularVelocity = Vector3.zero;
    }

    public void StartAutoSkill()
    {
        StartCoroutine("AutoSkill");
    }

    public void StopAutoSkill()
    {
        StopCoroutine("AutoSkill");
    }

    IEnumerator AutoSkill()
    {
        if (curSkillIndex < skills.Count) 
        {
            SkillManager curSkill = skills[curSkillIndex];
            curSkill.UseSkill();

            curSkillIndex++;
            if (curSkillIndex == skills.Count)
                curSkillIndex = 0;
        }
        yield return new WaitForSeconds(1.0f);
        StartCoroutine("AutoSkill");
    }

    public void LookAtMonster()
    {
        nav.enabled = false;
        transform.LookAt(monster.transform);
        nav.enabled = true;
    }
}