using UnityEngine;
using Ilumisoft.HealthSystem;

[RequireComponent(typeof(HealthComponent))]
public class HealthDestroyer : MonoBehaviour
{
    private HealthComponent hc;
    public int pointsOnDeath = 10;  // Points à donner pour cet ennemi

    void Awake()
    {
        hc = GetComponent<HealthComponent>();
        hc.OnHealthEmpty += HandleDeath;
    }

    void HandleDeath()
    {
        // Ajoute les points avant la destruction
        GameManager.Instance.AddScore(pointsOnDeath);

        // Détruire l’ennemi
        Destroy(gameObject);
    }

    void OnDestroy()
    {
        hc.OnHealthEmpty -= HandleDeath;
    }
}
