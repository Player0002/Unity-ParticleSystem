using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;
using UnityEngine.Experimental.XR.Interaction;

public class RotateMouse : MonoBehaviour
{
    public Camera cam;
    public bool isY = false;

    private Ray rayMouse;
    private Vector3 pos;
    private Vector3 direction;
    private Quaternion rotation;
    private Vector3 currentDistance;
    private Vector3 targetpos;
    private bool moveit;
    public float maxSpeed; //최대 속도
    public float aclrt; //가속도

    private float curr_speed; //현재 속도
    private Vector3 dir; //방향

    void Start()
    {
        currentDistance = cam.transform.position - gameObject.transform.position;
    }
    void Update()
    {
        if (cam != null)
        {
            RotateToMouse();
            MoveMouse();
            MovePosition();
            if (Input.GetKeyDown(KeyCode.Y)) isY = !isY;
        }
    }

    void MovePosition()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 10000f))
            {
                if (hit.transform.tag == "Ground")
                {
                    targetpos = hit.point;
                    targetpos.y = 1;
                }
            }
            moveit = true;
        }
        if (moveit == true)
        {
            transform.localPosition = Vector3.Lerp(transform.position, targetpos, Time.deltaTime * 1f);
            float dis = Vector3.Distance(transform.position, targetpos);
            if (dis < 0.01f)
            {
                moveit = false;
            }
        }
    }
    void MoveMouse()
    {
        if (isY)
        {
            Vector3 position = gameObject.transform.position;
            cam.transform.position = new Vector3(position.x, 0 ,position.z);
            cam.transform.position += currentDistance;
        }
        else
        {
            Debug.Log(Input.mousePosition + " / " + cam.ScreenToViewportPoint(Input.mousePosition));
            var direction = cam.ScreenToViewportPoint(Input.mousePosition);

            // X Point
            if (direction.x <= 0.1)
                cam.transform.position += Vector3.left / 9;

            if (direction.x >= 0.9)
                cam.transform.position += Vector3.right / 9;

            // Y Point
            if (direction.y <= 0.1) cam.transform.position += Vector3.back / 9;
            if (direction.y >= 0.9) cam.transform.position += Vector3.forward / 9;
        }
    }

    void RotateToMouse()
    {
        RaycastHit hitInfo;
        rayMouse = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(rayMouse, out hitInfo, Mathf.Infinity))
        {
            Vector3 targetPos = new Vector3(hitInfo.point.x - transform.position.x, 0f, hitInfo.point.z - transform.position.z);
            rotation = Quaternion.LookRotation(targetPos);
            gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.rotation, rotation, 1);
        }
        else
        {
            rotation = gameObject.transform.localRotation;
        }
    }

    public Quaternion getRot()
    {
        return rotation;
    }











 

}
