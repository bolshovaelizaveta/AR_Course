using UnityEngine;
using UnityEngine.UI;

public class AutoContourTracker : MonoBehaviour
{
    private WebCamTexture webcamTexture;
    private Color32[] pixels;

    [Header("Поиск контура")]
    [Range(0, 1)] public float threshold = 0.95f; 
    public RectTransform visualMarker; 

    [Header("Связь с игроком")]
    public ClickToMove playerScript;
    public LayerMask groundLayer;

    void Start()
    {
        webcamTexture = new WebCamTexture();
        webcamTexture.Play();
    }

    void Update()
    {
        if (Time.timeScale == 0) return; 
        if (webcamTexture.width < 100) return;

        int w = webcamTexture.width;
        int h = webcamTexture.height;
        pixels = webcamTexture.GetPixels32();

        float sumX = 0, sumY = 0;
        int count = 0;

        for (int y = 0; y < h; y += 15) 
        {
            for (int x = 0; x < w; x += 15)
            {
                Color32 c = pixels[y * w + x];
                if ((c.r + c.g + c.b) / 3f / 255f > threshold)
                {
                    sumX += x; sumY += y; count++;
                }
            }
        }

        if (count > 2)
        {
            float avgX = sumX / count;
            float avgY = sumY / count;

            if (visualMarker)
            {
                visualMarker.gameObject.SetActive(true);
                Vector2 targetPos = new Vector2((avgX/w)*Screen.width - Screen.width/2, (avgY/h)*Screen.height - Screen.height/2);
                visualMarker.anchoredPosition = Vector2.Lerp(visualMarker.anchoredPosition, targetPos, 0.2f);
            }

            Ray ray = Camera.main.ScreenPointToRay(new Vector3((avgX/w)*Screen.width, (avgY/h)*Screen.height, 0));
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundLayer))
            {
                if (playerScript) playerScript.MoveToARPoint(hit.point);
            }
        }
        else 
        { 
            if (visualMarker) visualMarker.gameObject.SetActive(false); 
        }
    }
}