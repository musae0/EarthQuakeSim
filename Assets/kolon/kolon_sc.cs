using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kolon_sc : MonoBehaviour
{
    public GameObject[] columnParts; // Kolon parçalarýný buraya atayýn
    public float destructionDelayMin = 0.1f; // En az yýkýlma gecikmesi
    public float destructionDelayMax = 1.0f; // En fazla yýkýlma gecikmesi
    public float forceMagnitude = 10f; // Uygulanacak kuvvetin büyüklüðü

    public bool isQuaking = false;

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
