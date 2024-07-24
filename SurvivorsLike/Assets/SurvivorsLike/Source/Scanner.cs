using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public RaycastHit2D[]   Targets;
    public LayerMask        TargetLayer;
    public Transform        NearestTarget;
    public float            ScanRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Targets = Physics2D.CircleCastAll(transform.position, ScanRange, Vector2.zero, 0, TargetLayer);
        NearestTarget = GetNearest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Custom
    Transform GetNearest()
    {
        Transform Result = null;
        float Diff = 100.0f;

        Vector3 PlayerPos = transform.position;
        foreach(RaycastHit2D Target in Targets)
        {
            Vector3 TargetPos = Target.transform.position;
            float CurDiff = Vector3.Distance(PlayerPos, TargetPos);

            // 플레이어와의 거리차가 제일 적은 값 구하기
            if (Diff > CurDiff)
            {
                Diff = CurDiff;
                Result = Target.transform;
            }
        }

        return Result;
    }

    #endregion
}
