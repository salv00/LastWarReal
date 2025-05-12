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
        // D�sactive ou d�truit le Player
        gameObject.SetActive(false);
    }

    void OnDestroy()
    {
        // Pour �viter d�accrocher plusieurs fois l��v�nement
        hc.OnHealthEmpty -= HandlePlayerDeath;
    }
}
