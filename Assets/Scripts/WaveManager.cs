using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave Settings")]
    public int enemiesPerWave = 5;
    public float timeBetweenWaves = 5f;

    [Header("Advanced Wave")]
    [Tooltip("Toutes les X vagues seront avancées")]
    public int advancedEveryXWaves = 5;
    [Tooltip("Bonus de vitesse à appliquer aux vagues avancées")]
    public float speedIncrementPerWave = 0.2f;

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

        bool isAdvanced = (currentWave % advancedEveryXWaves == 0);

        // Utilise la nouvelle signature
        formation.SpawnFormation(isAdvanced);

        // Si vague avancée, booste la vitesse
        if (isAdvanced)
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
