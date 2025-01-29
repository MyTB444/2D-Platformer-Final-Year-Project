using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    //Simple animation handler for the player.
    private SpriteRenderer _sprite;
    private Animator[] _anim;
    void Start()
    {
        _anim = GetComponentsInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    public void RunAnim(float move)
    {
        _anim[0].SetFloat("Running", Mathf.Abs(move));
    }
    public void JumpAnim()
    {
        _anim[0].SetTrigger("Jump");
    }
    public void SwingRightAnim()
    {
        _anim[0].SetTrigger("Swing");
    }
    public void SwingLeftAnim()
    {
        _anim[0].SetTrigger("Swingleft");
    }
    public void PickUpLeft()
    {
        _anim[0].SetTrigger("Pickupl");
    }
    public void PickUpRight()
    {
        _anim[0].SetTrigger("Pickupr");
    }
    public void RollAnim()
    {
        _anim[0].SetTrigger("Roll");
    }
    public void ClimbAnimStart()
    {
        _anim[0].SetInteger("Climb", 1);
    }
    public void ClimbAnimStop()
    {
        _anim[0].SetInteger("Climb", -1);
    }
    public void DeathAnim()
    {
        _anim[0].SetTrigger("Dead");
    }
    public void Damaged()
    {
        StartCoroutine(Reding());
    }
    public void BlueRight()
    {
        _anim[1].SetTrigger("Right");
    }
    public void BlueLeft()
    {
        _anim[1].SetTrigger("Left");
    }
    //Manually created animation for changing color when damage is taken.
    private IEnumerator Reding()
    {
        _sprite.color = Color.red;
        yield return new WaitForSeconds(0.4f);
        _sprite.color = Color.white;
    }
}
