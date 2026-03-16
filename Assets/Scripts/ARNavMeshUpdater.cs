using UnityEngine;
using Unity.AI.Navigation;
using Vuforia;
using System.Collections;

public class ARNavMeshUpdater : MonoBehaviour
{
    public NavMeshSurface navMeshSurface;
    private ObserverBehaviour mObserverBehaviour;
    private bool isNavMeshBuilt = false;

    void Start()
    {
        mObserverBehaviour = GetComponent<ObserverBehaviour>();
        if (mObserverBehaviour)
        {
            mObserverBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            if (!isNavMeshBuilt)
            {
                StartCoroutine(DelayedNavMeshBuild());
            }
        }
        else
        {
            isNavMeshBuilt = false;
        }
    }

    IEnumerator DelayedNavMeshBuild()
    {
        yield return new WaitForSeconds(0.5f);
        
        if (navMeshSurface != null)
        {
            navMeshSurface.BuildNavMesh();
            isNavMeshBuilt = true;
            Debug.Log("AR NavMesh built successfully!");
        }
    }
}