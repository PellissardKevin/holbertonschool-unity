using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform teleportDestination;
    public Teleporter pairedTeleporter; // Reference to the paired teleporter
    public float cooldownTime = 1.0f; // Time in seconds before teleportation can occur again

    private bool canTeleport = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canTeleport)
        {
            // Teleport the player
            other.transform.position = teleportDestination.position;
            other.transform.rotation = teleportDestination.rotation;

            // Start cooldown to prevent immediate re-teleport
            StartCoroutine(TeleportCooldown());
            pairedTeleporter.StartCoroutine(pairedTeleporter.TeleportCooldown());
        }
    }

    private System.Collections.IEnumerator TeleportCooldown()
    {
        canTeleport = false;
        yield return new WaitForSeconds(cooldownTime);
        canTeleport = true;
    }
}
