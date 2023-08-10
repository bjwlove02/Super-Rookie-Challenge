using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataManager : MonoBehaviour
{
    public enum DataType { Exp, Level, HP };
    public DataType dataType;

    Text dataText;
    Slider dataSlider;

    private void Awake()
    {
        dataText = GetComponent<Text>();
        dataSlider = GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        switch (dataType)
        {
            case DataType.Exp:
                float curExp = GameManager.instance.exp;
                float maxExp = GameManager.instance.maxExp;
                dataSlider.value = curExp / maxExp;
                break;

            case DataType.Level:
                dataText.text = string.Format("Lv.{0:F0}", GameManager.instance.level);
                break;

            case DataType.HP:
                float curHP = GameManager.instance.hp;
                float maxHP = GameManager.instance.maxHP;
                if (curHP > maxHP)
                    curHP = maxHP;
                dataSlider.value = curHP / maxHP;
                break;
        }
    }
}
