using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Play SFX saat button diklik. Attach script ini LANGSUNG ke tiap GameObject Button
/// (ButtonPlay, ButtonOptions, ButtonQuit, ButtonBack).
/// </summary>
[RequireComponent(typeof(Button))]
public class ButtonClickSFX : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioSource audioSource; // AudioSource khusus buat SFX (lihat cara setup di bawah)

    private void Start()
    {
        // Auto-hook ke event klik button ini
        GetComponent<Button>().onClick.AddListener(PlayClickSound);
    }

    private void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}