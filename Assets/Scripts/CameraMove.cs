using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float smoothing = 0.2f;

    private Vector2 minCameraBoundary;
    private Vector2 maxCameraBoundary;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void FixedUpdate()
    {
        if (player == null) return;

        Vector3 targetPos = new Vector3(player.position.x, player.position.y, transform.position.z);

        targetPos.x = Mathf.Clamp(targetPos.x, minCameraBoundary.x, maxCameraBoundary.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minCameraBoundary.y, maxCameraBoundary.y);

        transform.position = Vector3.Lerp(transform.position, targetPos, smoothing);
    }

    public void SetCameraBounds(Vector2 min, Vector2 max)
    {
        minCameraBoundary = min;
        maxCameraBoundary = max;
    }

    public void SetTarget(Transform newTarget)
    {
        player = newTarget;
    }
}