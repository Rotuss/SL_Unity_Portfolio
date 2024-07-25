using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage;
    public int Per;

    Rigidbody2D Rigid;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (false == collision.CompareTag("Enemy") || -1 == Per) return;

        --Per;
        if(-1 >= Per || true == collision.CompareTag("Area"))
        {
            Rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    #region Custom
    public void Init(float InDamage, int InPer, Vector3 InDir)
    {
        Damage = InDamage;
        Per = InPer;

        // °üÅë½Ã
        if(-1 < InPer)
        {
            Rigid.velocity = InDir * 15.0f;
        }
    }
    #endregion
}
