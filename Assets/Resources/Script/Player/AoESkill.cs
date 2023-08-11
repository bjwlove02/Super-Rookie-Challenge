using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoESkill : SkillManager
{
    public override void UseSkill()
    {
        if (canUseSkill && GameManager.instance.playerCtrl.canSkill2)
        {
            StartCoroutine(GameManager.instance.playerCtrl.Skill2());
            GameManager.instance.soundManager.SFXPlay("Skill2", AudioSource);
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
