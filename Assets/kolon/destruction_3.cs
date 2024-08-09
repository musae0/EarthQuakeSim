using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destruction_3 : MonoBehaviour
{
    public string partNamePrefix = "Cube_cell."; // Par�a isimlerinin ortak prefix'i
    public int partCount = 98; // Toplam par�a say�s�
    public float delayBetweenChanges = 0.5f; // Kinematik �zelli�ini kapatma gecikmesi
    public KeyCode toggleKey = KeyCode.Space; // Kinematik kapatma i�lemini tetiklemek i�in kullan�lan tu�

    private GameObject[] columnParts;
    private bool isQuaking = false;
    private Coroutine destructionCoroutine;

    void Start()
    {
        // Par�alar� isimlerine g�re bul ve diziye ekle
        columnParts = new GameObject[partCount];
        for (int i = 0; i < partCount; i++)
        {
            string partName = partNamePrefix + i.ToString("D3"); // �rne�in "Cube_cell.001"
            columnParts[i] = GameObject.Find(partName);
            if (columnParts[i] == null)
            {
                Debug.LogError("Kolon par�as� bulunamad�: " + partName);
            }
        }
    }

    void Update()
    {
        // Belirli bir tu�a bas�ld���nda kinematik kapatma i�lemini ba�lat veya durdur
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
                    rb.isKinematic = false; // Kinematik �zelli�ini kapat
                    Debug.Log("Kinematik kapat�ld�: " + randomPart.name);
                }
            }

            yield return new WaitForSeconds(delayBetweenChanges); // Gecikme s�resi bekle
        }
    }
}
