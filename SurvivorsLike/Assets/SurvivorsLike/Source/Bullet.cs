using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public int Per;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Custom
    public void Init(float InDamage, int InPer)
    {
        Damage = InDamage;
        Per = InPer;
    }
    #endregion
}
