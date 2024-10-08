using UnityEngine;

public class SlingshotAmmo : MonoBehaviour
{
    public float launchForce = 500f;
    public LineRenderer lineRenderer;
    private bool isDragging = false;
    private Vector3 dragStartPos;
    private Rigidbody rb;
    private Transform slingshotPoint; // Référence au point de slingshot

    public void Initialize(Transform point) // Méthode d'initialisation
    {
        slingshotPoint = point;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))  // Début du drag
        {
            StartDrag();
        }
        else if (Input.GetMouseButton(0) && isDragging)  // Pendant le drag
        {
            Dragging();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)  // Relâchement du projectile
        {
            EndDrag();
        }
    }

    void StartDrag()
    {
        isDragging = true;
        dragStartPos = GetMouseWorldPosition();
        lineRenderer.enabled = true;
    }

    void Dragging()
    {
        Vector3 currentPos = GetMouseWorldPosition();
        Vector3 direction = slingshotPoint.position - currentPos;
        lineRenderer.SetPosition(0, slingshotPoint.position);
        lineRenderer.SetPosition(1, currentPos);
    }

    void EndDrag()
    {
        isDragging = false;
        lineRenderer.enabled = false;

        Vector3 dragReleasePos = GetMouseWorldPosition();
        Vector3 launchDirection = (slingshotPoint.position - dragReleasePos).normalized;
        float dragDistance = Vector3.Distance(slingshotPoint.position, dragReleasePos);

        // Appliquer la force au projectile
        rb.AddForce(launchDirection * dragDistance * launchForce);
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }
}
