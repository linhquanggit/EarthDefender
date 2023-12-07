using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace EarthDenfender
{
    public class ParticalFX : MonoBehaviour
    {
        [SerializeField] private float lifeTime;
        [SerializeField] ParticalFXPool particalFXPool;
        private float curLifeTime;
        private void OnEnable()
        {
            curLifeTime = lifeTime;
        }

        private void Update()
        {
            if (curLifeTime <= 0)
            {
                particalFXPool.Release(this);
            }
            curLifeTime -= Time.deltaTime;
        }

        public void SetPool(ParticalFXPool pool)
        {
            particalFXPool = pool;
        }

    }
}