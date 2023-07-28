using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public float sens, mouseX, mouseY, damp, camDamp, speed;
    public Transform mc, target;
    public CharacterController plane;
    float h;
    float v;

    void Start()
    {
        transform.LookAt(mc);
    }
    void FixedUpdate()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        mouseX -= Input.GetAxisRaw("Mouse X") * sens;
        mouseY += Input.GetAxisRaw("Mouse Y") * sens;
        CamControl();
    }

    void CamControl()
    {
        transform.LookAt(mc);
        target.LookAt(mc);
        target.transform.rotation = Quaternion.Lerp(Quaternion.Euler(target.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z), Quaternion.Euler(mouseY, mouseX, 0), camDamp);
        if (mouseX != 0 | mouseY != 0)
        {
            mc.rotation = Quaternion.Lerp(new Quaternion(mc.transform.rotation.x, mc.transform.rotation.y, mc.transform.rotation.z, mc.transform.rotation.w), new Quaternion(target.transform.rotation.x, target.transform.rotation.y, target.transform.rotation.z, target.transform.rotation.w), damp);
        }
        Vector3 moveDir = transform.forward;
        plane.Move(moveDir * speed * Time.deltaTime);
    }
}