using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Collider2D door; // Este ser� el collider que bloquea el paso

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null && inventory.hasKey)
            {
                Debug.Log("�Puerta abierta!");
                door.enabled = false; // Desactiva la colisi�n
            }
            else
            {
                Debug.Log("La puerta est� cerrada. Necesitas una llave.");
            }
        }
    }
}
