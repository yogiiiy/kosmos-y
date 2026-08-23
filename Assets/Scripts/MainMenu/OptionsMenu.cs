using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Handle slider volume di Options panel.
/// Attach ke GameObject "OptionsPanel", assign AudioMixer & Slider di Inspector.
///
/// PENTING: AudioMixer perlu punya 1 parameter yang di-"Expose" bernama "MasterVolume".
/// Caranya ada di SETUP_GUIDE.md.
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    private const string MIXER_PARAM = "MasterVolume";
    private const string PREF_KEY = "MasterVolume";

    private void Start()
    {
        // Load volume tersimpan sebelumnya (default 0.75 kalau belum pernah diatur)
        float savedVolume = PlayerPrefs.GetFloat(PREF_KEY, 0.75f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);

        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    // Dipanggil tiap slider digeser
    public void SetVolume(float sliderValue)
    {
        // Slider pakai skala linear 0.0001-1, AudioMixer pakai desibel (dB).
        // Konversi log10 ini standar buat volume yang kerasa natural di telinga.
        float dB = Mathf.Log10(Mathf.Max(sliderValue, 0.0001f)) * 20f;
        audioMixer.SetFloat(MIXER_PARAM, dB);

        PlayerPrefs.SetFloat(PREF_KEY, sliderValue);
    }

    private void OnDestroy()
    {
        volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }
}