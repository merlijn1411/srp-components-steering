using UnityEngine;
using System.Collections;

public class MouseFollower : MonoBehaviour {

    
    void Update () {
        // Als je met de muis klikt: geef dan aan SteeringVehicle door
        // via SetTarget() waar het vliegtuig naartoe moet
    }

    Vector2 GetMousePosition()
    {
        var mousePosition = Input.mousePosition;
        mousePosition.z = 10;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}