using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruction_2 : MonoBehaviour
{
    public string partNamePrefix = "Cube_cell."; // Par�a isimlerinin ortak prefix'i
    public int partCount = 98; // Toplam par�a say�s�
    public float delayBetweenChanges = 0.5f; // Kinematik �zelli�ini kapatma gecikmesi

    private GameObject[] columnParts;
    public bool isQuaking = false;
    void Update()
    {
        // Par�alar� isimlerine g�re bul ve diziye ekle
        columnParts = new GameObject[partCount];
        for (int i = 1; i < partCount; i++)
        {
            string partName = partNamePrefix + i.ToString("D3"); // �rne�in "Cube_cell.001"
            columnParts[i] = GameObject.Find(partName);
            if (columnParts[i] == null)
            {
                Debug.LogError("Kolon par�as� bulunamad�: " + partName);
            }
        }

        // Rastgele olarak kinematik �zelli�ini kapatmaya ba�la
        StartCoroutine(DisableKinematicRandomly());
    }

    IEnumerator DisableKinematicRandomly()
    {
        if (!isQuaking) {
            while (true)
            {
                int randomIndex = Random.Range(0, columnParts.Length);
                GameObject randomPart = columnParts[randomIndex];

                if (randomPart != null)
                {
                    Rigidbody rb = randomPart.GetComponent<Rigidbody>();
                    if (rb != null && rb.isKinematic)
                    {
                        rb.isKinematic = false; // Kinematik �zelli�ini kapat
                        Debug.Log("Kinematik kapat�ld�: " + randomPart.name);
                    }
                }

                yield return new WaitForSeconds(delayBetweenChanges); // Gecikme s�resi bekle
            }
        }
    }
}
