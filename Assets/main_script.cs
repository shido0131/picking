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
    int soundlevel;
    int number;
    int difference;
    int passtimes;
    int dummyonthewayMousePosition;
    public float stoptime;
    public int pass;
    private Camera mainCamera;
    private float startMousePosition;
    private float lastMousePosition;
    private float onthewayMousePosition;
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
        if (Physics.Raycast(ray, out hit, 10.0f))
        {
            if (hit.collider.gameObject.tag == "dial" && measurement == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startMousePosition = Input.mousePosition.x;
                    measurement = true;
                }

            }
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.red, 5);

        }
        if (Input.GetMouseButton(0)&&measurement==true)
        {
            lastMousePosition = Input.mousePosition.x;
            onthewayMousePosition = (lastMousePosition - startMousePosition) / 10;//ダイアルをクリックした時の座標と離した時の差分
            onthewayMousePosition = Mathf.Floor(onthewayMousePosition);
            nownuber.text = ((int)onthewayMousePosition).ToString();
            nownuber.color = new Color(0.9922f, 0.4941f, 0f, 1f);
        }
        if (Input.GetMouseButtonUp(0) && measurement == true)
        {
            measurement = false;
            number += (int)onthewayMousePosition;
            if ( number > 60)
            {
                number %= 60;
            }
            if (number < 0)
            {
                number *= -1;
            }
            if (number == 0)
            {
                number =60;
            }
            dummyonthewayMousePosition = (int)onthewayMousePosition;
            if (dummyonthewayMousePosition < 0)
            {
                dummyonthewayMousePosition *= -1;
            }
            for (int i=0;i<dummyonthewayMousePosition;++i)
            {
                
                transform.Rotate(0f,0f,6f);
            }
            transform.Rotate(0f,0f,onthewayMousePosition*6);
            nownuber.text = (number).ToString();
            nownuber.color = new Color(1f, 0.92f, 0.016f, 1f);
            //ダイアルをクリックした時の座標と離した時の差分を角度回す●
            //Debug.Log(MousePosi_x);
        }
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
    }
}
