/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EarthDenfender
{
    public class CameraBoundary : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player")) // Kiểm tra xem GameObject có ra khỏi màn hình không
            {
                Debug.Log("Player Kiaaaa!!!");
                Vector3 position = transform.position;
                position.x = Mathf.Clamp(position.x, -4.8f, 4.8f); // Giới hạn vị trí theo chiều ngang
                position.y = Mathf.Clamp(position.y, -2.5f, 2.5f); // Giới hạn vị trí theo chiều dọc
                transform.position = position;
            }
        }
    }
}*/