using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class TavernNPC : MonoBehaviour
{
    [SerializeField] private ItemData slimeJellyData;
    [SerializeField] private int requiredAmount = 5;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private TextMeshProUGUI winText;
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

        int currentAmount = Inventory.Instance.GetQuantity(slimeJellyData);

        if (currentAmount >= requiredAmount)
        {
            winText.text = "Congrats!";
        }
        else
        {
            ShowFeedback("Defeat slimes and collect 5 slime jellies");
        }
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