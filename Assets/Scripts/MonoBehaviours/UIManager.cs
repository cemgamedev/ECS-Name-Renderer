using UnityEngine;
using TMPro;
using Unity.Entities;
using CubeSwarm.Components;

namespace CubeSwarm.MonoBehaviours
{
    public class UIManager : MonoBehaviour
    {
        public TextMeshProUGUI statsText;
        private float updateInterval = 0.5f; // Güncelleme aralığı
        private float accum = 0.0f;
        private int frames = 0;
        private float timeleft;
        private float fps = 0.0f;
        private EntityQuery cubeQuery;

        void Start()
        {
            if (statsText == null)
            {
                Debug.LogError("Stats Text is not assigned to UIManager!");
            }

            timeleft = updateInterval;
            
            // Küp entity'lerini saymak için query oluştur
            var world = World.DefaultGameObjectInjectionWorld;
            if (world != null)
            {
                cubeQuery = world.EntityManager.CreateEntityQuery(typeof(CubeComponent));
            }
        }

        void Update()
        {
            timeleft -= Time.deltaTime;
            accum += Time.timeScale / Time.deltaTime;
            frames++;

            if (timeleft <= 0.0)
            {
                fps = accum / frames;
                timeleft = updateInterval;
                accum = 0.0f;
                frames = 0;

                // Küp sayısını al
                int cubeCount = cubeQuery.IsEmpty ? 0 : cubeQuery.CalculateEntityCount();

                // UI'ı güncelle
                if (statsText != null)
                {
                    statsText.text = string.Format("FPS: {0:F2}\nCube Count: {1}", fps, cubeCount);
                }
            }
        }
    }
} 