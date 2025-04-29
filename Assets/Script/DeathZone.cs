using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerRespawn playerRespawn = collision.GetComponent<PlayerRespawn>();

        if (playerRespawn != null)
        {
            Debug.Log("Jugador tocó la zona de muerte");
            playerRespawn.Respawn();
        }
    }
}