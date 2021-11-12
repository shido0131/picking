﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using System.IO;


public class main_script : MonoBehaviour
{
    float Volume;
    public AudioClip Sound1;
    public AudioSource audioSource;
    public GameObject dummy_safe;
    public GameObject clear_safe;
    public Text nownuber;
    public Text Timer;
    public Text Ranking;
    int number;
    int passtimes;
    int dummyonthewayMousePosition;
    float stoptime;
    float secounds;
    float secound;
    float minutes;
    int pass;
    private Camera mainCamera;
    private float startMousePosition;
    private float lastMousePosition;
    private float onthewayMousePosition;
    private bool timer_measurement;
    private bool measurement;
    private bool delay;
    private PlayerData myData;
    [SerializeField] Text counterText;
    [SerializeField] InputField inputArea;
    [System.Serializable]
    public class PlayerData
    {
        float fast;
        float second;
        float third;
        float fourth;
        float fiveth;

        

         public string playerscore;
    }
    
    void Start()
    {
        mainCamera = Camera.main;
        GetComponent<main_script>().setplay();
        pass = Random.Range(1, 60);
        Ranking.gameObject.SetActive(false);
        myData = new PlayerData();
    }
    void setplay()
    {
        secound = 0;
        secound = PlayerPrefs.GetFloat("ReoundingTime", 0);
        minutes = 0;
        secounds = 0;//score用の時間を計測
        number = 0;
        passtimes = 5;
        pass = Random.Range(1, 60);
        nownuber.gameObject.SetActive(true);
        Timer.gameObject.SetActive(true);
        //Ranking.gameObject.SetActive(false);
        timer_measurement = true;

    }
    void gameclear()
    {
        /*GameObject.Instantiate(clear_safe);
        if (secound < fast||fast==0)
        {
            fast = secound;
        }else if (secound < second||second == 0)
        {
            second = secound;
        }
        else if (secound < third||third == 0)
        {
            third = secound;
        }
        else if (secound < fourth||fourth == 0)
        {
            fourth = secound;
        }
        else if (secound < fiveth||fiveth == 0)
        {
            fiveth = secound;
        }
        timer_measurement = false;*///ランキングのスコアを算出

    }

    // Update is called once per frame
    void Update()
    {
        if(timer_measurement == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            nownuber.text = number.ToString();
            /*secounds += Time.deltaTime;
            secound += Time.deltaTime;
            Timer.text = minutes.ToString("f0") + "m" + secounds.ToString("f2") + "s";
            if (secounds >= 60)
            {
                secounds -= 60;
                minutes += 1;
            }*/
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
            if (Input.GetMouseButton(0))
            {
                lastMousePosition = Input.mousePosition.x;
                onthewayMousePosition = (lastMousePosition - startMousePosition) / 10;//ダイアルをクリックした時の座標と離した時の差分
                onthewayMousePosition = Mathf.Floor(onthewayMousePosition);
                nownuber.text = ((int)onthewayMousePosition).ToString();
                nownuber.color = new Color(0.9922f, 0.4941f, 0f, 1f);
            }
            if (Input.GetMouseButtonUp(0) && measurement == true && delay == false)
            {
                measurement = false;
                number += (int)onthewayMousePosition;
                if (number > 60)
                {
                    number %= 60;
                }
                if (number < 0)
                {
                    number *= -1;
                }
                if (number == 0)
                {
                    number = 60;
                }
                dummyonthewayMousePosition = (int)onthewayMousePosition;
                if (dummyonthewayMousePosition < 0)
                {
                    dummyonthewayMousePosition *= -1;
                }
                StartCoroutine("Dealay");
                //transform.Rotate(0f,0f,onthewayMousePosition*6);
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
    public IEnumerator Dealay()
    {
        delay = true;
        for (int i = 0; i < dummyonthewayMousePosition; ++i)
        {
            audioSource.PlayOneShot(Sound1);
            if (onthewayMousePosition < 0)
            {
                transform.Rotate(0f, 0f, -6f);
            }
            else
            {
                transform.Rotate(0f, 0f, 6f);
            }
            
            yield return new WaitForSeconds(0.1f/onthewayMousePosition);
        }
        delay = false;
    }
    public void SavePlayerData()
    {
        StreamWriter writer;
        var playerscore = inputArea.text;
        myData.playerscore = playerscore;
        string jsonstr = JsonUtility.ToJson(myData);
        writer = new StreamWriter(Application.dataPath + "/save" + playerscore + ".json", false);
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
    }
    public void LoadPlayerScoreData()
    {
        string datestr = "";
        var playerscore = inputArea.text;
        StringReader reader;
        reader = new StringReader(Application.dataPath + "/save" + playerscore + ".json");
        datestr = reader.ReadToEnd();
        reader.Close();
        myData = JsonUtility.FromJson<PlayerData>(datestr);//ロードしたデータの上書き
        Debug.Log(myData + playerscore + "のデータをロードしました。");
        counterText.text = myData.playerscore.ToString();
    }
}

