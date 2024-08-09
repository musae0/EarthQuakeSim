using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddComponents : MonoBehaviour
{
    public string partNamePrefix = "Cube_cell."; // Parça isimlerinin ortak prefix'i
    public int partCount = 97; // Toplam parça sayýsý

    void Start()
    {
        for (int i = 0; i < partCount; i++)
        {
            string partName = partNamePrefix + i.ToString("D3"); // Örneðin "Cube_cell.001"
            GameObject part = GameObject.Find(partName);
            if (part != null)
            {
                if (part.GetComponent<Collider>() == null)
                {
                    part.AddComponent<BoxCollider>(); // Veya uygun baþka bir Collider ekleyin
                }
                if (part.GetComponent<Rigidbody>() == null)
                {
                    Rigidbody rb = part.AddComponent<Rigidbody>();
                    rb.isKinematic = true; // Baþlangýçta kinematic olmalý
                }
            }
            else
            {
                Debug.LogError("Kolon parçasý bulunamadý: " + partName);
            }
        }
    }
}
