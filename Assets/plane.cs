using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plane : MonoBehaviour
{
    Color32 m_defaultColor;
    Color32 m_collisionColor;
    /** 本来の色から変えるか */
    bool m_defaultColorRequired = false;
    bool m_blinkRequired = false;
    List<Coroutine> events;

    const float BLINK_TIME = 3f;
    // Start is called before the first frame update
    void Start()
    {
        //オブジェクトの色をRGBA値を用いて変更する
        m_defaultColor =  new Color32(0, 168, 255, 125);
        this.GetComponent<MeshRenderer>().material.color = m_defaultColor;
        //オブジェクトの色をRGBA値を用いて変更する
        m_collisionColor  = new Color32(0, 255, 168, 125);

        events = new List<Coroutine>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_defaultColorRequired)
        {
            this.GetComponent<MeshRenderer>().material.color = m_collisionColor;
        } else
        {
            this.GetComponent<MeshRenderer>().material.color = m_defaultColor;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (events.Count > 0)
        {
            events.ForEach(delegate (Coroutine c) {
                StopCoroutine(c);
            });
            events.Clear();
        }

        events.Add(StartCoroutine(ColorChange()));
        events.Add(StartCoroutine(ColorFlash()));

    }
    // 3秒後に点滅を止める
    IEnumerator ColorChange()
    {
        m_blinkRequired = true;
        //
        yield return new WaitForSeconds(BLINK_TIME);


        m_blinkRequired = false;

        m_defaultColorRequired = false;
    }
    // 0.1秒ごとに点滅させる
    IEnumerator ColorFlash()
    {
        while (m_blinkRequired)
        {
            m_defaultColorRequired = !m_defaultColorRequired;//反転
            yield return new WaitForSeconds(0.1f);

        }

    }

}
