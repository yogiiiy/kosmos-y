using UnityEngine;

public class NightManager : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;

    [Header("Spawn interval per Night (detik) — index 0 = Night 1")]
    [SerializeField] private float[] spawnIntervalPerNight = { 4f, 3.5f, 3f, 2.5f, 2f };

    public int CurrentNight { get; private set; } = 1;

    private void Start()
    {
        // Start() dijamin jalan SETELAH semua Awake() di scene selesai,
        // jadi TimeManager.Instance udah pasti ke-set di sini —
        // beda sama OnEnable yang urutannya gak dijamin antar-objek.
        TimeManager.Instance.OnNightStarted += HandleNightStarted;
        TimeManager.Instance.OnNightEnded += HandleNightEnded;
    }

    private void OnDisable()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnNightStarted -= HandleNightStarted;
            TimeManager.Instance.OnNightEnded -= HandleNightEnded;
        }
    }

    private void HandleNightStarted()
    {
        int index = Mathf.Clamp(CurrentNight - 1, 0, spawnIntervalPerNight.Length - 1);
        float interval = spawnIntervalPerNight[index];
        enemySpawner.StartSpawning(interval);
    }

    private void HandleNightEnded()
    {
        enemySpawner.StopSpawning();
        CurrentNight++;
        // Night 6 (goblin horde/boss) belum ditangani di sini —
        // itu masuk Fase 4, logic-nya bakal beda (spawn sekaligus, bukan interval).
    }
}