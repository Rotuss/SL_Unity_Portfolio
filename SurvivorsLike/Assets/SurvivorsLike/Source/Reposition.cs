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

        // �÷��̾� ����
        Vector3 PlayerDirection = GameManager.Instance.MainPlayer.InputVec;
        float DirX = PlayerPosition.x - MapPosition.x;
        float DirY = PlayerPosition.y - MapPosition.y;

        float DiffPosX = Mathf.Abs(DirX);
        float DiffPosY = Mathf.Abs(DirY);

        DirX = 0 > DirX ? -1 : 1;
        DirY = 0 > DirY ? -1 : 1;

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
