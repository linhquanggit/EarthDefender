using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EarthDenfender
{
    public class BossController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform[] wayPoints;

        private int currentWayPoint;
        private void Update()
        {
            int nextWayPoint = currentWayPoint + 1;
            if (nextWayPoint > wayPoints.Length - 1)
                nextWayPoint = 0;
            transform.position = Vector3.MoveTowards(transform.position, wayPoints[nextWayPoint].position, moveSpeed * Time.deltaTime);
            if (transform.position == wayPoints[nextWayPoint].position)
                currentWayPoint = nextWayPoint;
        }
    }
}
