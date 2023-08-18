using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f, jumpForce = 200f;
    public Transform FollowCamera;

    AudioSource m_FootStep;
    Animator m_Animator;
    Rigidbody m_Rigidbody;

    Vector3 m_Movement;
    Quaternion m_Rotation = Quaternion.identity;
    bool isJump = false;

    string mode = "follow";

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_FootStep = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (mode == "down")
            m_Movement.Set(horizontal, 0f, vertical);
        if (mode == "follow")
        {
            Vector3 forward = FollowCamera.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 left = Vector3.Cross(Vector3.up, forward);

            m_Movement = forward * vertical + left * horizontal;
        }

        m_Movement.Normalize();

        bool isInSpace = transform.position.y > 0.1;
        if (!isJump && !isInSpace && Input.GetKeyDown(KeyCode.Space))
            isJump = true;

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput || isInSpace;
        m_Animator.SetBool("IsWalking", isWalking);

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);

        if (isWalking && !isInSpace)
        {
            if (!m_FootStep.isPlaying)
                m_FootStep.Play();
        }
        else
            m_FootStep.Stop();
    }

    void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);

        if (isJump)
        {
            m_Rigidbody.AddForce(new Vector3(0, jumpForce, 0));
            isJump = false;
        }
    }

    public void ChangeMode(string new_mode, bool enbleMesh, Transform newCamera = null)
    {
        mode = new_mode;
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = enbleMesh;
        if (newCamera)
            FollowCamera = newCamera;
    }
}
