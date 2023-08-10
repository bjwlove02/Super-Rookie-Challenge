using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class SkillManager : MonoBehaviour
{
    public enum SkillType { Single, AoE, Heal };
    public SkillType skillType;

    public Image skillFilter;
    public Text coolTimeCounter;
    public GameObject Particle;
    public Transform ParticleTr;

    public AudioSource AudioSource;
    protected Animator ani;

    public float coolTime;
    protected float curCoolTime;
    protected float skillRange;
    protected bool canUseSkill = true;

    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    private void Start()
    {
        skillFilter.fillAmount = 0;
        Skills();
    }

    public void Skills()
    {
        switch (skillType)
        {
            case SkillType.Single:
                coolTime = 3f;
                break;

            case SkillType.AoE:
                coolTime = 5f;
                break;

            case SkillType.Heal:
                coolTime = 10f;
                break;
        }
    }

    abstract public void UseSkill();

    protected IEnumerator Cooltime()
    {
        while (skillFilter.fillAmount > 0)
        {
            skillFilter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;

            yield return null;
        }

        canUseSkill = true;

        yield break;
    }

    protected IEnumerator CoolTimeCounter()
    {
        while (curCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            curCoolTime -= 1.0f;
            coolTimeCounter.text = "" + curCoolTime;
        }

        coolTimeCounter.text = "";
        yield break;
    }
}