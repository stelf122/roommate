using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private CameraConfig _config;
    
    private void Update()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 viewInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        bool holding = Input.GetMouseButton(0);

        if (moveInput.magnitude > 0)
        {
            transform.position += transform.right * _config.MoveSpeed() * moveInput.x;
            transform.position += transform.forward * _config.MoveSpeed() * moveInput.y;
        }

        if (holding && viewInput.magnitude > 0)
        {
            transform.eulerAngles += new Vector3(-viewInput.y * _config.ViewSpeed(), viewInput.x * _config.ViewSpeed(), 0);
        }
    }
}
