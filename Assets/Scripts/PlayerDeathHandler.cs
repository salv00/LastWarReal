using UnityEngine;
using Ilumisoft.HealthSystem;

[RequireComponent(typeof(HealthComponent))]
public class PlayerDeathHandler : MonoBehaviour
{
    private HealthComponent hc;

    void Awake()
    {
        hc = GetComponent<HealthComponent>();
        hc.OnHealthEmpty += HandlePlayerDeath;
    }

    void HandlePlayerDeath()
    {
        // Affiche le Game Over
        GameManager.Instance.GameOver();
        // Désactive ou détruit le Player
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        // Pour éviter d’accrocher plusieurs fois l’événement
        hc.OnHealthEmpty -= HandlePlayerDeath;
    }
}
