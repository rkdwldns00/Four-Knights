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
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");
        if (hor != 0 || ver != 0)
        {
            Vector2 direction = new Vector2(hor, ver).normalized;
            Vector3 mul = Quaternion.Euler(0, camControll.camRotX, 0) * -new Vector3(direction.x, 0, direction.y);
            direction = new Vector2(mul.x, mul.z);

            if (Input.GetKey(KeyCode.LeftShift))
            {
                Run(direction);
            }
            else
            {
                Walk(direction);
            }

            if(hor != 0)
            {
                camControll.camRotX += hor * Time.deltaTime * 5;
            }
        }
    }
}
