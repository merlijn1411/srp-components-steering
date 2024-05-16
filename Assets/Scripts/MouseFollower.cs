using UnityEngine;

public class MouseFollower : MonoBehaviour
{
    // Dit script wordt nu niet gebruikt in deze repository
    // maar hier gaan jullie later wel mee aan de slag

    void Update()
    {
        // Als je met de muis klikt: geef dan aan SteeringVehicle door
        // via SetTarget() waar het vliegtuig naartoe moet
        var targetPosition = GetMousePosition();
        transform.position = targetPosition;
    }

    void OnMouseDown()
    {
        var targetPosition = GetMousePosition();
        Debug.Log(targetPosition);
    }

    Vector2 GetMousePosition()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}