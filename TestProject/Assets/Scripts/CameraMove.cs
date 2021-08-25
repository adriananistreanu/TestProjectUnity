using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
  

    public Vector3 dragStartPosition;
    public Vector3 dragCurrentPosition;
    public Vector3 newPosition;
    public float movementTime;

    Vector3 limit;

   
    void Start()
    {
        newPosition = transform.position;
        
    }

    private void Update()
    {
           HandleMouseInput();
    }
  
    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if(plane.Raycast(ray, out entry))
            {
                dragStartPosition = ray.GetPoint(entry);
            }
        }

        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float entry;

            if (plane.Raycast(ray, out entry))
            {
                dragCurrentPosition = ray.GetPoint(entry);
                newPosition = transform.position + dragStartPosition - dragCurrentPosition;
            }
        }
    
        limit = new Vector3(Mathf.Clamp(transform.position.x, -66f, 66f), transform.position.y, Mathf.Clamp(transform.position.z, -150, 150f));
        transform.position = Vector3.Lerp(limit, newPosition, Time.deltaTime * movementTime);
       
    }
   

}
