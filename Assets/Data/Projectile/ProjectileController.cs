using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EarthDenfender
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected int damage;
        [SerializeField] protected Vector2 direction;

        private bool fromPlayer;
        private float lifeTime;
        private float currentMoveSpeed;

        void Update()
        {
            transform.Translate(Time.deltaTime * direction * currentMoveSpeed);
            if (lifeTime <= 0)
                Release();
            lifeTime -= Time.deltaTime;
        }
        public void Fire(float speedMultiplier)
        {
            lifeTime = 10f / speedMultiplier;
            currentMoveSpeed = moveSpeed * speedMultiplier;
        }

        private void Release()
        {
            if (fromPlayer)
                SpawnManager.Instance.ReleasePlayerProjectile(this);
            else
                SpawnManager.Instance.ReleaseEnemyProjectile(this);
        }
        public void SetFromPlayer(bool value)
        {
            fromPlayer = value;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            //sDebug.Log("Trigger" + collision.gameObject.name);
            if (collision.gameObject.CompareTag("Enemy"))
            {
                if (fromPlayer)
                    SpawnManager.Instance.ReleasePlayerProjectile(this);
                else
                    SpawnManager.Instance.ReleaseEnemyProjectile(this);
                Vector3 hitPos = collision.ClosestPoint(transform.position);
                SpawnManager.Instance.SpawnHitFX(hitPos);
                EnemyController enemy;
                collision.gameObject.TryGetComponent(out enemy);
                enemy.GetHit(damage);

            }

            if (collision.gameObject.CompareTag("Player"))
            {
                if (fromPlayer)
                    SpawnManager.Instance.ReleasePlayerProjectile(this);
                else
                    SpawnManager.Instance.ReleaseEnemyProjectile(this);
                Vector3 hitPos = collision.ClosestPoint(transform.position);
                SpawnManager.Instance.SpawnPlayerGetHitFX(hitPos);
                PlayerController player;
                collision.gameObject.TryGetComponent(out player);
                player.GetHit(damage);
            }
        }
    }
}