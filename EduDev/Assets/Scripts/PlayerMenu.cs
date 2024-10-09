using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMenu : MonoBehaviour
{
    public Transform[] points; // Array titik tujuan
    public float speed = 2f; // Kecepatan gerak karakter
    private int currentPointIndex = 0; // Indeks titik tujuan saat ini

    private void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        // Cek jika titik tujuan ada
        if (points.Length > 0)
        {
            // Hitung arah menuju titik tujuan saat ini
            Vector3 direction = (points[currentPointIndex].position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            // Cek apakah karakter sudah mencapai titik tujuan
            if (Vector3.Distance(transform.position, points[currentPointIndex].position) < 0.1f)
            {
                // Ubah indeks ke titik tujuan berikutnya
                currentPointIndex++;

                // Jika sudah mencapai titik terakhir, ulangi dari awal
                if (currentPointIndex >= points.Length)
                {
                    currentPointIndex = 0;
                }
            }

            if (direction.x > 0)
            {
                // Jika bergerak ke kanan, hadap kanan
                transform.localScale = new Vector3(1, 1, 1); // Mengatur skala karakter agar menghadap kanan
            }
            else if (direction.x < 0)
            {
                // Jika bergerak ke kiri, hadap kiri
                transform.localScale = new Vector3(-1, 1, 1); // Mengatur skala karakter agar menghadap kiri
            }
        }
    }
}
