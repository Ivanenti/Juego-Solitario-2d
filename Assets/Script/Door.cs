using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Collider2D door; // Este será el collider que bloquea el paso

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null && inventory.hasKey)
            {
                Debug.Log("¡Puerta abierta!");
                door.enabled = false; // Desactiva la colisión
            }
            else
            {
                Debug.Log("La puerta está cerrada. Necesitas una llave.");
            }
        }
    }
}
