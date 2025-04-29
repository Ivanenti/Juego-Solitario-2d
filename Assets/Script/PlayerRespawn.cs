using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Transform respawnPoint; // Asignaremos el objeto "RespawnPoint"

    private Vector3 initialPosition;

    private void Start()
    {
        if (respawnPoint != null)
        {
            initialPosition = respawnPoint.position;
        }
        else
        {
            initialPosition = transform.position; // En caso de que no pongas nada
        }
    }

    public void Respawn()
    {
        transform.position = initialPosition;
        Debug.Log("¡Jugador respawneado!");
    }
}

