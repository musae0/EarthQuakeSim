using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kolon_sc : MonoBehaviour
{
    public GameObject[] columnParts; // Kolon par�alar�n� buraya atay�n
    public float destructionDelayMin = 0.1f; // En az y�k�lma gecikmesi
    public float destructionDelayMax = 1.0f; // En fazla y�k�lma gecikmesi
    public float forceMagnitude = 10f; // Uygulanacak kuvvetin b�y�kl���

    public bool isQuaking = false;

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
            yield return new WaitForSeconds(Random.Range(destructionDelayMin, destructionDelayMax)); // Rasgele bir s�re bekle
        }
    }
}
