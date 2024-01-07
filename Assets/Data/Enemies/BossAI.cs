using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
namespace EarthDenfender
{

    public class BossAI : MonoBehaviour
    {
        [SerializeField] private Seeker seeker;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float nextWPDistance;
        private Transform target;
        private Path path;
        private Coroutine moveCoroutine;

        private void Start()
        {
            target = FindObjectOfType<PlayerController>().transform;
           InvokeRepeating("CalculatePath", 0f, 0.2f);
        }
        private void CalculatePath()
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(transform.position, target.position, OnPathCallBack);
            }
        }
        private void OnPathCallBack(Path p)
        {
            if (p.error) return;
            {
                path = p;
                MoveToTarget();
            }
        }
        private void MoveToTarget()
        {
            if (moveCoroutine != null) StopCoroutine(moveCoroutine);
            moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
        }
        /*IEnumerator MoveToTargetCoroutine()
        {
            int currentWayPoint = 0;
            
            while (currentWayPoint < path.vectorPath.Count)
            {
                Vector2 direction = ((Vector2)path.vectorPath[currentWayPoint] - (Vector2)transform.position).normalized;
                Vector3 force = direction * moveSpeed * Time.deltaTime;
                transform.position += force;
                float distance = Vector2.Distance(transform.position, path.vectorPath[currentWayPoint]);
                if (distance < nextWPDistance)
                {
                    currentWayPoint++;
                }
                yield return null ;
            }
        }*/
        IEnumerator MoveToTargetCoroutine()
        {
            int currentWayPoint = 0;

            while (currentWayPoint < path.vectorPath.Count)
            {
                Vector2 targetPosition = path.vectorPath[currentWayPoint];
                Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

                // Số liệu khoảng cách giữa BossAI và PlayerController
                float distanceToPlayer = Vector2.Distance(transform.position, target.position);

                // Khoảng cách muốn giữ giữa BossAI và PlayerController
                float desiredDistance = 2.0f;  // Đặt giá trị mong muốn của bạn

                // Kiểm tra và điều chỉnh vị trí nếu khoảng cách không đủ lớn
                if (distanceToPlayer < desiredDistance)
                {
                    // Điều chỉnh vị trí để giữ khoảng cách mong muốn
                    Vector3 adjustedPosition = targetPosition - direction * desiredDistance;
                    transform.position = Vector3.MoveTowards(transform.position, adjustedPosition, moveSpeed * Time.deltaTime);
                }
                else
                {
                    // Di chuyển theo đường đi bình thường
                    Vector3 force = direction * moveSpeed * Time.deltaTime;
                    transform.position += force;
                }

                // Kiểm tra điều kiện để chuyển đến Waypoint tiếp theo
                float distance = Vector2.Distance(transform.position, targetPosition);
                if (distance < nextWPDistance)
                {
                    currentWayPoint++;
                }

                yield return null;
            }
        }

    }
}
