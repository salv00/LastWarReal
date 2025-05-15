using UnityEngine;

public class FormationSpawner : MonoBehaviour
{
    [Header("Formation Settings")]
    public GameObject[] standardPrefabs;  // zombies
    public GameObject[] advancedPrefabs;  // golems
    public Transform spawnPoint;
    public int rows = 2;
    public int columns = 5;
    public float spacing = 2f;

    /// <summary>
    /// Spawn une formation soit de prefabs standards, soit de prefabs avancés.
    /// </summary>
    public void SpawnFormation(bool useAdvanced = false)
    {
        // Choix du tableau
        var source = useAdvanced ? advancedPrefabs : standardPrefabs;
        if (source == null || source.Length == 0)
        {
            Debug.LogError("FormationSpawner: Aucun prefab dans " +
                (useAdvanced ? "advancedPrefabs" : "standardPrefabs") + " !");
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
                Vector3 offset = new Vector3(
                    c * spacing - (columns - 1) * spacing / 2f,
                    0,
                    r * spacing
                );
                Vector3 spawnPos = basePos + offset;

                // Instancie un prefab aléatoire du tableau sélectionné
                var prefab = source[Random.Range(0, source.Length)];
                var enemyGO = Instantiate(prefab, spawnPos, Quaternion.identity);
                enemyGO.tag = "Enemy";

                // Orientation vers le joueur
                var playerT = GameObject.FindWithTag("Player")?.transform;
                if (playerT != null)
                {
                    enemyGO.transform.LookAt(playerT);
                    var eScript = enemyGO.GetComponent<Enemy>();
                    if (eScript != null)
                        eScript.player = playerT;
                }
            }
        }
    }
}
