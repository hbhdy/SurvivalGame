using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV2D : MonoBehaviour
{
    public Color circleColor = Color.white;

    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    [HideInInspector]
    public bool isTargetInside = false;

    public void OnEnable()
    {
        StartCoroutine("FindTargetsWithDelay", .2f);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    public void FindVisibleTargets()
    {
        visibleTargets.Clear();
        
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll((Vector2)transform.position, viewRadius, targetMask);

        // 레이어에 해당하는 충돌체 탐색 ( 씬에서 GUI를 통한 거리표시 )
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.up, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }

        // 반경안에 발견됨
        if (visibleTargets.Count > 0)
        {
            isTargetInside = true;
        }
        else
        {
            isTargetInside = false;
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
