using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSkill : SkillManager
{
    public override void UseSkill()
    {
        if (canUseSkill && GameManager.instance.playerCtrl.canSkill1) 
        {
            //GameManager.instance.playerCtrl.canAttack = true;
            StartCoroutine(GameManager.instance.playerCtrl.Skill1());
            GameManager.instance.soundManager.SFXPlay("Skill1", AudioSource);
            
            Instantiate(Particle, ParticleTr);
            skillFilter.fillAmount = 1;
            StartCoroutine(Cooltime());

            curCoolTime = coolTime;
            coolTimeCounter.text = "" + curCoolTime;

            StartCoroutine(CoolTimeCounter());

            canUseSkill = false;
        }
    }
}
