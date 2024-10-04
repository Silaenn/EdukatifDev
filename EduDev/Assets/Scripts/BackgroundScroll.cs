using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] private GameObject[] backgrounds;
    [SerializeField] private float backgroundSpeed = 5f;

    private Vector3[] startPositions;
    private float[] backgroundWidths;

    private void Start()
    {
        int backgroundCount = backgrounds.Length;
        startPositions = new Vector3[backgroundCount];
        backgroundWidths = new float[backgroundCount];

        for (int i = 0; i < backgroundCount; i++)
        {
            startPositions[i] = backgrounds[i].transform.position;
            
            // Mengambil width dari sprite renderer
            SpriteRenderer spriteRenderer = backgrounds[i].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                backgroundWidths[i] = spriteRenderer.bounds.size.x;
            }
            else
            {
                Debug.LogWarning($"SpriteRenderer tidak ditemukan pada background {i}. Menggunakan default width 1.");
                backgroundWidths[i] = 1f;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.Translate(Vector3.left * backgroundSpeed * Time.deltaTime);

            if (backgrounds[i].transform.position.x <= -backgroundWidths[i])
            {
                RepositionBackground(i);

            }
        }
    }

    private void RepositionBackground(int index)
    {
        int nextIndex = (index + 1) % backgrounds.Length;
        float newX = backgrounds[nextIndex].transform.position.x + backgroundWidths[nextIndex];
        backgrounds[index].transform.position = new Vector3(newX, startPositions[index].y, startPositions[index].z);
    }
}
