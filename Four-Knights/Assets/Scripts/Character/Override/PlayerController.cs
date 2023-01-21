using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    CameraControll camControll;

    protected override void Start()
    {
        base.Start();
        camControll = GetComponent<CameraControll>();
    }

    protected override void Update()
    {
        base.Update();
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Run(direction);
            }
            else
            {
                Walk(direction);
            }
        }
    }
}
