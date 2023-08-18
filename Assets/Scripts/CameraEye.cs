using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEye : MonoBehaviour
{
    public Transform player;
    public float verticalSpeed = 100f, horizontalSpeed = 100f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = player.position + Vector3.up;
        transform.LookAt(player.forward);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotationX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * Time.deltaTime * horizontalSpeed, Vector3.up);
        Quaternion rotationY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * Time.deltaTime * verticalSpeed, -transform.right);

        transform.position = player.position + Vector3.up;
        transform.rotation = rotationX * rotationY * transform.rotation;
    }
}
