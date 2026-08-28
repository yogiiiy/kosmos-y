using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class TavernNPC : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private float displayDuration = 3f;
    [SerializeField] private float fadeDuration = 1f;

    private bool playerInRange = false;
    private PlayerControls controls;
    private Coroutine fadeCoroutine;

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Player.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        controls.Player.Interact.performed -= OnInteract;
        controls.Player.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        if (!playerInRange) return;

        if (TimeManager.Instance.IsNight)
        {
            ShowFeedback("Masih malam, tunggu sampai pagi.");
            return;
        }

        TimeManager.Instance.StartNight();
        ShowFeedback("Malam dimulai...");
    }

    private void ShowFeedback(string message)
    {
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        fadeCoroutine = StartCoroutine(ShowThenFade(message));
    }

    private IEnumerator ShowThenFade(string message)
    {
        feedbackText.text = message;
        Color c = feedbackText.color;
        c.a = 1f;
        feedbackText.color = c;

        yield return new WaitForSeconds(displayDuration);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            feedbackText.color = c;
            yield return null;
        }

        feedbackText.text = "";
    }
}