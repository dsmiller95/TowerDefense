using UnityEngine;

[RequireComponent(typeof(PointTowardsComponent))]
public class BulletSpawnComponent : MonoBehaviour
{
    public float secondsPerBullet;
    public PointTowardsComponent bulletPrefab;
    private float lastFiringTime = 0;

    private void Update()
    {
        if (lastFiringTime + secondsPerBullet < Time.time)
        {
            lastFiringTime = Time.time;
            FireBullet();
        }
    }

    private void FireBullet()
    {
        var target = GetComponent<PointTowardsComponent>().target;
        if (target == null)
        {
            return;
        }

        var newBullet = Instantiate(bulletPrefab);
        newBullet.target = target;

        newBullet.transform.position = transform.position;
    }
}
