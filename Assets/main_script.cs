using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_script : MonoBehaviour
{
    public float Volume;
    public AudioClip Sound1;
    AudioSource audioSource;
    int tragetnumeber;
    int soundlevel;
    int number;
    int difference;
    int passtimes;
    public int pass;

    private Camera mainCamera;
    private Vector2 lastMousePosition;

    void Start()
    {
        mainCamera = Camera.main;
        this.GetComponent<main_script>().setplay();
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
        Debug.Log(difference);
    }


    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        /*if (Input.GetKeyDown(KeyCode.A))
        {
            AudioSource audio = GetComponent<AudioSource>();
            audio.volume = 0f;
            audioSource.PlayOneShot(Sound1);
        }*/
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            if (hit.collider.gameObject.tag == "dial")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    number -= 6;
                    transform.Rotate(0f, 0f, -6f);
                    this.GetComponent<main_script>().CalcAnswer();
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.volume = difference;
                    audioSource.PlayOneShot(Sound1);
                    Debug.Log(number);
                    number = (int)(transform.rotation.z / 6);
                    if (number > 0)
                    {
                        transform.rotation = Quaternion.Euler(0f, 0f, 360f);
                        number = 60;

                    }
                }
                else
                    if (Input.GetMouseButtonDown(1))
                {
                    number += 6;
                    transform.Rotate(0f, 0f, 6f);
                    this.GetComponent<main_script>().CalcAnswer();
                    AudioSource audio = GetComponent<AudioSource>();
                    audio.volume = difference;
                    audioSource.PlayOneShot(Sound1);
                    Debug.Log(number);
                    number = (int)(transform.rotation.z / 6);
                    if (number>60)
                    {
                        transform.rotation= Quaternion.Euler(0, 0, 0);
                        number = 0;
                    }


                }
            }
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);

        }
    }
}
