// --- PlayerShooting.cs ---
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float shootCooldown = 0.5f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        GameObject nearest = FindNearestEnemy();
        if (nearest != null && timer >= shootCooldown)
        {
            // 1) Oriente le joueur uniquement en yaw (Y) vers l’ennemi
            Vector3 dir = (nearest.transform.position - transform.position).normalized;
            dir.y = 0;  // on ignore l’axe vertical
            if (dir.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(dir);

            // 2) Tire
            Shoot(dir);
            timer = 0f;
        }
    }

    void Shoot(Vector3 dir)
    {
        var bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = dir * 10f;
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float minDist = Mathf.Infinity;
        GameObject nearest = null;

        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        return nearest;
    }
}
