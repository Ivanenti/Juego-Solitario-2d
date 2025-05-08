using UnityEngine;
using UnityEngine.SceneManagement; // Para reiniciar o cargar Game Over
using System.Collections;  // Añadir esta línea para poder usar IEnumerator

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;


    // Variables para el parpadeo
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;
    public float blinkDuration = 0.2f;  // Duración de cada parpadeo
    public int blinkCount = 5;  // Número de parpadeos
    public float invincibilityDuration = 1f;  // Tiempo de invulnerabilidad tras recibir daño

    void Start()
    {
        currentLives = maxLives;
        spriteRenderer = GetComponent<SpriteRenderer>(); // Obtiene el SpriteRenderer

        // Verifica si el SpriteRenderer está presente
        if (spriteRenderer == null)
        {
            Debug.LogError("El objeto no tiene un SpriteRenderer. Asegúrate de agregar uno.");
        }

        UpdateUI();
    }

    public void TakeDamage()
    {
        if (spriteRenderer == null) return;  // Si no hay SpriteRenderer, no se ejecuta el parpadeo

        // Resta vidas y actualiza la UI
        currentLives--;
        UpdateUI();

        // Inicia el parpadeo al recibir daño
        if (!isBlinking)
        {
            StartCoroutine(BlinkEffect());  // Inicia la corutina de parpadeo
        }

        // Si no quedan vidas, reinicia la escena
        if (currentLives <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void UpdateUI()
    {
        Debug.Log("Vidas restantes: " + currentLives);
        // Aquí puedes actualizar un sistema visual (corazones, texto, etc.)
    }

    // Coroutine para el parpadeo
    private IEnumerator BlinkEffect()
    {
        isBlinking = true;  // Establece que el parpadeo está activo
        int blink = 0;

        // El parpadeo se repite 'blinkCount' veces
        while (blink < blinkCount)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;  // Cambia la visibilidad del sprite
            yield return new WaitForSeconds(blinkDuration);  // Espera el tiempo de cada parpadeo
            blink++;
        }

        // Asegura que el sprite es visible después del parpadeo
        spriteRenderer.enabled = true;

        // Espera la duración de la invulnerabilidad antes de permitir daño nuevamente
        yield return new WaitForSeconds(invincibilityDuration);

        isBlinking = false;  // Termina el parpadeo
    }
}