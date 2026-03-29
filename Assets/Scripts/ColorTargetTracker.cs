using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ColorTargetTracker : MonoBehaviour
{
    [Header("Настройки CV")]
    public Color32 targetColor = new Color32(255, 0, 0, 255); // Ищем красный предмет
    public float threshold = 70f; // Чувствительность
    public RectTransform debugMarker; // Рамка на экране

    [Header("Кому бежать")]
    public NavMeshAgent playerAgent;

    private WebCamTexture webcamTexture;
    private Color32[] pixels;

    void Start()
    {
        // Берем текстуру от Vuforia 
        webcamTexture = new WebCamTexture();
        webcamTexture.Play();
    }

    void Update()
    {
        if (webcamTexture.width < 100) return;

        DetectColorContour();
    }

    void DetectColorContour()
    {
        pixels = webcamTexture.GetPixels32();
        int w = webcamTexture.width;
        int h = webcamTexture.height;

        int sumX = 0, sumY = 0, count = 0;

        // Поиск пикселей нужного цвета
        for (int y = 0; y < h; y += 5) 
        {
            for (int x = 0; x < w; x += 5)
            {
                Color32 c = pixels[y * w + x];
                float diff = Mathf.Abs(c.r - targetColor.r) + Mathf.Abs(c.g - targetColor.g) + Mathf.Abs(c.b - targetColor.b);

                if (diff < threshold)
                {
                    sumX += x;
                    sumY += y;
                    count++;
                }
            }
        }

        if (count > 10) // Если нашли достаточно пикселей (контур объекта)
        {
            Vector2 screenPos = new Vector2(sumX / count, sumY / count);

            // Отображаем рамку 
            if (debugMarker)
            {
                debugMarker.gameObject.SetActive(true);
                debugMarker.anchoredPosition = new Vector2(screenPos.x - w/2, screenPos.y - h/2);
            }

            // Двигаем игрока 
            MovePlayerToPoint(screenPos, w, h);
        }
        else
        {
            if (debugMarker) debugMarker.gameObject.SetActive(false);
        }
    }

    void MovePlayerToPoint(Vector2 pos, int w, int h)
    {
        // Переводим координаты камеры в координаты острова
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(pos.x * Screen.width / w, pos.y * Screen.height / h, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (playerAgent != null)
            {
                playerAgent.SetDestination(hit.point);
            }
        }
    }
}