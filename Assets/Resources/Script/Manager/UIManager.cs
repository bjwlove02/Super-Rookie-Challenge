using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Skill UI")]
    public GameObject AutoFilter;
    public GameObject AutoBtnImage;
    public GameObject SFXBtn;
    
    [Header("Setting UI")]
    public GameObject BGMBtn;
    public GameObject ExitBtn;
    public GameObject BGMOnImg;
    public GameObject BGMOffImg;
    public GameObject SFXOnImg;
    public GameObject SFXOffImg;

    [Header("Sound")]
    public AudioSource TouchSound;

    float rotSpeed = 200f;
    bool isOpen = false;
    bool isBGMMute = false;
    bool isSFXMute = false;

    public void AutoSkillBtn()
    {
        if (AutoFilter.activeSelf)
        {
            AutoFilter.SetActive(false);
            StartCoroutine("RotateAutoBtn");
            GameManager.instance.playerCtrl.StartAutoSkill();
        }
        else
        {
            AutoFilter.SetActive(true);
            StopCoroutine("RotateAutoBtn");
            GameManager.instance.playerCtrl.StopAutoSkill();
        }
    }

    IEnumerator RotateAutoBtn()
    {
        AutoBtnImage.transform.Rotate(0, 0, -Time.deltaTime * rotSpeed, Space.Self);

        yield return null;

        StartCoroutine("RotateAutoBtn");
    }

    public void PlayTouchSound()
    {
        GameManager.instance.soundManager.SFXPlay("Touch", TouchSound);
    }

    public void SettingListCtrl()
    {
        if (!isOpen)
        {
            SFXBtn.SetActive(true);
            BGMBtn.SetActive(true);
            ExitBtn.SetActive(true);
            isOpen = true;
        }
        else
        {
            SFXBtn.SetActive(false);
            BGMBtn.SetActive(false);
            ExitBtn.SetActive(false);
            isOpen = false;
        }
    }

    public void SFXCtrl()
    {
        if (isSFXMute)
        {
            SFXOnImg.SetActive(true);
            SFXOffImg.SetActive(false);
            GameManager.instance.soundManager.SFXVolume(0f);
            isSFXMute = false;
        }
        else
        {
            SFXOnImg.SetActive(false);
            SFXOffImg.SetActive(true);
            GameManager.instance.soundManager.SFXVolume(-80f);
            isSFXMute = true;
        }
    }

    public void BGMCtrl()
    {
        if (isBGMMute)
        {
            BGMOnImg.SetActive(true);
            BGMOffImg.SetActive(false);
            GameManager.instance.soundManager.BGMVolume(0f);
            isBGMMute = false;
        }
        else
        {
            BGMOnImg.SetActive(false);
            BGMOffImg.SetActive(true);
            GameManager.instance.soundManager.BGMVolume(-80f);
            isBGMMute = true;
        }
    }
}
