using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(PointTowardsComponent))]
public class MortarTurretController : MonoBehaviour
{
    public LayerMask bulletBlockers;

    public float secondsPerBullet;
    public PointTowardsComponent bulletPrefab;


    private float lastFiringTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        this.GetComponent<PointTowardsComponent>().target = this.PickTarget();
        if(lastFiringTime + secondsPerBullet < Time.time)
        {
            lastFiringTime = Time.time;
            this.FireBullet();
        }
    }

    private GameObject PickTarget()
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
                }
                else
                {
                    Debug.Log($"Could not raycast to {nextTarget}");
                }
            }
        }

        return validEnemy?.gameObject;
    }

    private void FireBullet()
    {
        var target = this.GetComponent<PointTowardsComponent>().target;
        if(target == null)
        {
            return;
        }

        var newBullet = Instantiate(bulletPrefab);
        newBullet.target = target;

        newBullet.transform.position = this.transform.position;
    }
}
