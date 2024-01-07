using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EarthDenfender
{
    public class ProjectileController : MonoBehaviour
    {
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected Vector2 direction;
        [Range(1, 100)] public int minDamage;
        [Range(1, 100)] public int maxDamage;
        private int damage;
        private bool fromPlayer;
        private bool isMaxDame;
        private float lifeTime;
        private float currentMoveSpeed;

        void Update()
        {
            transform.Translate(Time.deltaTime * direction * currentMoveSpeed);
            if (lifeTime <= 0)
                Release();
            lifeTime -= Time.deltaTime;
            damage = Random.Range(minDamage, maxDamage);
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

                if (damage < maxDamage - 1)
                {
                    isMaxDame = false;
                }
                else
                {
                    isMaxDame = true;
                }
                enemy.GetHit(damage, isMaxDame);
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                if (fromPlayer)
                    SpawnManager.Instance.ReleasePlayerProjectile(this);
                else
                    SpawnManager.Instance.ReleaseEnemyProjectile(this);
                Vector3 hitPos = collision.ClosestPoint(transform.position);
                SpawnManager.Instance.SpawnPlayerGetHitFX(hitPos);
                PlayerController player=FindObjectOfType<PlayerController>();
                collision.gameObject.TryGetComponent(out player);
                player.GetHit(damage);
            }
        }
    }
}