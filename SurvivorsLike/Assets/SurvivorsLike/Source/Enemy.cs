using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D Target;
    public float Speed;

    Rigidbody2D Rigid;
    SpriteRenderer Spriter;

    bool IsLive = true;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        Spriter = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        if (false == IsLive) return;

        // ���� ���ϱ�
        Vector2 DirVec = Target.position - Rigid.position;
        Vector2 NextVec = DirVec.normalized * Speed * Time.fixedDeltaTime;
        Rigid.MovePosition(Rigid.position + NextVec);
        Rigid.velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (false == IsLive) return;

        // Enemy�� x��ġ�� Target�� x��ġ���� ū ���(Enemy�� ������ Target�� ����) true(������ �ٶ󺸰� ����O)
        // Enemy�� x��ġ�� Target�� x��ġ���� ���� ���(Enemy�� ���� Target�� ������) false(�������� �ٶ󺸰� ����X)
        Spriter.flipX = Target.position.x < Rigid.position.x ? true : false;
    }
}
