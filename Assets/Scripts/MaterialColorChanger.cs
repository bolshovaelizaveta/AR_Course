using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[AddComponentMenu("ARCourse/MaterialColorChanger")]

[RequireComponent(typeof(MeshRenderer))]
public class MaterialColorChanger : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Color _color; 

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _meshRenderer.material.color = _color; 
    }
}
