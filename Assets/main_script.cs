using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using System.IO;


public class main_script : MonoBehaviour
{
    public AudioClip Sound1;
    public AudioSource audioSource;
    public GameObject dummy_safe;
    public GameObject clear_safe;
    public Text nownuber;
    public Text Timer;
    public Text Password;
    public Text ranking;
    int number;
    int passtimes;
    int dummyonthewayMousePosition;
    float stoptime;
    float secounds;
    int pass;
    private Camera mainCamera;
    private float startMousePosition;
    private float lastMousePosition;
    private float onthewayMousePosition;
    private bool timer_measurement;
    private bool measurement;
    private bool delay;
    [SerializeField] InputField inputArea;
    public Button button;
    public class PlayerData
    {
        public string myName;
        public float[] rankingscores = new float[6];
        public float fast;
        public float second;
        public float third;
        public float fourth;
        public float fiveth;
    }

    PlayerData myData = new PlayerData();
    public void SavePlayerData()
    {
        StreamWriter writer;
        myData.myName = inputArea.text;
        string jsonstr = JsonUtility.ToJson(myData);
        writer = new StreamWriter(Application.dataPath + "/playersavedata.json", false);//「/」に今回でいう「C:\Users\shido\OneDrive\ドキュメント\GitHub\セーブ＆ロード実験」の中の「playersavedata.json」を指定する
        writer.Write(jsonstr);
        writer.Flush();
        writer.Close();
        inputArea.text = "";
    }
    public void LoadPlayerData()
    {
        string datastr = "";
        var playerName = inputArea.text;
        StreamReader reader;

        reader = new StreamReader(Application.dataPath + "/playersavedata.json");
        datastr = reader.ReadToEnd();
        reader.Close();
        myData.myName = JsonUtility.FromJson<string>(datastr); // ロードしたデータで上書き
        Debug.Log(myData.myName + "のデータをロードしました");
    }
    void Start()
    {
        ranking.text = "1位：" + myData.rankingscores[0].ToString("f2") + "\n" + "2位：" + myData.rankingscores[1].ToString("f2") + "\n" + "3位：" + myData.rankingscores[2].ToString("f2") + "\n" + "4位：" + myData.rankingscores[3].ToString("f2") + "\n" + "5位：" + myData.rankingscores[4].ToString("f2");
        timer_measurement = false;
        pass = 0;
        ranking.gameObject.SetActive(true);
        inputArea.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        nownuber.gameObject.SetActive(false);
        Timer.gameObject.SetActive(false);
        Password.gameObject.SetActive(false);
        mainCamera = Camera.main;
        pass = Random.Range(1, 60);
        myData = new PlayerData();
        
    }
    public void setplay()
    {
        Password.gameObject.SetActive(true);
        ranking.gameObject.SetActive(false);
        inputArea.gameObject.SetActive(false);
        button.gameObject.SetActive(false);
        nownuber.gameObject.SetActive(true);
        Timer.gameObject.SetActive(true);
        myData.myName = inputArea.text;
        secounds = 0;
        secounds = PlayerPrefs.GetFloat("ReoundingTime", 0);
        number = 0;
        passtimes = 5;
        pass = Random.Range(1, 60);
        Password.text = (pass).ToString();
        //Ranking.gameObject.SetActive(false);
        timer_measurement = true;

    }
    void gameclear()
    {
        timer_measurement = false;
        ranking.gameObject.SetActive(true);
        inputArea.gameObject.SetActive(true);
        button.gameObject.SetActive(true);
        myData.rankingscores[5] = secounds;
        ranking.text = "1位：" + myData.rankingscores[0].ToString("f2") + "\n" + "2位：" + myData.rankingscores[1].ToString("f2") + "\n" + "3位：" + myData.rankingscores[2].ToString("f2") + "\n" + "4位：" + myData.rankingscores[3].ToString("f2") + "\n" + "5位：" + myData.rankingscores[4].ToString("f2");
        GameObject.Instantiate(clear_safe);

    }

    // Update is called once per frame
    void Update()
    {
        if(timer_measurement == true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            nownuber.text = number.ToString();
            secounds += Time.deltaTime;
            Debug.Log(secounds);
            Timer.text = (secounds/60).ToString("f0") + "m" + (secounds%60).ToString("f2") + "s";
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
                    if (passtimes == 0)
                    {
                        GetComponent<main_script>().gameclear();
                    }else
                    {
                        pass = Random.Range(1, 60);
                        Password.text = (pass).ToString();
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
}