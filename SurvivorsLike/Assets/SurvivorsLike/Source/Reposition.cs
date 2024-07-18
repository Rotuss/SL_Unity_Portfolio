using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        // 태그가 Area일 때만 작동
        if (false == collision.CompareTag("Area")) return;

        // 플레이어 위치
        Vector3 PlayerPosition = GameManager.Instance.MainPlayer.transform.position;
        // 맵 위치
        Vector3 MapPosition = transform.position;

        float DiffPosX = Mathf.Abs(PlayerPosition.x - MapPosition.x);
        float DiffPosY = Mathf.Abs(PlayerPosition.y - MapPosition.y);

        // 플레이어 방향
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
