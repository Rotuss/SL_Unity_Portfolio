using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passive : MonoBehaviour
{
    public static float Speed
    {
        get { return 0 == GameManager.Instance.PlayerID ? 1.1f : 1.0f; }
    }

    public static float WeaponSpeed
    {
        get { return 0 == GameManager.Instance.PlayerID ? 1.1f : 1.0f; }
    }

    public static float WeaponRate
    {
        get { return 1 == GameManager.Instance.PlayerID ? 0.9f : 1.0f; }
    }

    public static float Damage
    {
        get { return 2 == GameManager.Instance.PlayerID ? 1.2f : 1.0f; }
    }

    public static int Count
    {
        get { return 3 == GameManager.Instance.PlayerID ? 1 : 0; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
