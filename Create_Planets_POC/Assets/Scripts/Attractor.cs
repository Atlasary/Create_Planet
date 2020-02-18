using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    private AttractorManager manager;
    private Rigidbody rb;
    public Rigidbody rbsun;
    //public Vector3 iniForce;
    public bool isPlanet;

    // Update is called once per frame

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = FindObjectOfType<AttractorManager>();
        manager.T = 1;
        Initialize();
    }

    public void Attract(Attractor obj)
    {
        Rigidbody rb2 = obj.rb;

        Vector3 dir = rb2.position - rb.position;
        float dist = dir.magnitude;

        float force = manager.G * (rb.mass * (manager.T*manager.T) * rb2.mass) / (dist * dist);
        
        rb.AddForce(dir.normalized*force);
    }

    public void Initialize()
    {
        if (isPlanet)
        {
            Vector3 dir = (rbsun.position - rb.position).normalized;
            rb.AddForce(new Vector3(dir.z, 0, -dir.x) * rb.mass * 50f * manager.T * Mathf.Sqrt(rbsun.mass / (rbsun.position - rb.position).magnitude));
            if (rbsun.gameObject.GetComponent<Attractor>().isPlanet)
            {
                Rigidbody rbSsun = rbsun.gameObject.GetComponent<Attractor>().rbsun;
                print(rbsun.velocity);
                rb.AddForce(new Vector3(dir.z, 0, -dir.x) * rb.mass * 50f * manager.T * Mathf.Sqrt(rbSsun.mass / (rbSsun.position - rb.position).magnitude));
            }
        }
    }
}
