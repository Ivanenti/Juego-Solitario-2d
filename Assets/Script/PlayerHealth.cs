using UnityEngine;
using UnityEngine.SceneManagement; // Para reiniciar o cargar Game Over

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;

    void Start()
    {
        currentLives = maxLives;
        UpdateUI();
    }

    public void TakeDamage()
    {
        currentLives--;
        UpdateUI();

        if (currentLives <= 0)
        {
            // Reinicia la escena actual (puedes cambiar esto por cargar Game Over)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void UpdateUI()
    {
        Debug.Log("Vidas restantes: " + currentLives);
        // Aquí puedes actualizar un sistema visual (corazones, texto, etc)
    }
}
