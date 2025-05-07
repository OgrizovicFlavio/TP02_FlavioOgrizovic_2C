using UnityEngine;

public static class CameraManager
{
    public static Camera CurrentCamera { get; private set; }

    public static void SetCamera(Camera cam)
    {
        CurrentCamera = cam;
    }
}
