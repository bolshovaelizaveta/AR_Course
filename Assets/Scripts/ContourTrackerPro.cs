using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ContourTrackerPro : MonoBehaviour, IPointerClickHandler
{
    public RawImage cameraDisplay;
    public NavMeshAgent playerAgent;
    public RectTransform contourFrame; 
    public LayerMask groundLayer;

    private WebCamTexture webcamTexture;
    private Color32 targetColor;
    private bool colorSelected = false;
    private float sensitivity = 40f;

    void Start()
    {
        // Запуск камеры
        webcamTexture = new WebCamTexture();
        cameraDisplay.texture = webcamTexture;
        webcamTexture.Play();
        
        if (contourFrame) contourFrame.gameObject.SetActive(false);
    }

    // Метод для выбора цвета кликом 
    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(cameraDisplay.rectTransform, eventData.position, null, out localPoint);
        
        float xFactor = (localPoint.x + cameraDisplay.rectTransform.rect.width / 2) / cameraDisplay.rectTransform.rect.width;
        float yFactor = (localPoint.y + cameraDisplay.rectTransform.rect.height / 2) / cameraDisplay.rectTransform.rect.height;

        int texX = (int)(xFactor * webcamTexture.width);
        int texY = (int)(yFactor * webcamTexture.height);

        targetColor = webcamTexture.GetPixels32()[texY * webcamTexture.width + texX];
        colorSelected = true;
        Debug.Log("Цвет выбран: " + targetColor);
    }

    void Update()
    {
        if (!colorSelected || webcamTexture.width < 100) return;

        FindContour();
    }

    void FindContour()
    {
        Color32[] pixels = webcamTexture.GetPixels32();
        int w = webcamTexture.width;
        int h = webcamTexture.height;

        int minX = w, maxX = 0, minY = h, maxY = 0;
        bool found = false;

        for (int y = 0; y < h; y += 8) 
        {
            for (int x = 0; x < w; x += 8)
            {
                Color32 c = pixels[y * w + x];
                float diff = Mathf.Sqrt(Mathf.Pow(c.r - targetColor.r, 2) + Mathf.Pow(c.g - targetColor.g, 2) + Mathf.Pow(c.b - targetColor.b, 2));

                if (diff < sensitivity)
                {
                    if (x < minX) minX = x;
                    if (x > maxX) maxX = x;
                    if (y < minY) minY = y;
                    if (y > maxY) maxY = y;
                    found = true;
                }
            }
        }

        if (found)
        {
            // Рисуем контур 
            contourFrame.gameObject.SetActive(true);
            float screenW = cameraDisplay.rectTransform.rect.width;
            float screenH = cameraDisplay.rectTransform.rect.height;

            float centerX = (minX + maxX) / 2f;
            float centerY = (minY + maxY) / 2f;

            contourFrame.anchoredPosition = new Vector2(
                (centerX / w * screenW) - screenW / 2,
                (centerY / h * screenH) - screenH / 2
            );
            contourFrame.sizeDelta = new Vector2((maxX - minX) * screenW / w, (maxY - minY) * screenH / h);

            // Движение к объекту 
            MoveToContour(new Vector2(centerX / w * Screen.width, centerY / h * Screen.height));
        }
    }

    void MoveToContour(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, groundLayer))
        {
            if (playerAgent && playerAgent.isOnNavMesh)
            {
                playerAgent.SetDestination(hit.point);
            }
        }
    }
}