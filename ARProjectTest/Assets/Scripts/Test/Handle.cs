using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
             //print(Input.mousePosition);

            float x = Input.mousePosition.x;
            float y = Input.mousePosition.y;
            if (y <= 425 && y >= 245)
            {
                x = (x - 240) / -2;
                transform.eulerAngles = new Vector3(0, 0, x);
            }
            else if (y < 245 && y >= 65)
            {
                x = (x + 90) / 2;
                transform.eulerAngles = new Vector3(0, 0, x);
            }
        }
    }
}
