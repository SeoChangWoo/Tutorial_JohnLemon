using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Vector3 m_Movement; 
    public Animator m_Animator;
    public float turnSpeed;
    Quaternion m_Rotation = Quaternion.identity;
    public Rigidbody m_Rigidbody;
 
    void FixedUpdate()
    {
        // 캐릭터의 이동
        float horizontal = Input.GetAxis("Horizontal"); // horizontal 그릇(변수) 안에 <- : -1 , -> : +1  , region 변수
        float vertical = Input.GetAxis("Vertical"); // vertical 그릇(변수) 안에 윗방향키 : +1 , 아랫방향키 : -1
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        // 캐릭터의 애니메이션
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);
        // 캐릭터의 회전
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
        //Vector3 direction = new Vector3(horizontal, 0, vertical);
        //if(!(horizontal == 0 && vertical == 0))
        //{
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 2f);
        //}
    }

    private void OnAnimatorMove()
    {
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}
