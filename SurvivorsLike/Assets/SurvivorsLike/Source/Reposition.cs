using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    Collider2D Col;

    private void Awake()
    {
        Col = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // 태그가 Area일 때만 작동
        if (false == collision.CompareTag("Area")) return;

        // 플레이어 위치
        Vector3 PlayerPosition = GameManager.Instance.MainPlayer.transform.position;
        // 맵 위치
        Vector3 MapPosition = transform.position;

        switch (transform.tag)
        {
            case "Ground":
            {
                // 플레이어 방향
                float DirX = PlayerPosition.x - MapPosition.x;
                float DirY = PlayerPosition.y - MapPosition.y;

                float DiffPosX = Mathf.Abs(DirX);
                float DiffPosY = Mathf.Abs(DirY);

                DirX = 0 > DirX ? -1 : 1;
                DirY = 0 > DirY ? -1 : 1;

                if (DiffPosX > DiffPosY) transform.Translate(Vector3.right * DirX * 40);
                else if (DiffPosX < DiffPosY) transform.Translate(Vector3.up * DirY * 40);
                break;
            }
            case "Enemy":
            {
                if (true == Col.enabled)
                {
                    // 거리차이로 위치 재배치
                    Vector3 Dist = PlayerPosition - MapPosition;
                    transform.Translate(Dist * 2 + new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), 0));
                }
                break;
            }
            default:
                break;
        }
    }
}
