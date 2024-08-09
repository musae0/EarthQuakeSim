using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Cubeprefab;
    public GameObject Zeminprefab;// Yerleþtirilecek Prefab
    public Camera camera; // Kamera
    Vector3 myvector;
   

    void Start()
    {
        // Eðer kamera atanmamýþsa ana kamera ile kullan
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    void Update()
    {
        myvector = new Vector3(0.0f, 20.0f, 0.0f);
        // "1" tuþuna basýldýðýnda bir nesne oluþtur
        if (Input.GetKeyDown(KeyCode.Alpha1)) // "1" tuþu
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition); // Fare konumundan bir ýþýn gönder
            RaycastHit hit; // Hit testi için

            if (Physics.Raycast(ray, out hit)) // Eðer bir þey ile çarpýþýrsa
            {
                // Týklanan noktaya Prefab'ý yerleþtir
                Instantiate(Cubeprefab, hit.point + myvector, Quaternion.identity); // Varsayýlan dönüþ açýsý kullanýlýyor
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // "2" tuþu
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition); // Fare konumundan bir ýþýn gönder
            RaycastHit hit; // Hit testi için

            if (Physics.Raycast(ray, out hit)) // Eðer bir þey ile çarpýþýrsa
            {
                // Týklanan noktaya Prefab'ý yerleþtir
                Instantiate(Zeminprefab, hit.point + myvector, Quaternion.identity); // Varsayýlan dönüþ açýsý kullanýlýyor
            }
        }


    }
}
