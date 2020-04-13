using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{

    //[SerializeField]
    //private float x_sensitivity = 3f;
    //[SerializeField]
    //private float y_sensitivity = 3f;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    float x_mouse = Input.GetAxis("Mouse X");

    //    Vector3 newRotation = transform.localEulerAngles;
    //    newRotation.y += x_mouse * x_sensitivity;
    //    this.transform.localEulerAngles = newRotation;


    //    float y_mouse = Input.GetAxis("Mouse Y");

    //    Vector3 newRotation2 = transform.localEulerAngles;
    //    newRotation2.x -= y_mouse * y_sensitivity;
    //    this.transform.GetChild(0).localEulerAngles = newRotation2;
    //}
    
    //cameraのtransform
    private Transform m_camTransform;

    //マウス操作の始点
    private Vector3 m_startMousePos;

    //カメラ回転の始点情報
    private Vector3 m_presentCamRotation;

    void Start()
    {
        m_camTransform = this.gameObject.transform;
    }

    void Update()
    {
        //カメラの回転 マウス
        CameraRotationMouseControl();
    }

    //カメラの回転 マウス
    private void CameraRotationMouseControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_startMousePos = Input.mousePosition;
            m_presentCamRotation.x = m_camTransform.transform.eulerAngles.x;
            m_presentCamRotation.y = m_camTransform.transform.eulerAngles.y;
        }

        if (Input.GetMouseButton(0))
        {
            //(移動開始座標 - マウスの現在座標) / 解像度 で正規化
            float x = (m_startMousePos.x - Input.mousePosition.x) / Screen.width;
            float y = (m_startMousePos.y - Input.mousePosition.y) / Screen.height;

            //回転開始角度 ＋ マウスの変化量 * 90
            float eulerX = m_presentCamRotation.x + y * 90.0f;
            float eulerY = m_presentCamRotation.y + x * 90.0f;

            m_camTransform.rotation = Quaternion.Euler(eulerX, eulerY, 0);
        }
    }
}
