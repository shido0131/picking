﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_script : MonoBehaviour
{
    public float Volume;
    public AudioClip Sound1;
    AudioSource audioSource;
    public GameObject dummy_safe;
    public GameObject clear_safe;
    public GameObject password;
    public GameObject pass_select;
    int tragetnumeber;
    int soundlevel;
    public int number;
    int difference;
    int passtimes;
    int MousePosi_x;
    public float stoptime;
    public int pass;
    private Camera mainCamera;
    private float startMousePosition;
    private float lastMousePosition;
    private bool measurement;

    void Start()
    {
        mainCamera = Camera.main;
        GetComponent<main_script>().setplay();
        pass = Random.Range(1, 60);
    }
    void setplay()
    {
        number = 0;
        passtimes = 5;
        pass = Random.Range(1, 60);


    }
    void CalcAnswer()
    {
        difference = pass - number;
        if (difference < 0)
        {
            difference = difference * -1;
        }
    }
    void gameclear()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;


        if (pass == number)
        {
            stoptime += Time.deltaTime;

            if (stoptime >= 0.5f)
            {
                stoptime = 0;
                passtimes -= 1;
                pass = Random.Range(1, 60);
                if (passtimes == 0)
                {
                    GetComponent<main_script>().gameclear();
                }

            }else if (pass != number)
            {
                stoptime = 0;
            }
        }
        if (Input.GetMouseButton(0)&&measurement==true)
        {
            lastMousePosition = Input.mousePosition.x;
            //Debug.Log(lastMousePosition);
            MousePosi_x = (int)(lastMousePosition - startMousePosition) / 8;
            Debug.Log(MousePosi_x);
            if (MousePosi_x > 60)
            {
                MousePosi_x -= 60;
                //Debug.Log(startMousePosition);
            }
            transform.Rotate(0f, 0f, MousePosi_x);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            measurement = false;
            startMousePosition = 0;
            lastMousePosition = 0;
        }
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            if (hit.collider.gameObject.tag == "dial")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startMousePosition = Input.mousePosition.x;
                    lastMousePosition = 0;
                    measurement = true;
                }
                /*if (Input.GetMouseButtonDown(0))
                {
                    number -= 1;
                    transform.Rotate(0f, 0f, -6f);
                    this.GetComponent<main_script>().CalcAnswer();
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.volume = difference;
                    //audioSource.PlayOneShot(Sound1);
                    Volume = 0.1f;
                    if (number > 0)
                    {
                        transform.rotation = Quaternion.Euler(0f, 0f, 360f);
                        number = 60;

                    }
                }
                else
                    if (Input.GetMouseButtonDown(1))
                {
                    number += 1;
                    transform.Rotate(0f, 0f, 6f);
                    this.GetComponent<main_script>().CalcAnswer();
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.volume = difference;
                    //audioSource.PlayOneShot(Sound1);
                    Volume = 0.1f;
                    if (number>60)
                    {
                        transform.rotation= Quaternion.Euler(0, 0, 0);
                        number = 0;
                    }


                }*/
            }
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);

        }
    }
}
