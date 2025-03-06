using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // El transform del jugador
    public float smoothSpeed = 0.125f; // La velocidad con la que la cámara se mueve
    public Vector3 offset; // La diferencia de posición entre la cámara y el jugador
    public float forwardOffset = 1f; // La distancia extra en la dirección del jugador

    void FixedUpdate()
    {
        // Asegurarse de que el jugador está asignado
        if (player == null)
            return;

        // Calcula la posición deseada: la posición del jugador + el offset
        Vector3 desiredPosition = player.position + offset;

        // Adelantar la cámara en función de la dirección del jugador
        // Esto mueve la cámara un poco hacia adelante en el eje X o Z, dependiendo de la orientación
        desiredPosition += player.forward * forwardOffset;

        // Interpolamos entre la posición actual de la cámara y la deseada para un movimiento suave
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Movemos la cámara a la nueva posición suavizada
        transform.position = smoothedPosition;
    }
}

