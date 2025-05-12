using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   // TON prefab �Enemy�
    public Transform player;         // La r�f�rence au joueur
    public float spawnRadius = 10f;  // Rayon autour du joueur o� spawn
    public float spawnInterval = 2f; // Temps (s) entre chaque spawn

    private float timer = 0f;

    void Update()
    {

        // Si le joueur n�existe plus, on stoppe le spawner
        if (player == null) return;

        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            // G�n�re une position al�atoire sur une sph�re autour du joueur
            Vector3 spawnPos = player.position + Random.onUnitSphere * spawnRadius;
            spawnPos.y = 0;  // Assure que l�ennemi reste sur le sol
            // Instancie l�ennemi et lui passe la r�f�rence du joueur pour le suivi
            GameObject e = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            e.GetComponent<Enemy>().player = player;
            timer = 0f;
        }
    }
}
