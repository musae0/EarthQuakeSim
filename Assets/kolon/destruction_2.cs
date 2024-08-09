using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruction_2 : MonoBehaviour
{
    public string partNamePrefix = "Cube_cell."; // Parça isimlerinin ortak prefix'i
    public int partCount = 98; // Toplam parça sayýsý
    public float delayBetweenChanges = 0.5f; // Kinematik özelliðini kapatma gecikmesi

    private GameObject[] columnParts;
    public bool isQuaking = false;
    void Update()
    {
        // Parçalarý isimlerine göre bul ve diziye ekle
        columnParts = new GameObject[partCount];
        for (int i = 1; i < partCount; i++)
        {
            string partName = partNamePrefix + i.ToString("D3"); // Örneðin "Cube_cell.001"
            columnParts[i] = GameObject.Find(partName);
            if (columnParts[i] == null)
            {
                Debug.LogError("Kolon parçasý bulunamadý: " + partName);
            }
        }

        // Rastgele olarak kinematik özelliðini kapatmaya baþla
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
                        rb.isKinematic = false; // Kinematik özelliðini kapat
                        Debug.Log("Kinematik kapatýldý: " + randomPart.name);
                    }
                }

                yield return new WaitForSeconds(delayBetweenChanges); // Gecikme süresi bekle
            }
        }
    }
}
