using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//シーンマネジメントを有効にする

public class Title : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown("space") || Input.GetButtonDown("Action1") || Input.GetButtonDown("Jump")) //マウス左クリック、スペースキー、Aボタン、ジャンプボタンを押した場合
        {
            SceneManager.LoadScene("some_sensei");//some_senseiシーンをロードする
        }

    }
}
