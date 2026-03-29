using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class PointCloudGenerator : MonoBehaviour
{
    public int pointsCount = 1000; 
    public float cloudSize = 0.5f;   // Размер самого облака брызг
    public float pointSize = 0.1f;   // Размер одной точки 

    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        GeneratePointCloud();
    }

    void GeneratePointCloud()
    {
        var main = ps.main;
        main.maxParticles = pointsCount;
        main.simulationSpace = ParticleSystemSimulationSpace.Local; 

        particles = new ParticleSystem.Particle[pointsCount];

        for (int i = 0; i < pointsCount; i++)
        {
            // Генерируем точки в форме фонтанчика
            Vector3 pos = Random.insideUnitSphere * cloudSize;
            if (pos.y < 0) pos.y = -pos.y; 

            particles[i].position = pos;
            particles[i].startColor = Color.cyan;  // Голубой
            particles[i].startSize = pointSize; 
            particles[i].startLifetime = 1000f;
            particles[i].remainingLifetime = 1000f;
            
            //  Скорость брызгам, чтобы они двигались
            particles[i].velocity = Random.insideUnitSphere * 0.1f;
        }

        ps.SetParticles(particles, pointsCount);
    }

    void Update()
    {
        // Чтобы создать эффект брызгов воды
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].position += Random.insideUnitSphere * 0.01f;
        }
        ps.SetParticles(particles, pointsCount);
    }
}