using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using UnityEngine.UI;

public class Arduino : MonoBehaviour
{

    public Text externalTextResource;
    private string[] arduinoOutput = new string[3];

    SerialPort stream;

    private int x = 0;
    private int y = 0;
    private bool press = false;
    private bool status = false;

    public int getX()
    {
        return this.x;
    }

    public int getY()
    {
        return this.y;
    }

    public bool getPress()
    {
        return this.press;
    }
    public bool getStatus()
    {
        return this.status;
    }


    void Start()
    {
        openStream(true);
    }

    private void OnApplicationQuit()
    {
        this.status = false;
        if (stream != null && stream.IsOpen)
            stream.Close();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            openStream(true);
            if (stream != null && stream.IsOpen)
            {
                status = true;
                arduinoOutput = stream.ReadLine().Split(',');
                stream.BaseStream.Flush();
                x = Int16.Parse(arduinoOutput[0]);
                y = Int16.Parse(arduinoOutput[1]);
                press = Int16.Parse(arduinoOutput[2]) == 0 ? false : true;

                updateText();
            }
            else
            {
                openStream(false);
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error caught: " + e);
        }
    }

    private bool openStream(bool open)
    {
        try
        {
            if (open)
            {
                if (stream == null)
                {
                    stream = new SerialPort("COM3", 9600);
                }
                else if (stream != null && !stream.IsOpen)
                {
                    stream.ReadTimeout = 1000;
                    stream.Open();
                    status = true;
                }

                return true;
            }
            else
            {
                if (stream != null && stream.IsOpen)
                    stream.Close();

                return false;
            }
        }
        catch (Exception e)
        {
            Debug.Log("Error caught: " + e);
            return false;
        }
    }
    private void updateText()
    {
        if (externalTextResource != null && externalTextResource.isActiveAndEnabled)
            externalTextResource.text = string.Format("X Value: {0}\r\nY Value: {1}\r\nButton: {2}", x, y, press);
    }


}
