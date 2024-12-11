using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_animations : MonoBehaviour
{
    //Simple animation handler for enemies.
    private SpriteRenderer _sprite;
    private Animator _anim;
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }
    public void StartWalking()
    {
        _anim.SetBool("Walking", true);
    }
    public void StopWalking()
    {
        _anim.SetBool("Walking", false);
    }
    public void WalkTrigger()
    {
        _anim.SetTrigger("Walk");
    }
    public void DeadAnimation()
    {
        _anim.SetTrigger("Dead");
    }
    public void AttackRight()
    {
        _anim.SetTrigger("Attackright");
    }
    public void AttackLeft()
    {
        _anim.SetTrigger("Attackleft");
    }
    public void Damaged()
    {
        StartCoroutine(Reding());
    }
    private IEnumerator Reding()
    {
        _sprite.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        _sprite.color = Color.white;

    }
}
