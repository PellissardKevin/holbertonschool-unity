using UnityEngine;
using UnityEngine.Android;

public class CameraPermissionRequester : MonoBehaviour
{
    void Start()
    {
        // Check if the app already has camera permission
        if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
        {
            // Request camera permission if not already granted
            Permission.RequestUserPermission(Permission.Camera);
        }
    }
}
