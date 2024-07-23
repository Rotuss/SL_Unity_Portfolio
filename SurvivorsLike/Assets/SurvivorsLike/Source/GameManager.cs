using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PoolManager Pool;
    public Player MainPlayer;
    public const float MaxGameTime = 40.0f;
    public float GameTime;

    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        GameTime += Time.deltaTime;

        if(MaxGameTime < GameTime) GameTime = MaxGameTime;
    }

}
