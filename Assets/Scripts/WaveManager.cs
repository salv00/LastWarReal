// WaveManager.cs
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Standard Enemies")]
    public GameObject enemyPrefab;
    public int enemiesPerWave = 5;
    public float timeBetweenWaves = 5f;

    [Header("Advanced Enemies")]
    public GameObject advancedEnemyPrefab;    // assigné en Inspector
    public int advancedEveryXWaves = 5;       // toutes les 5 vagues
    public float speedIncrementPerWave = 0.2f; // bonus de vitesse pour avancés

    private int currentWave = 0;
    private float timer = 0f;
    private FormationSpawner formation;

    void Awake()
    {
        formation = GetComponent<FormationSpawner>();
        if (formation == null)
            Debug.LogError("WaveManager: FormationSpawner manquant !");
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenWaves)
        {
            StartWave();
            timer = 0f;
        }
    }

    void StartWave()
    {
        currentWave++;
        Debug.Log($"Wave {currentWave} started!");

        // Détermine quel prefab utiliser
        bool isAdvancedWave = (currentWave % advancedEveryXWaves == 0);
        GameObject prefabToSpawn = isAdvancedWave && advancedEnemyPrefab != null
            ? advancedEnemyPrefab
            : enemyPrefab;

        // Prépare le spawner
        formation.enemyPrefab = prefabToSpawn;
        formation.SpawnFormation();

        // Si avancée, booste la vitesse de tous les nouveaux ennemis
        if (isAdvancedWave)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var e in enemies)
            {
                var script = e.GetComponent<Enemy>();
                if (script != null)
                    script.speed += currentWave * speedIncrementPerWave;
            }
        }
    }
}
