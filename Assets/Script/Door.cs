using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el objeto que entra es el jugador
        PlayerInventory playerInventory = collision.GetComponent<PlayerInventory>();

        if (playerInventory != null && playerInventory.hasKey)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        // Aquí puedes poner la lógica que quieras al abrir la puerta
        Debug.Log("¡Puerta abierta!");
        Destroy(gameObject); // Destruye la puerta (opcional)
    }
}
