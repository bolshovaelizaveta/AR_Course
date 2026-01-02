using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectFindSample : MonoBehaviour
{

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Vector3 _movementDirection;
    [SerializeField] private Vector3 _rotationDirection;


    void Start()
    {

    }

    void Update()
    {

        var tr = transform;

        tr.Translate(_movementDirection * _movementSpeed * Time.deltaTime); 
    
        tr.Rotate(_rotationDirection * _rotationSpeed * Time.deltaTime);
    }
}