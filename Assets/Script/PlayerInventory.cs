using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasKey = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            hasKey = true;
            Destroy(collision.gameObject); // Destruye la llave del mundo
        }
    }
}