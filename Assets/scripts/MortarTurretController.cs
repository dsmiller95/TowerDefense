using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MortarTurretController : MonoBehaviour
{
    public LayerMask bulletBlockers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        IEnumerable<EnemyComponent> targets = GameObject.FindObjectsOfType<EnemyComponent>();
        targets = targets.OrderBy(x => Vector3.Distance(transform.position, x.transform.position));

        EnemyComponent validEnemy = null;
        foreach (var nextTarget in targets)
        {
            var rayToTarget = new Ray(transform.position, nextTarget.transform.position - transform.position);
            Debug.DrawRay(rayToTarget.origin, rayToTarget.direction, Color.red);
            if (Physics.Raycast(rayToTarget, out RaycastHit hit, 100, bulletBlockers.value))
            {
                var hitObject = hit.collider.gameObject;
                var hitEnemy = hitObject?.GetComponentInParent<EnemyComponent>();
                if (hitEnemy != null)
                {
                    validEnemy = hitEnemy;
                    break;
                } else
                {
                    Debug.Log($"Could not raycast to {nextTarget}");
                }
            }
        }

        if(validEnemy == null)
        {
            return;
        }

        this.AimtAt(validEnemy.gameObject);
    }

    private void AimtAt(GameObject target)
    {
        transform.LookAt(target.transform, Vector3.up);
    }
}
