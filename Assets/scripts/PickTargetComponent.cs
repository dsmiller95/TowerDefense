using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PointTowardsComponent))]
public class PickTargetComponent : MonoBehaviour
{
    private void Update()
    {
        this.GetComponent<PointTowardsComponent>().target = this.PickTarget();
    }

    private GameObject PickTarget()
    {
        IEnumerable<EnemyComponent> targets = GameObject.FindObjectsOfType<EnemyComponent>();
        targets = targets.OrderBy(x => Vector3.Distance(transform.position, x.transform.position));
        return targets.FirstOrDefault()?.gameObject;
    }
}
