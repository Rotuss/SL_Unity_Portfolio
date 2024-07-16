using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 InputVec;
    public float Speed;

    Rigidbody2D Rigid;

    void Awake()
    {
        //Speed = 3.0f;

        Rigid = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        Vector2 NextVec = InputVec.normalized * Speed * Time.fixedDeltaTime;
        Rigid.MovePosition(Rigid.position + NextVec);
    }

    // Update is called once per frame
    void Update()
    {
        // �ڿ������� ���� ����� ���·� �����̰� �ʹٸ� GetAxis
        InputVec.x = Input.GetAxisRaw("Horizontal");
        InputVec.y = Input.GetAxisRaw("Vertical");
    }
}
