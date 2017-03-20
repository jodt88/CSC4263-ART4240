using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerDos : MonoBehaviour {

    float Speed = 8.0f;
    Transform endPoint;
    Transform cam;
    bool enter = false;


    void Start()
    {
        //can probably replace the CamPoses with the room transforms as they are the same
        endPoint = GameObject.FindGameObjectWithTag("RightCamPos").transform;
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.position;
        pos.z = -10;
        cam.position = pos;
        //need to change so it translates the transform instead of interpolating
        if (enter)
            cam.transform.position = Vector3.Lerp(cam.position, endPoint.position, Time.deltaTime * Speed);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            enter = true;
        }

    }

    void switchEndpoint()
    {
        if (endPoint.tag == "RightCamPos")
        {
            endPoint = GameObject.FindGameObjectWithTag("MidCamPos").transform;
        }

        else
            endPoint = GameObject.FindGameObjectWithTag("RightCamPos").transform;

    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            switchEndpoint();
            enter = false;
        }
    }

}
