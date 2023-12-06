using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelCtrl : MonoBehaviour
{

    [SerializeField] protected float rotationSpeed;
    void Update()
    {
        RotateProjectile();
    }
    protected virtual void RotateProjectile()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
    }
}
