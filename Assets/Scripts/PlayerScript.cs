using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{

    CharacterController con;
    Animator anim;

    float normalSpeed = 3f; // �ʏ펞�̈ړ����x
    float sprintSpeed = 5f; // �_�b�V�����̈ړ����x
    float jump = 8f;        // �W�����v��
    float gravity = 10f;    // �d�͂̑傫��
    bool isJumping = false;
    Vector3 moveDirection = Vector3.zero;

    Vector3 startPos;


    Vector3 ZmodifyPos;
    void Start()
    {
        con = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();



        startPos = transform.position;

        ZmodifyPos.z = 0;
    }

    void Update()
    {
        
        // �ړ����x���擾
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : normalSpeed;

        // �J�����̌�������ɂ������ʕ����̃x�N�g��
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;


        // �O�㍶�E�̓��́iWASD�L�[�j����A�ړ��̂��߂̃x�N�g�����v�Z
        // Input.GetAxis("Vertical") �͑O��iWS�L�[�j�̓��͒l
        // Input.GetAxis("Horizontal") �͍��E�iAD�L�[�j�̓��͒l
        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed * 0;  //�@�O��i�J������j�@ 
        Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * speed; // ���E�i�J������j

        // isGrounded �͒n�ʂɂ��邩�ǂ����𔻒肵�܂�
        // �n�ʂɂ���Ƃ��̓W�����v���\��

        // �d�͂���������
        
        moveDirection.y += Physics.gravity.y * Time.deltaTime;
        
        if (con.isGrounded && isJumping == false)
        {
            moveDirection.y = -0.5f;

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jump;
                isJumping = true;
                anim.SetBool("is_jumping", true);
            }


        }
        else if (con.isGrounded && isJumping == true)
        {
            isJumping = false;
            anim.SetBool("is_jumping", false);
            
        }



        moveDirection = moveZ + moveX + new Vector3(0, moveDirection.y, 0);


        // �ړ��̃A�j���[�V����
        anim.SetFloat("MoveSpeed", (moveZ + moveX).magnitude);

        // �v���C���[�̌�������͂̌����ɕύX�@
        transform.LookAt(transform.position + moveZ + moveX);

        // Move �͎w�肵���x�N�g�������ړ������閽��
        con.Move(moveDirection * Time.deltaTime);

        if (transform.position.z > 0 || transform.position.z < 0)
        {
            transform.position = ZmodifyPos;
        }
    }

    public void MoveStartPos()
    {
        con.enabled = false;

        moveDirection = Vector3.zero;
        transform.position = startPos + Vector3.up * 3f;
        transform.rotation = Quaternion.identity;

        con.enabled = true;
    }
}