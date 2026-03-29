using UnityEngine;
using Unity.AI.Navigation;
using Vuforia;
using System.Collections;

public class ARNavMeshUpdater : MonoBehaviour
{
    [Header("Настройки AR")]
    public NavMeshSurface navMeshSurface;
    public GameObject arenaObject; 

    private ObserverBehaviour mObserverBehaviour;
    private bool isNavMeshBuilt = false;

    void Start()
    {
        mObserverBehaviour = GetComponent<ObserverBehaviour>();
        if (mObserverBehaviour)
        {
            mObserverBehaviour.OnTargetStatusChanged += OnTargetStatusChanged;
        }
            
        if (arenaObject != null) 
            arenaObject.SetActive(false);
    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus targetStatus)
    {
        if (targetStatus.Status == Status.TRACKED || targetStatus.Status == Status.EXTENDED_TRACKED)
        {
            if (arenaObject != null && !arenaObject.activeSelf) 
            {
                arenaObject.SetActive(true);
            }

            if (!isNavMeshBuilt) 
            {
                StartCoroutine(SafeNavMeshBuild());
            }
        }
        else
        {
        }
    }

    IEnumerator SafeNavMeshBuild()
    {
        yield return new WaitForSeconds(0.5f);

        if (navMeshSurface != null)
        {
            try 
            {
                navMeshSurface.BuildNavMesh();
                isNavMeshBuilt = true;
                Debug.Log("AR: NavMesh успешно запечен на маркере");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Ошибка запекания NavMesh: " + e.Message);
            }
        }
    }
}