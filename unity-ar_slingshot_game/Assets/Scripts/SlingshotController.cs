using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    public GameObject ammoPrefab; // Référence au prefab de l'Ammo
    public Transform slingshotPoint; // Référence au point de slingshot

    public void LaunchAmmo()
    {
        GameObject ammo = Instantiate(ammoPrefab, slingshotPoint.position, Quaternion.identity);
        ammo.GetComponent<SlingshotAmmo>().Initialize(slingshotPoint);
    }
}
