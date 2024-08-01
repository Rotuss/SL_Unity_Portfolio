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
        // Collision된 Tag가 Enemy가 아니거나 현재 무기가 근거리 무기면 무시
        if (false == collision.CompareTag("Enemy") || -100 == Per) return;

        --Per;
        if(0 > Per)
        {
            Rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    // 투사체가 Area를 벗어나면 비활성화 작업
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Collision된 Tag가 Area가 아니거나 현재 무기가 근거리 무기면 무시
        if (false == collision.CompareTag("Area") || -100 == Per) return;

        gameObject.SetActive(false);
    }

    #region Custom
    public void Init(float InDamage, int InPer, Vector3 InDir, int InType)
    {
        Damage = InDamage;
        Per = InPer;

        // 관통시, 원거리 무기의 경우
        if(0 <= InPer)
        {
            if(1 == InType) Rigid.velocity = InDir * 15.0f;
            else if(2 == InType) Rigid.AddForce(InDir * 8.0f, ForceMode2D.Impulse);
        }
    }
    #endregion
}
