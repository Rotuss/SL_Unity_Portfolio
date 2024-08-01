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
        // Collision�� Tag�� Enemy�� �ƴϰų� ���� ���Ⱑ �ٰŸ� ����� ����
        if (false == collision.CompareTag("Enemy") || -100 == Per) return;

        --Per;
        if(0 > Per)
        {
            Rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }

    // ����ü�� Area�� ����� ��Ȱ��ȭ �۾�
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Collision�� Tag�� Area�� �ƴϰų� ���� ���Ⱑ �ٰŸ� ����� ����
        if (false == collision.CompareTag("Area") || -100 == Per) return;

        gameObject.SetActive(false);
    }

    #region Custom
    public void Init(float InDamage, int InPer, Vector3 InDir, int InType)
    {
        Damage = InDamage;
        Per = InPer;

        // �����, ���Ÿ� ������ ���
        if(0 <= InPer)
        {
            if(1 == InType) Rigid.velocity = InDir * 15.0f;
            else if(2 == InType) Rigid.AddForce(InDir * 8.0f, ForceMode2D.Impulse);
        }
    }
    #endregion
}
