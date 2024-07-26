using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public SpriteRenderer Spriter;
    public bool IsLeft;

    SpriteRenderer Player;
    Vector3 RightPos = new Vector3(0.35f, -0.15f, 0.0f);
    Vector3 RightPosRev = new Vector3(-0.15f, -0.15f, 0.0f);
    Quaternion LeftRot = Quaternion.Euler(0.0f, 0.0f, -30.0f);
    Quaternion LeftRotRev = Quaternion.Euler(0.0f, 0.0f, -150.0f);

    private void Awake()
    {
        Player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        // �÷��̾� �¿� ���� Ȯ��
        bool IsRev = Player.flipX;

        // �ٰŸ�
        if(true == IsLeft)
        {
            transform.localRotation = true == IsRev ? LeftRotRev : LeftRot;
            Spriter.flipY = IsRev;
            Spriter.sortingOrder = true == IsRev ? 4 : 6;
        }
        else
        {
            transform.localPosition = true == IsRev ? RightPosRev : RightPos;
            Spriter.flipX = IsRev;
            // ���� �������� ȸ��
            if(null != Player.GetComponent<Player>().Scan.NearestTarget)
            {
                Vector3 TargetPos = Player.GetComponent<Player>().Scan.NearestTarget.position;
                Vector3 Dir = TargetPos - transform.position;
                transform.localRotation = Quaternion.FromToRotation(Vector3.right, Dir);

                // ������ ���� ��������Ʈ ������
                bool IsRotA = 90 < transform.localRotation.eulerAngles.z && 270 > transform.localRotation.eulerAngles.z;
                bool IsRotB = -90 > transform.localRotation.eulerAngles.z && -270 < transform.localRotation.eulerAngles.z;
                Spriter.flipY = IsRotA || IsRotB;
                Spriter.sortingOrder = 6;
            }
            else Spriter.sortingOrder = true == IsRev ? 6 : 4;
        }
    }
}
