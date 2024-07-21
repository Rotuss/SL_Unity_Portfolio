using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PoolManager Pool;
    public Player MainPlayer;

    void Awake()
    {
        Instance = this;
    }

}
