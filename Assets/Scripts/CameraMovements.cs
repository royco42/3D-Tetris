using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovements : MonoBehaviour
{

    [SerializeField] private Camera cam;
    [SerializeField] private Transform target;
    
    private Vector3 prevPosition;

    // Update is called once per frame
    void Update()
    {

            if (Input.GetMouseButtonDown(1))
            {
                prevPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(1))
            {
                Vector3 direction = prevPosition - cam.ScreenToViewportPoint(Input.mousePosition);
                // get the mouse position and add to camera
                cam.transform.position = target.position;
                //moving up and down with mouse
                cam.transform.Rotate(new Vector3(1, 0, 0), direction.y * 90);
                //moving right and left 
                cam.transform.Rotate(new Vector3(0, 1, 0), direction.x * 90, Space.World);
                cam.transform.Translate(new Vector3(0, 0, -18));
                //maintain smoothnes
                prevPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }
    }
}
