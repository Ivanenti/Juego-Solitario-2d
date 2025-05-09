using UnityEngine;
using TMPro; // Importante para usar TextMeshProUGUI

public class DoorController : MonoBehaviour
{
    public Collider2D door;                     // Collider que bloquea el paso
    public TextMeshProUGUI mensajeTexto;        // Texto TMP para mostrar el mensaje

    private void Start()
    {
        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false); // Oculta el texto al iniciar
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            if (inventory != null && inventory.hasKey)
            {
                Debug.Log("¡Puerta abierta!");
                door.enabled = false; // Desactiva la puerta
                MostrarMensaje("¡¡¡LO HAS COMPLETADO!!!");
            }
            else
            {
                Debug.Log("La puerta está cerrada. Necesitas una llave.");
            }
        }
    }

    private void MostrarMensaje(string mensaje)
    {
        if (mensajeTexto == null) return;

        mensajeTexto.text = mensaje;
        mensajeTexto.gameObject.SetActive(true);
        Invoke("OcultarMensaje", 2f); // Oculta el mensaje después de 2 segundos
    }

    private void OcultarMensaje()
    {
        if (mensajeTexto != null)
            mensajeTexto.gameObject.SetActive(false);
    }
}
