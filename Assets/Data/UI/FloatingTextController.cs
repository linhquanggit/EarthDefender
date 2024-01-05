using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EarthDenfender
{

    public class FloatingTextController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }
        private void Release()
        {
            SpawnManager.Instance.ReleaseFloatingText(this);
        }
        // Update is called once per frame
        void Update()
        {
            Invoke("Release", 1f);
        }
    }
}
