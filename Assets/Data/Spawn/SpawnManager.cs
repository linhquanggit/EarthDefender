using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EarthDenfender
{
    [System.Serializable]
    public class PopUpPool
    {
        public FloatingTextController prefab;
        public List<FloatingTextController> inactiveObjs;
        public List<FloatingTextController> activeObjs;
        public FloatingTextController Spawn(Vector3 position, Transform parent)
        {;
            if (inactiveObjs.Count == 0)
            {
                FloatingTextController newObj = GameObject.Instantiate(prefab, parent);
                newObj.transform.position = position;
                activeObjs.Add(newObj);
                return newObj;
            }
            else
            {
                FloatingTextController oldObj = inactiveObjs[0];
                oldObj.gameObject.SetActive(true);
                oldObj.transform.SetParent(parent);
                oldObj.transform.position = position;
                activeObjs.Add(oldObj);
                inactiveObjs.RemoveAt(0);
                return oldObj;
            }
        }
        public void Release(FloatingTextController obj)
        {
            if (activeObjs.Contains(obj))
            {
                activeObjs.Remove(obj);
                inactiveObjs.Add(obj);
                obj.gameObject.SetActive(false);
            }
        }
        public void Clear()
        {
            while (activeObjs.Count > 0)
            {
                FloatingTextController obj = activeObjs[0];
                obj.gameObject.SetActive(false);
                activeObjs.RemoveAt(0);
                inactiveObjs.Add(obj);

            }
        }

    }
    [System.Serializable]
    public class EnemiesPool
    {
        public EnemyController[] prefabs;
        public List<EnemyController> inactiveObjs;
        public List<EnemyController> activeObjs;
        public EnemyController Spawn(Vector3 position, Transform parent)
        {
            int index = Random.Range(0, prefabs.Length - 1);
            if (inactiveObjs.Count == 0)
            {
                EnemyController newObj = GameObject.Instantiate(prefabs[index], parent);
                newObj.transform.position = position;
                activeObjs.Add(newObj);
                return newObj;
            }
            else
            {
                EnemyController oldObj = inactiveObjs[0];
                oldObj.gameObject.SetActive(true);
                oldObj.transform.SetParent(parent);
                oldObj.transform.position = position;
                activeObjs.Add(oldObj);
                inactiveObjs.RemoveAt(0);
                return oldObj;
            }
        }
        public void Release(EnemyController obj)
        {
            if (activeObjs.Contains(obj))
            {
                activeObjs.Remove(obj);
                inactiveObjs.Add(obj);
                obj.gameObject.SetActive(false);
            }
        }
        public void Clear()
        {
            while (activeObjs.Count > 0)
            {
                EnemyController obj = activeObjs[0];
                obj.gameObject.SetActive(false);
                activeObjs.RemoveAt(0);
                inactiveObjs.Add(obj);

            }
        }

    }

    [System.Serializable]
    public class ProjectilePool
    {
        public ProjectileController[] prefab;
        private int curPrefab = 0;
        public List<ProjectileController> inactiveObjs;
        public List<ProjectileController> activeObjs;
        public ProjectileController Spawn(Vector3 position, Transform parent,bool isEnemies)
        {
            if(!isEnemies)
            {
                int index = GameController.Instance.GetLevel()-1;
                if(index >= curPrefab && index<= prefab.Length-1)
                {
                    curPrefab = index;
                    inactiveObjs.Clear();
                }
                if (inactiveObjs.Count == 0)
                {
                    ProjectileController newObj = GameObject.Instantiate(prefab[curPrefab], parent);
                    newObj.transform.position = position;
                    activeObjs.Add(newObj);
                    return newObj;
                }
                else
                {
                    ProjectileController oldObj = inactiveObjs[0];
                    oldObj.gameObject.SetActive(true);
                    oldObj.transform.SetParent(parent);
                    oldObj.transform.position = position;
                    activeObjs.Add(oldObj);
                    inactiveObjs.RemoveAt(0);
                    return oldObj;
                }
            }
            else
            {
                if (inactiveObjs.Count == 0)
                {
                    ProjectileController newObj = GameObject.Instantiate(prefab[0], parent);
                    newObj.transform.position = position;
                    activeObjs.Add(newObj);
                    return newObj;
                }
                else
                {
                    ProjectileController oldObj = inactiveObjs[0];
                    oldObj.gameObject.SetActive(true);
                    oldObj.transform.SetParent(parent);
                    oldObj.transform.position = position;
                    activeObjs.Add(oldObj);
                    inactiveObjs.RemoveAt(0);
                    return oldObj;
                }
            }

        }
        public void Release(ProjectileController obj)
        {
            if (activeObjs.Contains(obj))
            {
                activeObjs.Remove(obj);
                inactiveObjs.Add(obj);
                obj.gameObject.SetActive(false);
            }
        }
        public void Clear()
        {
            while (activeObjs.Count > 0)
            {
                ProjectileController obj = activeObjs[0];
                obj.gameObject.SetActive(false);
                activeObjs.RemoveAt(0);
                inactiveObjs.Add(obj);

            }
        }

    }

    [System.Serializable]
    public class ParticalFXPool
    {
        public ParticalFX prefab;
        public List<ParticalFX> inactiveObjs;
        public List<ParticalFX> activeObjs;
        public ParticalFX Spawn(Vector3 position, Transform parent)
        {
            if (inactiveObjs.Count == 0)
            {
                ParticalFX newObj = GameObject.Instantiate(prefab, parent);
                newObj.transform.position = position;
                activeObjs.Add(newObj);
                return newObj;
            }
            else
            {
                ParticalFX oldObj = inactiveObjs[0];
                oldObj.gameObject.SetActive(true);
                oldObj.transform.SetParent(parent);
                oldObj.transform.position = position;
                activeObjs.Add(oldObj);
                inactiveObjs.RemoveAt(0);
                return oldObj;
            }
        }
        public void Release(ParticalFX obj)
        {
            if (activeObjs.Contains(obj))
            {
                activeObjs.Remove(obj);
                inactiveObjs.Add(obj);
                obj.gameObject.SetActive(false);
            }
        }
        public void Clear()
        {
            while (activeObjs.Count > 0)
            {
                ParticalFX obj = activeObjs[0];
                obj.gameObject.SetActive(false);
                activeObjs.RemoveAt(0);
                inactiveObjs.Add(obj);

            }
        }

    }
    public class SpawnManager : MonoBehaviour
    {

        private static SpawnManager instance;
        public static SpawnManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<SpawnManager>();
                return instance;
            }
        }
        [SerializeField] private bool isActive;
        [SerializeField] private int totalGroups;
        [SerializeField] private float enemySpawnInterval;

        [SerializeField] private EnemiesPool enemiesPool;
        [SerializeField] private PopUpPool popUpPool;
        [SerializeField] private ProjectilePool playerProjPool;
        [SerializeField] private ProjectilePool enemyProjPool;
        [SerializeField] private ParticalFXPool hitFXPool;
        [SerializeField] private ParticalFXPool playerGetHitFXPool;
        [SerializeField] private ParticalFXPool shootingFXPool;
        [SerializeField] private ParticalFXPool destroyEnemyFXPool;
        [SerializeField] private ParticalFXPool destroyPlayerFXPool;
        [SerializeField] private PlayerController playerControllerPrefab;


        public PlayerController Player => player;
        private PlayerController player;
        private WaveData currentWay;
        private Vector2 spawnPos;


        private bool isSpawningEnemies;
        private int enemyIndex;
        private int numEnemies;

        private float topLeftX;
        private float topLeftY;
        private float topRightX;

        
        private void Start()
        {
            Vector3 topLeftPoint = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
             topLeftX = topLeftPoint.x;
             topLeftY = topLeftPoint.y;
            topRightX = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x;
            
        }
        private void Update()
        {
            spawnPos = new Vector2(Random.Range(topLeftX, topRightX), topLeftY);
        }
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy(gameObject);
        }
        public void StartBattle(WaveData wave, bool resetPos)
        {
            currentWay = wave;
            numEnemies = currentWay.totalEnemies;
            totalGroups = currentWay.totalGroups;
            if (player == null)
                player = Instantiate(playerControllerPrefab);
            if (resetPos)
                player.transform.position = Vector3.zero;
            StartCoroutine(IESpawnGroups(totalGroups));
        }
        private IEnumerator IESpawnGroups(int groups)
        {
            isSpawningEnemies = true;
            for (int i = 0; i < groups; i++)
            {
                int enem = numEnemies;
                yield return StartCoroutine(IESpawnEnemies(enem, spawnPos));
                if (i < groups - 1)
                    yield return new WaitForSeconds(3f / currentWay.speedMultiplier);
            }
            isSpawningEnemies = false;

        }
        public bool IsClear()
        {
            if (isSpawningEnemies || enemiesPool.activeObjs.Count > 0)
                return false;
            return true;
        }
        private IEnumerator IESpawnEnemies(int totalEnemies, Vector2 position)
        {
            for (int i = 0; i < totalEnemies; i++)
            {
                yield return new WaitUntil(() => isActive);
                yield return new WaitForSeconds(enemySpawnInterval / currentWay.speedMultiplier);
                EnemyController enemy = enemiesPool.Spawn(spawnPos, transform);
                enemy.Init(currentWay.speedMultiplier, currentWay.enemyMaxHp);
            }
        }
        public void ReleaseEnemy(EnemyController obj)
        {
            enemiesPool.Release(obj);
        }

        public void ReleaseEnemyController(EnemyController enemy)
        {
            enemiesPool.Release(enemy);
        }

        public ProjectileController SpawnEnemyProjectile(Vector3 position)
        {
            ProjectileController obj = enemyProjPool.Spawn(position, transform,true);
            obj.SetFromPlayer(false);
            return obj;
        }

        public void ReleaseEnemyProjectile(ProjectileController projectile)
        {
            enemyProjPool.Release(projectile);
        }

        public ProjectileController SpawnPlayerProjectile(Vector3 position)
        {
            ProjectileController obj = playerProjPool.Spawn(position, transform,false);
            obj.SetFromPlayer(true);
            return obj;
        }


        public void ReleasePlayerProjectile(ProjectileController projectile)
        {
            playerProjPool.Release(projectile);
        }

        public ParticalFX SpawnHitFX(Vector3 position)
        {
            ParticalFX fx = hitFXPool.Spawn(position, transform);
            fx.SetPool(hitFXPool);
            return fx;
        }
        public void ReleaseHitFX(ParticalFX obj)
        {
            hitFXPool.Release(obj);
        }

        public ParticalFX SpawnPlayerGetHitFX(Vector3 position)
        {
            ParticalFX fx = playerGetHitFXPool.Spawn(position, transform);
            fx.SetPool(playerGetHitFXPool);
            return fx;
        }
        public void ReleasePlayerGetHitFX(ParticalFX obj)
        {
            playerGetHitFXPool.Release(obj);
        }
        public ParticalFX SpawnShootingFX(Vector3 position)
        {
            ParticalFX fx = shootingFXPool.Spawn(position, transform);
            fx.SetPool(shootingFXPool);
            return fx;
        }
        public void ReleaseShootingFX(ParticalFX obj)
        {
            shootingFXPool.Release(obj);

        }

        public ParticalFX SpawnDestroyEnemyFX(Vector3 position)
        {
            ParticalFX fx = destroyEnemyFXPool.Spawn(position, transform);
            fx.SetPool(destroyEnemyFXPool);
            return fx;
        }
        public void ReleaseDestroyEnemyFX(ParticalFX obj)
        {
            destroyEnemyFXPool.Release(obj);
        }

        public ParticalFX SpawnDestroyPlayerFX(Vector3 position)
        {
            ParticalFX fx = destroyPlayerFXPool.Spawn(position, transform);
            fx.SetPool(destroyPlayerFXPool);
            return fx;
        }
        public void ReleasePlayerDestroyedFX(ParticalFX obj)
        {
            destroyPlayerFXPool.Release(obj);
        }


        public FloatingTextController SpawnFloatingText(Vector3 position)
        {
            FloatingTextController text = popUpPool.Spawn(position, transform);
            return text;
        }
        public void ReleaseFloatingText(FloatingTextController obj)
        {
                popUpPool.Release(obj);
        }

        public void Clear()
        {
            enemiesPool.Clear();
            enemyProjPool.Clear();
            playerProjPool.Clear();
            hitFXPool.Clear();
            destroyEnemyFXPool.Clear();
            destroyPlayerFXPool.Clear();
            shootingFXPool.Clear();
            StopAllCoroutines();
        }

    }
}