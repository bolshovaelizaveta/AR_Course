using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("ARCourse/Homework")]
public class HomeworkScript : MonoBehaviour
{
    [Header("Базовые типы")]
    [SerializeField] private int IntValue = 10;
    [SerializeField] private float FloatValue = 15.5f;
    [SerializeField] private double DoubleValue = 20.0;
    [SerializeField] private string StringValue = "Domashka";
    [SerializeField] private bool BoolValue = true;

    [Header("Коллекция")]
    [SerializeField] private int[] MyCollectionOfInts; 

    [Header("Ссылка на другой компонент")]
    [SerializeField] private Transform AnotherComponentReference; 

    [Header("Префаб для спавна")]
    [SerializeField] private GameObject PrefabToSpawn;

    private void Start()
    {
        Debug.Log($"Int: {IntValue}, String: {StringValue}");

        if (PrefabToSpawn != null)
        {
            // Instantiate создает clone префаба на сцене 
            Instantiate(PrefabToSpawn, transform.position, Quaternion.identity);
            Debug.Log("Клон - префаб создан");
        }
        else
        {
            Debug.LogError("Ошибка: надо назначить Prefab To Spawn в инспекторе");
        }
    } 
}