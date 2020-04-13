using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    ball m_ball;
    LinkedList<Transform> rigids;

    // Start is called before the first frame update
    void Start()
    {
        rigids = new LinkedList<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move();
        Shot();

    }
    void OnTriggerEnter(Collider other)
    {
        Adsorb(other);
    }
    void Adsorb(Collider other)
    {
        rigids.AddLast(ConstRigidbody(other));
        WannaBeParent(other);
    }
    Transform ConstRigidbody(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        return other.gameObject.transform;
    }
    void WannaBeParent(Collider other)
    {
        other.gameObject.transform.parent = this.transform;
        other.gameObject.transform.position = this.transform.forward + this.transform.position;
    }

    void Shot()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (rigids.Count > 0)
            {
                rigids.First.Value.GetComponent<Rigidbody>().constraints
                    = RigidbodyConstraints.None;
                rigids.First.Value.GetComponent<Rigidbody>().transform.parent = null;
                rigids.First.Value.GetComponent<Rigidbody>().
                    AddForce(this.transform.forward * 1000);
                rigids.RemoveFirst();
            }

        }


    }
    void Move()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * Time.deltaTime * -1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position += transform.forward * Time.deltaTime * -1;
        }
    }
}
