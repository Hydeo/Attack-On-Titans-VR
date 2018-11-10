using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitansFieldOfView : MonoBehaviour {

    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();
    public List<float> distTargets = new List<float>();

    private void Start()
    {
        StartCoroutine("FindTargetWithDelay",.2f);
    }

    IEnumerator FindTargetWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTargets();
        }
    }

    void FindVisibleTargets()
    {
        visibleTargets.Clear();
        distTargets.Clear();
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for(int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirtoTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirtoTarget) < viewAngle / 2)
            {
                float dsttoTarget = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirtoTarget, dsttoTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    distTargets.Add(dsttoTarget);
                }
            }
        }
    }


    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
