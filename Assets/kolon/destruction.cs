using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruction : MonoBehaviour
{
    public string partNamePrefix = "Cube_cell."; // Par�a isimlerinin ortak prefix'i
    public int partCount = 98; // Toplam par�a say�s�
    public float destructionDelayMin = 0.1f; // En az y�k�lma gecikmesi
    public float destructionDelayMax = 1.0f; // En fazla y�k�lma gecikmesi
    public float forceMagnitude = 10f; // Uygulanacak kuvvetin b�y�kl���

    private GameObject[] columnParts;
    public bool isQuaking = false;

    void Start()
    {
        // Par�alar� isimlerine g�re bul ve diziye ekle
        columnParts = new GameObject[partCount];
        for (int i = 1; i < partCount; i++)
        {
            string partName = partNamePrefix + i.ToString("D3"); //"Cube_cell.001"
            columnParts[i] = GameObject.Find(partName);
            if (columnParts[i] == null)
            {
                Debug.LogError("Kolon par�as� bulunamad�: " + partName);
            }
        }
    }

    void Update()
    {
        // Deprem olup olmad���n� kontrol et
        if (!isQuaking)
        {
            StartCoroutine(DestroyColumnParts());
            isQuaking = true;
        }
    }

    IEnumerator DestroyColumnParts()
    {
        
        int randomIndex = Random.Range(0, columnParts.Length);
        GameObject randomPart = columnParts[randomIndex];

        if (randomPart != null)
        {
            Rigidbody rb = randomPart.GetComponent<Rigidbody>();//rasgele par�a se�imi
            if (rb != null)
            {
                rb.isKinematic = false; // Kinematik �zelli�ini kapat
                
                
                Vector3 randomForce = new Vector3(
                    Random.Range(-forceMagnitude, forceMagnitude),
                    Random.Range(-forceMagnitude, forceMagnitude),
                    Random.Range(-forceMagnitude, forceMagnitude)
                );
                rb.AddForce(randomForce); // Rasgele kuvvet uygula
                rb.AddTorque(Random.insideUnitSphere * forceMagnitude); // Rasgele d�n�� uygula
            }
        }

        // Geriye kalan par�alar� belirli bir s�re sonra y�k�lmaya ba�lat�n

        

        foreach (GameObject part in columnParts)
        {
            if (part == null || part == randomPart) continue; // par�an�n olup olmad���n�n kontrol�

            Rigidbody rb = part.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Ba�lang��ta kinematik olarak kal�r
            }
            yield return new WaitForSeconds(Random.Range(destructionDelayMin, destructionDelayMax)); // max veya min bekleme s�resi kadar bekle
        }

        // Geriye kalan par�alar� da ayn� �ekilde kinematik �zelli�ini kapat
        foreach (GameObject part in columnParts)
        {
            if (part == null) continue;

            Rigidbody rb = part.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Kinematik �zelli�ini kapat
                Vector3 randomForce = new Vector3(
                    Random.Range(-forceMagnitude, forceMagnitude),
                    Random.Range(-forceMagnitude, forceMagnitude),
                    Random.Range(-forceMagnitude, forceMagnitude)
                );
                rb.AddForce(randomForce); // Rasgele kuvvet uygula
                rb.AddTorque(Random.insideUnitSphere * forceMagnitude); // Rasgele d�n�� uygula
            }
            yield return new WaitForSeconds(Random.Range(destructionDelayMin, destructionDelayMax)); // Rasgele bir s�re bekle
        }
    }
}
