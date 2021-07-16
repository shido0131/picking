using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main_script : MonoBehaviour
{
    private float speed = 7;
    private int number;
    private Vector3 setTapPos;
    private Vector3 setMouthPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            setTapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        // ボタンを押しているとき
        if (Input.GetMouseButton(0))
        {
            setTapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            setTapPos.z = 10;
            transform.position = Vector3.Lerp(transform.position, setTapPos, speed * Time.deltaTime);
            Debug.Log(setTapPos.z);
        }
        /*if (Input.GetMouseButton(0))
        {
            setTapPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }*/
    }
}
