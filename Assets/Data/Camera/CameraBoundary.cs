using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace EarthDenfender
{
    public class CameraBoundary : MonoBehaviour
    {

        private static CameraBoundary instance;
        public static CameraBoundary Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType<CameraBoundary>();
                return instance;
            }
        }

        private Vector3 topRightCorner;
        private Vector3 topLeftCorner;
        private Vector3 bottomLeftCorner;
        private Vector3 bottomRightCorner;
        private void Start()
        {
            topRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            topLeftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
            bottomLeftCorner = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
            bottomRightCorner = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));

        }

        public Vector3 TopRightCorner()
        {
            return topRightCorner;
        }
        public Vector3 BottomLeftCorner()
        {
            return bottomLeftCorner;
        }
        public Vector3 BottomRightCorner()
        {
            return bottomRightCorner;
        }
        public Vector3 TopLeftCorner()
        {
            return topLeftCorner;
        }
    }

}