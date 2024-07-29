using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Game Object")]
    public PoolManager Pool;
    public Player MainPlayer;
    public LevelUp UILevelUp;
    public GameObject UIResult;

    [Header("Game Control")]
    public float MaxGameTime = 40.0f;
    public float GameTime;
    public bool IsStop;
    
    [Header("Player Infomation")]
    public int[] NextExp = { 10, 30, 50, 80, 120, 180, 250, 300, 380, 450, 600};
    public int Exp;
    public int Level;
    public int KillCount;
    public float MaxHP = 100;
    public float HP;

    void Awake()
    {
        Instance = this;
        IsStop = true;
    }
    
    private void Start()
    {
        
    }

    private void Update()
    {
        if (true == IsStop) return;

        GameTime += Time.deltaTime;

        if(MaxGameTime < GameTime) GameTime = MaxGameTime;
    }

    #region Custom
    public void GameStart()
    {
        IsStop = false;
        HP = MaxHP;

        // 임시 테스트용
        UILevelUp.Select(0);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        IsStop = true;
        
        yield return new WaitForSeconds(0.5f);

        UIResult.SetActive(true);
        Stop();
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void GetExp()
    {
        ++Exp;

        if (NextExp[Mathf.Min(Level, NextExp.Length - 1)] <= Exp)
        {
            ++Level;
            Exp = 0;
            
            UILevelUp.Show();
        }
    }

    public void Stop()
    {
        IsStop = true;
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        IsStop = false;
        Time.timeScale = 1.0f;
    }
    #endregion
}
