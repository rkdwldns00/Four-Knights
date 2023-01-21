using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    GameObject cam;
    public float camRotX { get; private set; }
    float camRotY = -50;
    float camDistance = 3;
    float dps = 4;

    void Start()
    {
        cam = FindObjectOfType<Camera>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            camRotY = Mathf.Clamp(camRotY + Input.GetAxis("Mouse Y") * dps, -40, 0);
            camRotX += Input.GetAxis("Mouse X") * dps;
            camDistance = Mathf.Clamp(camDistance + Input.GetAxis("Mouse ScrollWheel"), 1, 3);
        }

        Vector3 headPos = transform.position + Vector3.up * 1.5f;
        RaycastHit hit;
        if (Physics.Raycast(headPos, Quaternion.Euler(camRotY, camRotX, 0) * Vector3.forward, out hit, camDistance))
        {
            cam.transform.position = hit.point;
        }
        else
        {
            cam.transform.position = headPos + Quaternion.Euler(camRotY, camRotX, 0) * Vector3.forward * camDistance;
        }
        cam.transform.LookAt(headPos);
    }
}
