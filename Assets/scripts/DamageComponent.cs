using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class DamageComponent : MonoBehaviour
{
    public LayerMask damageTarget;
    public float damage;

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & damageTarget) != 0)
        {
            // layer of trigger enter is included in the damager layer mask
            var targetHealth = other.gameObject.GetComponentInParent<IHealthy>();
            targetHealth?.DamageHealth(damage);
            Destroy(this.gameObject);
        }
    }
}
