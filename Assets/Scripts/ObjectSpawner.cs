using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("ARCourse/ObjectSpawner")]

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _gameObjectToSpawn; 
    // Start is called before the first frame update
    void Start()
    {
        if (_gameObjectToSpawn != null)
        {
            Instantiate(_gameObjectToSpawn);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
