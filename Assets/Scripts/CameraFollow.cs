using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float verticalSpeed = 100f, horizontalSpeed = 100f;
    public Transform player;
    
    Vector3 m_Offset;

    void Start()
    {
        m_Offset = new Vector3(-2f, 0.4f, 0);

        transform.position = player.position + Vector3.up + m_Offset;
        transform.LookAt(player.position + Vector3.up);
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotationX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * Time.deltaTime * horizontalSpeed, Vector3.up);
        Quaternion rotationY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * Time.deltaTime * verticalSpeed, -transform.right);

        m_Offset = rotationX * rotationY * m_Offset;

        transform.position = player.position + Vector3.up + m_Offset;
        transform.LookAt(player.position + Vector3.up);

        // avoid shalter
        AdjustPosition();
    }

    void AdjustPosition()
    {
        Ray ray;
        RaycastHit RaycastHit;

        while (true)
        {
            ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit))
            {
                if (RaycastHit.collider.transform == player)
                {
                    Debug.Log("break" + (transform.position - player.position - Vector3.up).magnitude);
                    break;
                }
                transform.position += transform.forward * 0.1f;
            }
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        Debug.Log("stay");
        AdjustPosition();
    }
}
