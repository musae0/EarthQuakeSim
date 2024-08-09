using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float moveSpeed = 500f;
    public float rotationSpeed = 10000f;

    // Update is called once per frame
    void Update()
    {
        // Hareket için W, A, S, D tuþlarýný kullan
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        movement = transform.TransformDirection(movement);
        transform.position += movement * moveSpeed * Time.deltaTime;

        // Mouse'un sað tuþuna basýldýðýnda kamerayý döndür
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

            // Dikey eksende sýnýrlama
            Vector3 currentRotation = transform.localEulerAngles;
            float newRotationX = currentRotation.x - mouseY;

            // Dik açýya yakýn bir dönüþü önlemek için açýyý sýnýrlayýn
            if (newRotationX > 180f) newRotationX -= 360f; // normalize degree
            newRotationX = Mathf.Clamp(newRotationX, -80f, 80f); // sýnýrlama aralýðý

            transform.localEulerAngles = new Vector3(newRotationX, currentRotation.y + mouseX, 0);
        }
    }






    // Start is called before the first frame update
    void Start()
    {
        
    }

   
}
