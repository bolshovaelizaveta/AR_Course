using UnityEngine;
using DG.Tweening; 

public class DotWeenMovementExample : MonoBehaviour
{
    public Transform target; 
    public float speed = 2f; 
    public float stopDistance = 1.5f; 
    void Update()
    {
        if (target == null) return;

        transform.LookAt(target);

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > stopDistance)
        {
            transform.DOMove(target.position, speed).SetSpeedBased().SetEase(Ease.Linear);
        }
    }
    
    void OnDisable()
    {
        transform.DOKill();
    }
}