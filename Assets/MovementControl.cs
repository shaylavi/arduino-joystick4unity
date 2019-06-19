using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    Vector3 loc;
    public bool useArduino = false;
    public Arduino arduinoScript;
    private int x = 0;
    private int y = 0;
    private bool press = false;
    private const int X_ANKER = 503;
    private const int Y_ANKER = 534;

    // Start is called before the first frame update
    void Start()
    {
        loc = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float movementSpeed = 0.3f;

        if (!useArduino)
        {
            if (Input.GetKey("w"))
            {
                loc.y += movementSpeed;
                gameObject.transform.position = loc;
            }
            if (Input.GetKey("s"))
            {
                loc.y -= movementSpeed;
                gameObject.transform.position = loc;
            }
            if (Input.GetKey("a"))
            {
                loc.x -= movementSpeed;
                gameObject.transform.position = loc;
            }
            if (Input.GetKey("d"))
            {
                loc.x += movementSpeed;
                gameObject.transform.position = loc;
            }
        }
        else
        {
            this.x = arduinoScript.getX() - X_ANKER;
            this.y = arduinoScript.getY() - Y_ANKER;
            this.press = arduinoScript.getPress();

            if (this.x < 0 && this.x < -1)
            {
                movementSpeed += (float)Mathf.Abs(this.x) / X_ANKER;

                loc.x -= movementSpeed;
                gameObject.transform.position = loc;
            }
            if (this.x > 0)
            {
                movementSpeed += (float)Mathf.Abs(this.x) / X_ANKER;

                loc.x += movementSpeed;
                gameObject.transform.position = loc;
            }
            if (this.y < 0)
            {
                movementSpeed += (float)Mathf.Abs(this.y) / Y_ANKER;

                loc.y -= movementSpeed;
                gameObject.transform.position = loc;
            }
            if (this.y > 0)
            {
                movementSpeed += (float)Mathf.Abs(this.y) / Y_ANKER;

                loc.y += movementSpeed;
                gameObject.transform.position = loc;
            }
            if (this.press)
            {
                Debug.Log("Button fired..");
            }

        }
    }

    public int getX()
    {
        return this.x;
    }
    public int getY()
    {
        return this.y;
    }

}
