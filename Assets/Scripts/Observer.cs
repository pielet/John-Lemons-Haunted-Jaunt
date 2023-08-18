using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;

    bool m_IsInRange = false;

    // Update is called once per frame
    void Update()
    {
        if (m_IsInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit_info;

            if (Physics.Raycast(ray, out hit_info))
            {
                if (hit_info.collider.transform == player)
                    gameEnding.CaughtPlayer();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
            m_IsInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
            m_IsInRange = false;
    }
}
