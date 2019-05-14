using System.Collections.Generic;
using UnityEngine;

public class asf : MonoBehaviour
{
    public GameObject firePoint;
    public List<GameObject> vfx = new List<GameObject>();
    public RotateMouse rotateMouse;
    private GameObject effectSpawn;
    [SerializeField]
    private float fireRate = 1.0f;

    private float _lastFire;
    void Start()
    {
        effectSpawn = vfx[0];
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.time + " / " + (_lastFire + fireRate));
        if (Input.GetMouseButton(0) && Time.time > _lastFire + fireRate)
        {
            _lastFire = Time.time;
            Spawn();
        }
    }

    void Spawn()
    {
        GameObject vfx;
        if (firePoint != null)
        {
            vfx = (Instantiate(effectSpawn, firePoint.transform.position, Quaternion.identity));
            if (rotateMouse != null)
            {
                vfx.transform.localRotation = rotateMouse.getRot();
            }
        }
    }
}
