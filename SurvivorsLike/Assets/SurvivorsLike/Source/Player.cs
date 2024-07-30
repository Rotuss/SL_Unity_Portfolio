using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public RuntimeAnimatorController[] AnimControllers;
    public Hand[] Hands;
    public Scanner Scan;
    public Vector2 InputVec;
    public float DefaultSpeed;
    public float Speed;

    Rigidbody2D Rigid;
    SpriteRenderer Spriter;
    Animator Anim;

    void Awake()
    {
        Speed = DefaultSpeed;

        Hands = GetComponentsInChildren<Hand>(true);
        Scan = GetComponent<Scanner>();
        Rigid = GetComponent<Rigidbody2D>();
        Spriter = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Speed *= Passive.Speed;
        Anim.runtimeAnimatorController = AnimControllers[GameManager.Instance.PlayerID];
    }

    void FixedUpdate()
    {
        if (true == GameManager.Instance.IsStop) return;

        Vector2 NextVec = InputVec * Speed * Time.fixedDeltaTime;
        Rigid.MovePosition(Rigid.position + NextVec);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        if (true == GameManager.Instance.IsStop) return;

        Anim.SetFloat("Speed", InputVec.magnitude);

        if(0 != InputVec.x)
        {
            // InputVec.x�� 0���� ������(����) true(����On), ũ��(������) false
            Spriter.flipX = 0 > InputVec.x;
        }
    }

    void OnMove(InputValue value)
    {
        if (true == GameManager.Instance.IsStop) return;

        InputVec = value.Get<Vector2>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (true == GameManager.Instance.IsStop) return;

        GameManager.Instance.HP -= Time.deltaTime * 10;

        if(0 > GameManager.Instance.HP)
        {
            // Shadow, Area ������ ������ �ڽ� ������Ʈ
            for(int i = 2; i < transform.childCount; ++i)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }

            Anim.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        }
    }
}
