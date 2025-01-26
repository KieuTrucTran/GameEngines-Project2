using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public static CameraScript Instance { get; private set; } // Singleton instance

    [Header("Target Settings")]
    public GameObject player; 

    [Header("Follow Settings")]
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    [Header("Boundaries")]
    public float minX = -10f;
    public float maxX = 10f;
    public float minY = -5f;
    public float maxY = 5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void LateUpdate()
    {

        if (player.tag != "Box") {
            Vector3 desiredPosition = player.transform.position + offset;

            desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
            desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            smoothedPosition.Set(smoothedPosition.x, smoothedPosition.y, -10);
            transform.position = smoothedPosition;
        }
        
    }
}
