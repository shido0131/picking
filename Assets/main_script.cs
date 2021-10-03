using UnityEngine;
using UnityEngine.UI;

public class main_script : MonoBehaviour
{
    public float Volume;
    public AudioClip Sound1;
    AudioSource audioSource;
    public GameObject dummy_safe;
    public GameObject clear_safe;
    public Text nownuber;
    int tragetnumeber;
    int soundlevel;
    int number;
    int difference;
    int passtimes;
    float MousePosi_x;
    float MousePosi_y;
    float rote_z;
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
        nownuber.text = number.ToString();

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

            }
            else if (pass != number)
            {
                stoptime = 0;
            }
        }
        if (Input.GetMouseButton(0) && measurement == true)
        {
            lastMousePosition = Input.mousePosition.x;
            MousePosi_x = (lastMousePosition - startMousePosition);
            MousePosi_x = Mathf.Floor(MousePosi_x);

            /*if (360f >= this.transform.rotation.z + MousePosi_x)
            {
                transform.Rotate(0f, 0f, MousePosi_x);
            }
            else
            {
                transform.Rotate(0f, 0f, 360f - (transform.rotation.z + MousePosi_x));
            }
            rote_z = transform.rotation.z;
            number += (int)MousePosi_x;*/
            /*if (transform.rotation.z < 0)
            {
                number = ((int)transform.rotation.z * -1 + 180)/6;
            }else if(transform.rotation.z>0)
            {
                number = (int)transform.rotation.z/ 6;
            }
            if (((int)transform.rotation.z * -1 + 180) / 6 == 0 | (int)transform.rotation.z * -1 + 180 / 6 == 0)
            {
                number = 60;
            }*/
            /*if (number + (int)MousePosi_x <= 60)
            {
                number += (int)MousePosi_x-60;
            }
            else
            {
                number += (int)MousePosi_x;
            }*/


        }
        else if (Input.GetMouseButtonUp(0))
        {
            measurement = false;
            startMousePosition = 0;
            lastMousePosition = Input.mousePosition.x;
            //ダイアルをクリックした時の座標と離した時の差分を角度回す
            Debug.Log(MousePosi_x);
        }
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            if (hit.collider.gameObject.tag == "dial"&&measurement==false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startMousePosition = Input.mousePosition.x;
                    measurement = true;
                }

            }
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);

        }
    }
}
