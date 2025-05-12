using UnityEngine;
using Ilumisoft.HealthSystem;

public class EnemyAdvanced : Enemy
{
    public GameObject projectilePrefab;  // Prefab du projectile ennemi
    public float fireInterval = 3f;      // Intervalle entre tirs
    private float fireTimer;

    protected override void Start()
    {
        base.Start();

        // Configure la vie max via HealthComponent
        var hc = GetComponent<HealthComponent>();
        if (hc != null)
        {
            hc.MaxHealth = 10;           // Vie maximale
            hc.CurrentHealth = 10;       // Remet la vie à fond
        }

        // Décale le premier tir de façon aléatoire
        fireTimer = Random.Range(0f, fireInterval);
    }

    protected override void Update()
    {
        base.Update();
        if (GameManager.Instance.isGameOver) return;
        if (player == null || projectilePrefab == null) return;

        fireTimer += Time.deltaTime;
        if (fireTimer >= fireInterval)
        {
            fireTimer = 0f;
            Vector3 dir = (player.position - transform.position).normalized;
            Vector3 spawnPos = transform.position + dir * 1.5f;

            var proj = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(dir));
            var rb = proj.GetComponent<Rigidbody>();
            if (rb != null)
                rb.velocity = dir * 20f;
            else
                Debug.LogWarning("Projectile prefab has no Rigidbody!");
        }
    }
}
