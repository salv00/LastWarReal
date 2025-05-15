using UnityEngine;
using Ilumisoft.HealthSystem;

public class Bullet : MonoBehaviour
{
    public int damage = 1;
    public float lifeTime = 5f;

    void Start() => Destroy(gameObject, lifeTime);

    void OnTriggerEnter(Collider other)
    {
        // Tenter de trouver un HealthComponent sur l'objet touché ou ses parents
        var hc = other.GetComponentInParent<HealthComponent>();
        if (hc != null)
        {
            hc.ApplyDamage(damage);
            Destroy(gameObject);
        }
    }
}
