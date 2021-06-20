using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarWithFOV2D : MonoBehaviour
{
    private FOV2D fov2D;

    // 가까운 객체 탐색
    public bool isNearObject = true;

    [HideInInspector]
    public GameObject objTarget;

    public void Awake()
    {
        fov2D = GetComponent<FOV2D>();
    }

    public void FixedUpdate()
    {
        if (fov2D.isTargetInside)
        {
            // 반경 안
            if (isNearObject)
            {
                objTarget = GetNearestTarget();
            }

            else // 반경 밖
            {
                objTarget = GetOutOfRadiusTarget();
            }
        } else
            objTarget = null;
    }

    public GameObject GetNearestTarget()
    {
        if (fov2D.visibleTargets[0] == null)
            return null;

        float dis = Vector2.Distance(transform.position, fov2D.visibleTargets[0].position);

        int count = 0;
        for(int i = 0; i < fov2D.visibleTargets.Count; i++)
        {
            if (fov2D.visibleTargets[i] == null)
                continue;

            if(Vector2.Distance(transform.position,fov2D.visibleTargets[i].position)<dis)
            {
                dis = Vector2.Distance(transform.position, fov2D.visibleTargets[i].position);
                count = i;
            }
        }

        if (fov2D.visibleTargets.Count == 0)
            return null;

        return fov2D.visibleTargets[count].gameObject;
    }

    public GameObject GetOutOfRadiusTarget()
    {
        if (fov2D.visibleTargets[0] == null)
            return null;

        float dist = Vector2.Distance(transform.position, fov2D.visibleTargets[0].position);
        int count = 0;
        for (int i = 0; i < fov2D.visibleTargets.Count; i++)
        {
            if (fov2D.visibleTargets[i] == null)
                continue;

            if (Vector2.Distance(transform.position, fov2D.visibleTargets[i].position) > dist)
            {
                dist = Vector2.Distance(transform.position, fov2D.visibleTargets[i].position);
                count = i;
            }
        }

        if (fov2D.visibleTargets.Count == 0)
            return null;

        return fov2D.visibleTargets[count].gameObject;
    }
}
