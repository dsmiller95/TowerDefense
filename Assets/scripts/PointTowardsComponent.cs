using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointTowardsComponent : MonoBehaviour
{
    public GameObject target;

    private void FixedUpdate()
    {
        if(target != null)
        {
            transform.LookAt(target.transform, Vector3.up);
        }
    }
}
