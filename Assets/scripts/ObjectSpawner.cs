using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Cubeprefab;
    public GameObject Zeminprefab;// Yerle�tirilecek Prefab
    public Camera camera; // Kamera
    Vector3 myvector;
   

    void Start()
    {
        // E�er kamera atanmam��sa ana kamera ile kullan
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    void Update()
    {
        myvector = new Vector3(0.0f, 20.0f, 0.0f);
        // "1" tu�una bas�ld���nda bir nesne olu�tur
        if (Input.GetKeyDown(KeyCode.Alpha1)) // "1" tu�u
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition); // Fare konumundan bir ���n g�nder
            RaycastHit hit; // Hit testi i�in

            if (Physics.Raycast(ray, out hit)) // E�er bir �ey ile �arp���rsa
            {
                // T�klanan noktaya Prefab'� yerle�tir
                Instantiate(Cubeprefab, hit.point + myvector, Quaternion.identity); // Varsay�lan d�n�� a��s� kullan�l�yor
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // "2" tu�u
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition); // Fare konumundan bir ���n g�nder
            RaycastHit hit; // Hit testi i�in

            if (Physics.Raycast(ray, out hit)) // E�er bir �ey ile �arp���rsa
            {
                // T�klanan noktaya Prefab'� yerle�tir
                Instantiate(Zeminprefab, hit.point + myvector, Quaternion.identity); // Varsay�lan d�n�� a��s� kullan�l�yor
            }
        }


    }
}
