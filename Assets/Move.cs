using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float maxTime;
    public GameObject muzzle;
    public GameObject hit;
    void Start()
    {
        if (muzzle != null)
        {
            var muzvfx = Instantiate(muzzle, transform.position, Quaternion.identity);
            muzvfx.transform.forward = gameObject.transform.forward;
            var pm = muzvfx.GetComponent<ParticleSystem>();
            if (pm != null)
            {
                Destroy(muzvfx, pm.main.duration);
            }
            else
            {
                var pc = muzvfx.transform.GetChild(0).GetComponent<ParticleSystem>();
                Destroy(muzvfx, pc.main.duration);
            }
        }
        Destroy(gameObject, maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (speed != 0)
        {
            transform.position += transform.forward * (speed * Time.deltaTime);
        }
    }

    /*void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag == "Wall") Destroy(gameObject);
    }*/

    void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag.Equals("Wall"))
        {
            ContactPoint point = col.contacts[0];
            Quaternion rot = Quaternion.FromToRotation(-Vector3.forward, point.normal);
            Vector3 pos = point.point;
            if (hit != null)
            {
                var hitvfx = Instantiate(hit, pos, rot);
                var pm = hitvfx.GetComponent<ParticleSystem>();
                if (pm != null)
                    Destroy(hitvfx, pm.main.duration);
                else
                {
                    var pc = hitvfx.transform.GetChild(0).GetComponent<ParticleSystem>();
                    Destroy(hitvfx, pc.main.duration);
                }
            }
            speed = 0;
            Destroy(gameObject);
        }
    }
}
