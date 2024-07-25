using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Game Object")]
    public PoolManager Pool;
    public Player MainPlayer;

    [Header("Game Control")]
    public float MaxGameTime = 40.0f;
    public float GameTime;
    
    [Header("Player Infomation")]
    public int[] NextExp = { 10, 30, 50, 80, 120, 180, 250, 300, 380, 450, 600};
    public int Exp;
    public int Level;
    public int KillCount;

    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        GameTime += Time.deltaTime;

        if(MaxGameTime < GameTime) GameTime = MaxGameTime;
    }

    #region Custom
    public void GetExp()
    {
        ++Exp;

        if (NextExp[Level] <= Exp)
        {
            ++Level;
            Exp = 0;
        }
    }
    #endregion
}
