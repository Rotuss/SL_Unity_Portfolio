using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Scanner Scan;
    public Vector2 InputVec;
    public float Speed;

    Rigidbody2D Rigid;
    SpriteRenderer Spriter;
    Animator Anim;

    void Awake()
    {
        //Speed = 3.0f;

        Scan = GetComponent<Scanner>();
        Rigid = GetComponent<Rigidbody2D>();
        Spriter = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
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

    }

    void LateUpdate()
    {
        Anim.SetFloat("Speed", InputVec.magnitude);

        if(0 != InputVec.x)
        {
            // InputVec.x가 0보다 작으면(왼쪽) true(반전On), 크면(오른쪽) false
            Spriter.flipX = 0 > InputVec.x;
        }
    }

    void OnMove(InputValue value)
    {
        InputVec = value.Get<Vector2>();
    }
}
