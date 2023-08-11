using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSkill : SkillManager
{
    public override void  UseSkill()
    {
        if (canUseSkill)
        {
            Heal();
            Instantiate(Particle, ParticleTr);
            GameManager.instance.soundManager.SFXPlay("Skill3", AudioSource);
            skillFilter.fillAmount = 1;
            StartCoroutine(Cooltime());

            curCoolTime = coolTime;
            coolTimeCounter.text = "" + curCoolTime;

            StartCoroutine(CoolTimeCounter());

            canUseSkill = false;
        }
    }

    public void Heal()
    {
        GameManager.instance.hp += GameManager.instance.playerCtrl.ATK;
    }
}
