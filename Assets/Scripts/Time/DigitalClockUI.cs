using TMPro;
using UnityEngine;

/// <summary>
/// Tempel di GameObject yang punya TMP_Text.
/// Nampilin jam in-game dari TimeManager, format HH:mm.
/// </summary>
[RequireComponent(typeof(TMP_Text))]
public class DigitalClockUI : MonoBehaviour
{
    private TMP_Text label;

    private void Awake()
    {
        label = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        // Start() dijamin jalan setelah semua Awake() di scene selesai,
        // jadi TimeManager.Instance udah pasti ke-set di sini.
        TimeManager.Instance.OnTimeChanged += UpdateDisplay;
        UpdateDisplay(TimeManager.Instance.CurrentHour, TimeManager.Instance.CurrentMinute);
    }

    private void OnDisable()
    {
        if (TimeManager.Instance != null)
        {
            TimeManager.Instance.OnTimeChanged -= UpdateDisplay;
        }
    }

    private void UpdateDisplay(int hour, int minute)
    {
        label.text = $"{hour:00}:{minute:00}";
    }
}