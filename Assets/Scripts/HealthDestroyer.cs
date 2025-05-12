using UnityEngine;
using Ilumisoft.HealthSystem;

[RequireComponent(typeof(HealthComponent))]
public class HealthDestroyer : MonoBehaviour
{
    private HealthComponent hc;
    public int pointsOnDeath = 10;  // Points � donner pour cet ennemi

    void Awake()
    {
        hc = GetComponent<HealthComponent>();
        hc.OnHealthEmpty += HandleDeath;
    }

    void HandleDeath()
    {
        // Ajoute les points avant la destruction
        GameManager.Instance.AddScore(pointsOnDeath);

        // D�truire l�ennemi
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        hc.OnHealthEmpty -= HandleDeath;
    }
}
