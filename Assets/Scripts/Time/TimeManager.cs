using System;
using UnityEngine;

/// <summary>
/// Jam in-game Kósmos Y v0.2.
/// Skala: 1 menit in-game = 1 detik real-time (Time.deltaTime).
/// Day: 06:00 -> 18:00 (berjalan bebas, gak ada auto-end).
/// Night: 18:00 -> 02:00 (durasi tetap, berakhir otomatis, di-drive dari sini,
/// bukan dari timer independen di NightManager lagi).
/// </summary>
public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public enum DayPhase { Day, Night }

    [Header("Config")]
    [SerializeField] private float gameMinutesPerRealSecond = 1f; // 1 menit in-game = 1 detik real
    [SerializeField] private int dayStartHour = 6;   // 06:00
    [SerializeField] private int nightStartHour = 18; // 18:00
    [SerializeField] private int nightEndHour = 2;    // 02:00 (setelah lewat tengah malam)

    public DayPhase CurrentPhase { get; private set; } = DayPhase.Day;
    public int CurrentHour { get; private set; }
    public int CurrentMinute { get; private set; }

    // Dipakai NightManager buat tau kapan Night harus berakhir otomatis
    private float nightDurationSeconds; // dihitung sekali pas Night dimulai
    private float nightElapsedSeconds;

    private float totalGameMinutes; // akumulator mentah, di-mod 1440 buat wrap harian

    // Event yang bisa didengar UI jam, NightManager, dll.
    public event Action<int, int> OnTimeChanged;      // (hour, minute) tiap kali menit berganti
    public event Action OnNightStarted;
    public event Action OnNightEnded;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        SetTime(dayStartHour, 0);
        CurrentPhase = DayPhase.Day;
    }

    private void Update()
    {
        float deltaGameMinutes = Time.deltaTime * gameMinutesPerRealSecond;
        totalGameMinutes += deltaGameMinutes;

        int prevMinute = CurrentMinute;
        UpdateClockFromTotalMinutes();

        if (CurrentMinute != prevMinute)
        {
            OnTimeChanged?.Invoke(CurrentHour, CurrentMinute);
        }

        if (CurrentPhase == DayPhase.Night)
        {
            nightElapsedSeconds += Time.deltaTime;
            if (nightElapsedSeconds >= nightDurationSeconds)
            {
                EndNight();
            }
        }
    }

    private void UpdateClockFromTotalMinutes()
    {
        float wrapped = totalGameMinutes % 1440f; // 1440 menit = 24 jam
        CurrentHour = Mathf.FloorToInt(wrapped / 60f);
        CurrentMinute = Mathf.FloorToInt(wrapped % 60f);
    }

    /// <summary>
    /// Set jam langsung ke nilai tertentu. Dipakai buat inisialisasi
    /// dan buat TavernNPC pas trigger "Siap ke Malam".
    /// </summary>
    public void SetTime(int hour, int minute)
    {
        totalGameMinutes = hour * 60f + minute;
        UpdateClockFromTotalMinutes();
        OnTimeChanged?.Invoke(CurrentHour, CurrentMinute);
    }

    /// <summary>
    /// Panggil ini dari TavernNPC saat pemain pilih "Siap ke Malam".
    /// </summary>
    public void StartNight()
    {
        SetTime(nightStartHour, 0);
        CurrentPhase = DayPhase.Night;
        nightElapsedSeconds = 0f;

        // 18:00 -> 02:00 = 8 jam in-game = 480 menit in-game = 480 detik real
        // (di skala 1:1). Dihitung dari config biar gampang di-tuning kalau
        // gameMinutesPerRealSecond diubah nanti.
        float nightDurationGameMinutes = ((24 - nightStartHour) + nightEndHour) * 60f;
        nightDurationSeconds = nightDurationGameMinutes / gameMinutesPerRealSecond;

        OnNightStarted?.Invoke();
    }

    private void EndNight()
    {
        CurrentPhase = DayPhase.Day;
        SetTime(dayStartHour, 0);
        OnNightEnded?.Invoke();
    }

    public bool IsNight => CurrentPhase == DayPhase.Night;
}