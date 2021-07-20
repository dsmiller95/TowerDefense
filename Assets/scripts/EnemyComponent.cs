using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    private void FixedUpdate()
    {
        // moving the component must happen in FixedUpdate, so the physics system has time to update
        //  otherwise, the collider position will no longer be in sync with the actual transform position
        transform.position += Vector3.forward * Time.fixedDeltaTime;
    }
}
