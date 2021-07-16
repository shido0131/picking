using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class safe_script : MonoBehaviour
{

    bool anima;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        anima = false;
    }
    // Update is called once per frame
    void Update()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("safe", anima);
        
    }
}
