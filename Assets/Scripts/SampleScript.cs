using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("ARCourse/SampleScript")]

public class SampleScript : MonoBehaviour
{
    [SerializeField] private int[] ArrayOfInts;
    [SerializeField] private int IntValue = 10;
    [SerializeField] private float FloatValue = 15;
    [SerializeField] private double DoubleValue = 20;
    [SerializeField] private string StringValue = "Hello, World!";
    [SerializeField] private bool BoolValue = true;
    [Header("Color for material")]
    [TooltipAttribute("This color will be applied on choosen material")]
    [SerializeField] private Color ColorMaterial = Color.yellow;
    [Space(10)]
    [SerializeField] private SampleScript SampleComponent; 
    [SerializeField][Range(1.0f, 10.0f)] private float _privateFloatValue;

    private void Start()
    {
        Debug.Log($"@ArrayOfInts {ArrayOfInts}");
        Debug.Log($"@IntValue {IntValue}");
        Debug.Log($"@FloatValue {FloatValue}");
        Debug.Log($"@DoubleValue {DoubleValue}");
        Debug.Log($"@StringValue {StringValue}");
        Debug.Log($"@BoolValue {BoolValue}");
        Debug.Log($"@Color {ColorMaterial}");
    }

    private void Update()
    {
        Debug.Log($"@ArrayOfInts {ArrayOfInts}");
        Debug.Log($"IntValue {IntValue}");
        Debug.Log($"FloatValue {FloatValue}");
        Debug.Log($"DoubleValue {DoubleValue}");
        Debug.Log($"StringValue {StringValue}");
        Debug.Log($"BoolValue {BoolValue}");
        Debug.Log($"@Color {ColorMaterial}");
    }
}