using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EarthDenfender
{
    public class BossPath : MonoBehaviour
    {
        [SerializeField] private Transform[] wayPoints;
        private void OnDrawGizmos()
        {
            if(wayPoints != null && wayPoints.Length > 1)
            {
                Gizmos.color = Color.red;
                for(int i =0;i<wayPoints.Length-1;i++)
                {
                    Transform from = wayPoints[i];
                    Transform to = wayPoints[i + 1];
                    Gizmos.DrawLine(from.position, to.position);
                }
                Gizmos.DrawLine(wayPoints[0].position, wayPoints[wayPoints.Length - 1].position);
            }
        }
    }
}
