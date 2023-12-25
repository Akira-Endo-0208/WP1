using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{

    CharacterController con;
    Animator anim;

    float normalSpeed = 3f; // 通常時の移動速度
    float sprintSpeed = 5f; // ダッシュ時の移動速度
    float jump = 8f;        // ジャンプ力
    float gravity = 10f;    // 重力の大きさ
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
        
        // 移動速度を取得
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : normalSpeed;

        // カメラの向きを基準にした正面方向のベクトル
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;


        // 前後左右の入力（WASDキー）から、移動のためのベクトルを計算
        // Input.GetAxis("Vertical") は前後（WSキー）の入力値
        // Input.GetAxis("Horizontal") は左右（ADキー）の入力値
        Vector3 moveZ = cameraForward * Input.GetAxis("Vertical") * speed * 0;  //　前後（カメラ基準）　 
        Vector3 moveX = Camera.main.transform.right * Input.GetAxis("Horizontal") * speed; // 左右（カメラ基準）

        // isGrounded は地面にいるかどうかを判定します
        // 地面にいるときはジャンプを可能に

        // 重力を効かせる
        
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


        // 移動のアニメーション
        anim.SetFloat("MoveSpeed", (moveZ + moveX).magnitude);

        // プレイヤーの向きを入力の向きに変更　
        transform.LookAt(transform.position + moveZ + moveX);

        // Move は指定したベクトルだけ移動させる命令
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