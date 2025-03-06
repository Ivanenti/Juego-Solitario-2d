using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // El transform del jugador
    public float smoothSpeed = 0.125f; // La velocidad con la que la c�mara se mueve
    public Vector3 offset; // La diferencia de posici�n entre la c�mara y el jugador
    public float forwardOffset = 1f; // La distancia extra en la direcci�n del jugador

    void FixedUpdate()
    {
        // Asegurarse de que el jugador est� asignado
        if (player == null)
            return;

        // Calcula la posici�n deseada: la posici�n del jugador + el offset
        Vector3 desiredPosition = player.position + offset;

        // Adelantar la c�mara en funci�n de la direcci�n del jugador
        // Esto mueve la c�mara un poco hacia adelante en el eje X o Z, dependiendo de la orientaci�n
        desiredPosition += player.forward * forwardOffset;

        // Interpolamos entre la posici�n actual de la c�mara y la deseada para un movimiento suave
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Movemos la c�mara a la nueva posici�n suavizada
        transform.position = smoothedPosition;
    }
}

