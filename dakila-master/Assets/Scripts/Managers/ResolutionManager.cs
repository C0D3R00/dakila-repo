using UnityEngine;

public class ResolutionManager : Singleton<ResolutionManager>
{
    protected ResolutionManager() { }

    public static float CameraWidth { get; private set; }
    public static float CameraFullWidth { get; private set; }
    public static float CameraHeight { get; private set; }
    public static float CameraFullHeight { get; private set; }

    [SerializeField]
    private CameraTypes
        CameraType;

    [SerializeField]
    private float
        DesiredSize = 0f;

    private void Start()
    {
        if (CameraType == CameraTypes.FIXED_HEIGHT)
        {
            Camera.main.orthographicSize = DesiredSize / 2f;

            CameraHeight = DesiredSize / 2f;
            CameraWidth = CameraHeight * Camera.main.aspect;
            CameraFullHeight = DesiredSize;
            CameraFullWidth = CameraWidth * 2f;
        }
        else
        {
            var width = DesiredSize / 2f;
            var height = width / Camera.main.aspect;

            CameraWidth = DesiredSize / 2f;
            CameraFullWidth = DesiredSize;
            CameraHeight = height;
            CameraFullHeight = height * 2f; // full height

            Camera.main.orthographicSize = height;
        }

        Debug.Log("height: " + CameraHeight + " width: " + CameraWidth + " full height: " + CameraFullHeight + " full width: " + CameraFullWidth);
    }
}
