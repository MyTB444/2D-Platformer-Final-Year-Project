using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_animations : MonoBehaviour
{
    private Animator _anim;
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();

    }
    public void StartWalking()
    {
        _anim.SetBool("Walking", true);
    }
    public void StopWalking()
    {
        _anim.SetBool("Walking", false);
    }
    public void DeadAnimation()
    {
        _anim.SetTrigger("Dead");
    }
}
