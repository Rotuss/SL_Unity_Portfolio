using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
        Vector2 NextVec = InputVec * Speed * Time.fixedDeltaTime;
        Rigid.MovePosition(Rigid.position + NextVec);
    }

    // Update is called once per frame
    void Update()
    {
        // 자연스럽게 보간 적용된 상태로 움직이고 싶다면 GetAxis
        //InputVec.x = Input.GetAxisRaw("Horizontal");
        //InputVec.y = Input.GetAxisRaw("Vertical");
    }

    void OnMove(InputValue value)
    {
        // 여기서 노말라이즈도 적용이 같이 되고 있음
        InputVec = value.Get<Vector2>();
    }
}
