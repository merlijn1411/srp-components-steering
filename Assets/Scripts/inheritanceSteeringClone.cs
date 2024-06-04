using System.Collections;
using UnityEngine;

public class InheritanceSteeringClone : SteeringVehicle
{
    [SerializeField] private float radius;
    
    private void Start()
    {
        base.Start();
        StartCoroutine(HalfSecondCoroutine());
    }
    private void Update()
    {
        Radar();
    }
    
    IEnumerator HalfSecondCoroutine()
    {
        while (true)
        {
            int randomX, randomY;
            randomX = Random.Range(0, 10);
            randomY = Random.Range(0, 10);
            
            SetTarget(new Vector2(randomX, randomY)); // insert movement shit
            
            yield return new WaitForSeconds(0.5f);
        }
    }
    
    private void Radar()
    {
        if (Vector3.Distance(transform.position, targetTransform.position) < radius)
            base.Update();
        else
            StartCoroutine(HalfSecondCoroutine());
        
        
    }
    
    
    
}
