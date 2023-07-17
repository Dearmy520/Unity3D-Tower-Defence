using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float MouseWhellSpeed;

    // Update is called once per frame
    void Update()
    {

        float InputX = Input.GetAxis("Horizontal");
        float InputZ = Input.GetAxis("Vertical");
        float InputMouse = Input.GetAxis("Mouse ScrollWheel");
        transform.Translate(new Vector3(InputX * Speed, InputMouse * MouseWhellSpeed, InputZ * Speed) * Time.deltaTime, Space.World);
    }
}
