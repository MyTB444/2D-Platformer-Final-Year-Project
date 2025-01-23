using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //Simple animation handler for the player.
    private SpriteRenderer _sprite;
    private Animator _anim;
    void Start()
    {
        _anim = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
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
    public void PickUpLeft()
    {
        _anim.SetTrigger("Pickupl");
    }
    public void PickUpRight()
    {
        _anim.SetTrigger("Pickupr");
    }
    public void RollAnim()
    {
        _anim.SetTrigger("Roll");
    }
    public void ClimbAnimStart()
    {
        _anim.SetInteger("Climb", 1);
    }
    public void ClimbAnimStop()
    {
        _anim.SetInteger("Climb", -1);
    }
    public void DeathAnim()
    {
        _anim.SetTrigger("Dead");
    }
    public void Damaged()
    {
        StartCoroutine(Reding());
    }
    //Manually created animation for changing color when damage is taken.
    private IEnumerator Reding()
    {
        _sprite.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        _sprite.color = Color.white;
    }
}
