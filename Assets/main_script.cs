using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_script : MonoBehaviour
{
    public AudioClip Sound1;
    AudioSource audioSource;
    int number;
    int pass1;
    int pass2;
    int pass3;
    int pass4;
    int pass5;
    int pass6;
    int pass7;
    int pass8;
    int pass9;
    int pass10;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        number = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 10.0f))
        {
            if (hit.collider.gameObject.tag == "dial")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (number <= 0)
                    {
                        number =60;
                    }
                    else
                    {
                        number -= 6;
                    }

                    transform.Rotate(0f, 0f, -6f);
                    audioSource.PlayOneShot(Sound1);
                    Debug.Log(number);
                }
                else
                    if (Input.GetMouseButtonDown(1))
                {
                    if (number >= 60)
                    {
                        number = 0;
                    }
                    else
                    {
                        number += 6;
                    }
                    transform.Rotate(0f, 0f, 6f);
                    audioSource.PlayOneShot(Sound1);
                    Debug.Log(number);

                }
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);
        
    }
}
