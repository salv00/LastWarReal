using UnityEngine;
using Ilumisoft.HealthSystem;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var hc = other.GetComponent<HealthComponent>();
            if (hc != null)
                hc.ApplyDamage(damage);

            Destroy(gameObject);
        }
    }
}
