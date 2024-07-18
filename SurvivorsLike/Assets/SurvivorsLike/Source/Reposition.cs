using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        // �±װ� Area�� ���� �۵�
        if (false == collision.CompareTag("Area")) return;

        // �÷��̾� ��ġ
        Vector3 PlayerPosition = GameManager.Instance.MainPlayer.transform.position;
        // �� ��ġ
        Vector3 MapPosition = transform.position;

        float DiffPosX = Mathf.Abs(PlayerPosition.x - MapPosition.x);
        float DiffPosY = Mathf.Abs(PlayerPosition.y - MapPosition.y);

        // �÷��̾� ����
        Vector3 PlayerDirection = GameManager.Instance.MainPlayer.InputVec;
        float DirX = 0 > PlayerDirection.x ? -1 : 1;
        float DirY = 0 > PlayerDirection.y ? -1 : 1;

        switch (transform.tag)
        {
            case "Ground":
                if (DiffPosX > DiffPosY) transform.Translate(Vector3.right * DirX * 40);
                else if (DiffPosX < DiffPosY) transform.Translate(Vector3.up * DirY * 40);
                break;
            default:
                break;
        }
    }
}
