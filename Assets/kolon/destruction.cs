using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruction : MonoBehaviour
{
    public string partNamePrefix = "Cube_cell."; // Parça isimlerinin ortak prefix'i
    public int partCount = 98; // Toplam parça sayýsý
    public float destructionDelayMin = 0.1f; // En az yýkýlma gecikmesi
    public float destructionDelayMax = 1.0f; // En fazla yýkýlma gecikmesi
    public float forceMagnitude = 10f; // Uygulanacak kuvvetin büyüklüðü

    private GameObject[] columnParts;
    public bool isQuaking = false;

    void Start()
    {
        // Parçalarý isimlerine göre bul ve diziye ekle
        columnParts = new GameObject[partCount];
        for (int i = 1; i < partCount; i++)
        {
            string partName = partNamePrefix + i.ToString("D3"); //"Cube_cell.001"
            columnParts[i] = GameObject.Find(partName);
            if (columnParts[i] == null)
            {
                Debug.LogError("Kolon parçasý bulunamadý: " + partName);
            }
        }
    }

    void Update()
    {
        // Deprem olup olmadýðýný kontrol et
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
            Rigidbody rb = randomPart.GetComponent<Rigidbody>();//rasgele parça seçimi
            if (rb != null)
            {
                rb.isKinematic = false; // Kinematik özelliðini kapat
                
                
                Vector3 randomForce = new Vector3(
                    Random.Range(-forceMagnitude, forceMagnitude),
                    Random.Range(-forceMagnitude, forceMagnitude),
                    Random.Range(-forceMagnitude, forceMagnitude)
                );
                rb.AddForce(randomForce); // Rasgele kuvvet uygula
                rb.AddTorque(Random.insideUnitSphere * forceMagnitude); // Rasgele dönüþ uygula
            }
        }

        // Geriye kalan parçalarý belirli bir süre sonra yýkýlmaya baþlatýn

        

        foreach (GameObject part in columnParts)
        {
            if (part == null || part == randomPart) continue; // parçanýn olup olmadýðýnýn kontrolü

            Rigidbody rb = part.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Baþlangýçta kinematik olarak kalýr
            }
            yield return new WaitForSeconds(Random.Range(destructionDelayMin, destructionDelayMax)); // max veya min bekleme süresi kadar bekle
        }

        // Geriye kalan parçalarý da ayný þekilde kinematik özelliðini kapat
        foreach (GameObject part in columnParts)
        {
            if (part == null) continue;

            Rigidbody rb = part.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Kinematik özelliðini kapat
                Vector3 randomForce = new Vector3(
                    Random.Range(-forceMagnitude, forceMagnitude),
                    Random.Range(-forceMagnitude, forceMagnitude),
                    Random.Range(-forceMagnitude, forceMagnitude)
                );
                rb.AddForce(randomForce); // Rasgele kuvvet uygula
                rb.AddTorque(Random.insideUnitSphere * forceMagnitude); // Rasgele dönüþ uygula
            }
            yield return new WaitForSeconds(Random.Range(destructionDelayMin, destructionDelayMax)); // Rasgele bir süre bekle
        }
    }
}
