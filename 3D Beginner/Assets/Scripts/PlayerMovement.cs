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
    public AudioSource walkingAudio;
 
    void FixedUpdate()
    {
        // ĳ������ �̵�
        float horizontal = Input.GetAxis("Horizontal"); // horizontal �׸�(����) �ȿ� <- : -1 , -> : +1  , region ����
        float vertical = Input.GetAxis("Vertical"); // vertical �׸�(����) �ȿ� ������Ű : +1 , �Ʒ�����Ű : -1
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        // ĳ������ �ִϸ��̼�
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);
        if (isWalking == true)
        {
            if(walkingAudio.isPlaying == false)
            {
                walkingAudio.Play();
            }
        }
        else
        {
            walkingAudio.Stop();
        }
        // ĳ������ ȸ��
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
