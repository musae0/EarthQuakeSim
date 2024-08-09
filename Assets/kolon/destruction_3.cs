using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruction_3 : MonoBehaviour
{
    public string partNamePrefix = "Cube_cell."; // Parça isimlerinin ortak prefix'i
    public int partCount = 98; // Toplam parça sayýsý
    public float delayBetweenChanges = 0.5f; // Kinematik özelliðini kapatma gecikmesi
    public KeyCode toggleKey = KeyCode.Space; // Kinematik kapatma iþlemini tetiklemek için kullanýlan tuþ

    private GameObject[] columnParts;
    private bool isQuaking = false;
    private Coroutine destructionCoroutine;

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
        // Belirli bir tuþa basýldýðýnda kinematik kapatma iþlemini baþlat veya durdur
        if (Input.GetKeyDown(toggleKey))
        {
            if (isQuaking)
            {
                StopCoroutine(destructionCoroutine);
                isQuaking = false;
            }
            else
            {
                destructionCoroutine = StartCoroutine(DisableKinematicRandomly());
                isQuaking = true;
            }
        }
    }

    IEnumerator DisableKinematicRandomly()
    {
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
