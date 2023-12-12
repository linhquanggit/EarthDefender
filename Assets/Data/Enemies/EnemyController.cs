using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EarthDenfender
{
    public class EnemyController : MonoBehaviour
    {

        [SerializeField] private Vector2 moveDir;
        [SerializeField] private float moveSpeed;
        [SerializeField] private bool isActive;
        [SerializeField] protected Transform firePoint;
        [SerializeField] protected float minFireCoolDown;
        [SerializeField] protected float maxFireCoolDown;
        [SerializeField] protected int collisionDamage;
        [SerializeField] protected int enemyExp;
        // [SerializeField] protected int enemyHp;

        protected int enemyCurrentHp;
        protected float tempCoolDown;

        private float currentMoveSpeed;
        private float speedMultiplier;

        private void Update()
        {
            if (!isActive)
                return;
            transform.Translate(moveDir * currentMoveSpeed * Time.deltaTime);
            if (tempCoolDown <= 0)
            {
                Fire();
                tempCoolDown = Random.Range(minFireCoolDown, maxFireCoolDown);
            }
            tempCoolDown -= Time.deltaTime;
            if (transform.position.y <= -5f)
                SpawnManager.Instance.ReleaseEnemy(this);
            GameController.Instance.CheckWave();
            
        }
        public void Init(float speedMul, int maxHp)
        {
            speedMultiplier = speedMul;
            currentMoveSpeed = moveSpeed * speedMul;
            isActive = true;
            tempCoolDown = Random.Range(minFireCoolDown, maxFireCoolDown / speedMul);
            enemyCurrentHp = maxHp;
        }
        protected virtual void Fire()
        {
            // EnemyProjectile projectileCtrl = Instantiate(projectile, firePoint.position, Quaternion.identity, null);
            ProjectileController projectileCtrl = SpawnManager.Instance.SpawnEnemyProjectile(firePoint.position); ;
            projectileCtrl.Fire(speedMultiplier);
            AudioManager.Instance.PlayBombSFXClip();
        }

        public void GetHit(int playerDamage)
        {
            enemyCurrentHp -= playerDamage;
            if (enemyCurrentHp <= 0)
            {
                SpawnManager.Instance.SpawnDestroyEnemyFX(transform.position);
                SpawnManager.Instance.ReleaseEnemy(this);
                GameController.Instance.AddExp(enemyExp);
                AudioManager.Instance.PlayDestroySFXClip();
            }
            AudioManager.Instance.PlayHitSFXClip();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerController player;
                collision.gameObject.TryGetComponent(out player);
                player.GetHit(collisionDamage);
                SpawnManager.Instance.ReleaseEnemy(this);
                GameController.Instance.CheckWave();
                Vector3 hitPos = collision.ClosestPoint(transform.position);
                SpawnManager.Instance.SpawnPlayerGetHitFX(hitPos);
            }
        }

    }
}