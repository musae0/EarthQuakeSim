using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kolon_2 : MonoBehaviour
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
        for (int i = 0; i < partCount; i++)
        {
            string partName = partNamePrefix + i.ToString("D3"); // Örneðin "Cube_cell.001"
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
        foreach (GameObject part in columnParts)
        {
            Rigidbody rb = part.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Rigidbody'yi aktif hale getir
                Vector3 randomForce = new Vector3(
                    Random.Range(-forceMagnitude, forceMagnitude),
                    Random.Range(-forceMagnitude, forceMagnitude),
                    Random.Range(-forceMagnitude, forceMagnitude)
                );
                rb.AddForce(randomForce); // Rasgele kuvvet uygula
            }
            yield return new WaitForSeconds(Random.Range(destructionDelayMin, destructionDelayMax)); // Rasgele bir süre bekle
        }
    }
}
