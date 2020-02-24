using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRot : MonoBehaviour
{
    private Touch initTouch = new Touch();
    public Camera cam;

    private float rotX = 0f;
    private float rotY = 0f;
    private Vector3 origRot;

    public float rotSpeed = 0.5f;
    public float dir = -1;

    Vector3 tempPos;
    void Start()
    {
        origRot = cam.transform.eulerAngles;
        rotX = origRot.x;
        rotY = origRot.y;
    }


    void FixedUpdate()
    {


            if (Input.GetMouseButtonDown(0))
            {
                tempPos = Input.mousePosition;
            }

             if(Input.GetMouseButton(0))
             {
              if((tempPos != Input.mousePosition))
               {
                //swiping
                float deltaX = initTouch.position.x - Input.mousePosition.x;
                float deltaY = initTouch.position.y - Input.mousePosition.y;
                rotX -= deltaY * Time.deltaTime * rotSpeed * dir;
                rotY += deltaX * Time.deltaTime * rotSpeed * dir;
                cam.transform.eulerAngles = new Vector3(0f, 0, rotX);
               }
            }
            if (Input.GetMouseButtonUp(0))
            {
                tempPos = new Vector3(0, 0, 0);
            }     
    }
}
