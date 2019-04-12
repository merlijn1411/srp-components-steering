using System;
using UnityEngine;
using System.Collections;

public class SteeringVehicle : MonoBehaviour
{
    [SerializeField] private float _maxSpeed = 20;

    /** hoe zwaarder het object, hoe slechter hij kan bijsturen */
    [SerializeField] private float _mass = 20;

    /** boolean of het object de richting op kijkt waar we naar toe bewegen */
    [SerializeField] private bool _followPath = false;

    [SerializeField] private float _arrivalDistance = 0.1f;

    private Vector2 _currentVelocity;
    private Vector2 _currentPosition;

    /** Vector om de locatie bij te houden waar we heen willen */
    private Vector2 _currentTarget;

    public Action Arrived;

    void Start()
    {
        _currentVelocity = new Vector2(0, 0);
        _currentPosition = transform.position;

        SetTarget(new Vector2(-4, 4));
    }

    // Elke frametick kijken we hoe we moeten sturen
    void Update()
    {
        Seek();
    }

    public void SetTarget(Vector2 target)
    {
        _currentTarget = target;
    }

    // van buitenaf kun je de huidige target uitlezen
    public Vector2 Target
    {
        get { return _currentTarget; }
    }

    void Seek()
    {
        // we berekenen eerst de afstand/Vector tot de 'target'		
        var desiredStep = _currentTarget - _currentPosition;

        // als een vector ge'normalized' is .. dan houdt hij dezelfde richting
        // maar zijn lengte/magnitude is 1
        desiredStep.Normalize();

        // vermenigvuldigen met de maximale snelheid dan
        // wordt de lengte van deze Vector maxSpeed (aangezien 1 x maxSpeed = maxSpeed)
        // de x en y van deze Vector wordt zo vanzelf omgerekend
        var desiredVelocity = desiredStep * _maxSpeed;

        // bereken wat de Vector moet zijn om bij te sturen om bij de desiredVelocity te komen
        var steeringForce = desiredVelocity - _currentVelocity;

        // uiteindelijk voegen we de steering force toe maar wel gedeeld door de 'mass'
        // hierdoor gaat hij niet in een rechte lijn naar de target
        // hoe zwaarder het object des te groter de bocht
        _currentVelocity += steeringForce / _mass;

        // Als laatste updaten we de positie door daar onze beweging (velocity) bij op te tellen
        _currentPosition += _currentVelocity * Time.deltaTime;
        transform.position = _currentPosition;

        if (_followPath)
        {
            float angle = Mathf.Atan2(_currentVelocity.y, _currentVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Vector2.Distance(_currentTarget, _currentPosition) < _arrivalDistance)
        {
            if (Arrived != null)
            {
                Arrived();
            }
        }
    }
}
