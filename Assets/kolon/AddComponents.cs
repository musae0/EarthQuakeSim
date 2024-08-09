using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddComponents : MonoBehaviour
{
    public string partNamePrefix = "Cube_cell."; // Par�a isimlerinin ortak prefix'i
    public int partCount = 97; // Toplam par�a say�s�

    void Start()
    {
        for (int i = 0; i < partCount; i++)
        {
            string partName = partNamePrefix + i.ToString("D3"); // �rne�in "Cube_cell.001"
            GameObject part = GameObject.Find(partName);
            if (part != null)
            {
                if (part.GetComponent<Collider>() == null)
                {
                    part.AddComponent<BoxCollider>(); // Veya uygun ba�ka bir Collider ekleyin
                }
                if (part.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody rb = part.AddComponent<Rigidbody>();
                    rb.isKinematic = true; // Ba�lang��ta kinematic olmal�
                }
            }
            else
            {
                Debug.LogError("Kolon par�as� bulunamad�: " + partName);
            }
        }
    }
}
