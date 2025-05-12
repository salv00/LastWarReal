// FormationSpawner.cs
using UnityEngine;

public class FormationSpawner : MonoBehaviour
{
    [Header("Formation Settings")]
    public GameObject enemyPrefab;  // sera réécrit à chaque vague
    public Transform spawnPoint;    // MainSpawnPoint devant le joueur
    public int rows = 2;
    public int columns = 5;
    public float spacing = 2f;

    public void SpawnFormation()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("FormationSpawner: enemyPrefab not assigned");
            return;
        }
        if (spawnPoint == null)
        {
            Debug.LogError("FormationSpawner: spawnPoint not assigned");
            return;
        }

        Vector3 basePos = spawnPoint.position;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                // Calcul de la position en formation
                Vector3 offset = new Vector3(
                    c * spacing - (columns - 1) * spacing / 2f,
                    0,
                    r * spacing
                );
                Vector3 spawnPos = basePos + offset;

                // Instanciation
                var enemyGO = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
                enemyGO.tag = "Enemy";  // important pour FindGameObjectsWithTag

                // Orientation vers le joueur
                var playerT = GameObject.FindWithTag("Player")?.transform;
                if (playerT != null)
                    enemyGO.transform.LookAt(playerT);

                var eScript = enemyGO.GetComponent<Enemy>();
                if (eScript != null)
                    eScript.player = playerT;


                // Si c'est un EnemyAdvanced, il contiendra son propre script et sa vie augmentée
            }
        }
    }
}
