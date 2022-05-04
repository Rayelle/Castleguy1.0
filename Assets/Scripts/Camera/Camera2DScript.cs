using System;
using UnityEngine;

public class Camera2DScript : MonoBehaviour
{
    public Transform target;
    public float damping = 1;
    public float lookDelayedValue = 3;
    public float lookDelayedReturn = 0.5f;
    public float lookDelayedMoveBegin = 0.1f;
    public

    float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;


    private void Start()
    {
        SearchforPlayer();
    }


    private void Update()
    {
        float xMoveDelta = (target.position - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Math.Abs(xMoveDelta) > lookDelayedMoveBegin;

        if (updateLookAheadTarget)
        {
            m_LookAheadPos = lookDelayedValue * Vector3.right * Math.Sign(xMoveDelta);
        }
        else
        {
            m_LookAheadPos = lookDelayedValue * Vector3.MoveTowards(m_LookAheadPos, Vector3.zero,
                Time.deltaTime * lookDelayedReturn);
        }

        Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos,
            ref m_CurrentVelocity, damping);

        transform.position = newPos;

        m_LastTargetPosition = target.position;




    }

    public void SearchforPlayer()
    {
        m_LastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }

}
