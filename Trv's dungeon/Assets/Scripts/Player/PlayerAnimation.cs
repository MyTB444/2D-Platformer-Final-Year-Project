using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _anim;
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    public void RunAnim(float move)
    {
        _anim.SetFloat("Running", Mathf.Abs(move));
    }
    public void JumpAnim()
    {
        _anim.SetTrigger("Jump");
    }
    public void SwingRightAnim()
    {
        _anim.SetTrigger("Swing");
    }
    public void SwingLeftAnim()
    {
        _anim.SetTrigger("Swingleft");
    }

    public void RollAnim()
    {
        _anim.SetTrigger("Roll");
    }
}
