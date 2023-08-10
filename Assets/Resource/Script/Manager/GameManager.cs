using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Control")]
    public PlayerCtrl playerCtrl;
    public MonsterCtrl monsterCtrl;

    [Header("Manager")]
    public PoolManager poolManager;
    public UIManager uiManager;
    public SoundManager soundManager;
    [HideInInspector] public SkillManager skillManager;

    [Header("Data")]
    public float level;
    public float exp;
    public float maxExp = 5;
    public float hp;
    public float maxHP;

    bool paused;

    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
        maxHP = playerCtrl.maxHP;
        PauseGame();
    }

    private void Start()
    {
        hp = maxHP;
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    private void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            paused = true;
            PauseGame();
        }
        else
        {
            if(paused)
            {
                paused = false;
                ResumeGame();
            }
        }
    }

    public void OnApplicationQuit()
    {
#if !UNITY_ANDROID
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void GetExp()
    {
        exp++;
        if (exp >= maxExp) 
        {
            level++;
            maxExp *= 1.2f;
            exp = 0;
        }
    }
}
