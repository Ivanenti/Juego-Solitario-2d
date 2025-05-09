using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    // Corazones en el Canvas
    public GameObject[] heartIcons;

    // Parpadeo al recibir daño
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;
    public float blinkDuration = 0.2f;
    public int blinkCount = 5;
    public float invincibilityDuration = 1f;

    void Start()
    {
        currentLives = maxLives;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("El objeto no tiene un SpriteRenderer.");
        }

        UpdateUI();
    }

    public void TakeDamage()
    {
        if (spriteRenderer == null || isBlinking || currentLives <= 0) return;

        currentLives--;
        UpdateUI();

        if (!isBlinking)
        {
            StartCoroutine(BlinkEffect());
        }

        if (currentLives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < heartIcons.Length; i++)
        {
            heartIcons[i].SetActive(i < currentLives);
        }
    }

    private IEnumerator BlinkEffect()
    {
        isBlinking = true;
        int blink = 0;

        while (blink < blinkCount)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkDuration);
            blink++;
        }

        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(invincibilityDuration);
        isBlinking = false;
    }
}
