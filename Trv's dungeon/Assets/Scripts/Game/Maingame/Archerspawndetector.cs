using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Arenaspawndetector : MonoBehaviour
{
    private int phase1count = 0;
    private int phase2count = 0;
    private Spawn_man sm;
    private enum phase
    {
        nophase,
        phase1,
        phase2,
    }
    private phase currentphase = phase.nophase;
    void Start()
    {
        sm = GetComponentInParent<Spawn_man>();
    }
    void Update()
    {
        PhaseInitiate();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && phase1count == 0)
        {
            currentphase = phase.phase1;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            phase2count++;
            if (phase2count == 2)
            {
                currentphase = phase.phase2;
            }
        }
    }
    private void PhaseInitiate()
    {
        if (currentphase == phase.phase1 && phase1count == 0)
        {
            phase1count++;
            sm.SpawnBossArcher(transform.position.x + 7.5f, transform.position.y + 3);
            sm.SpawnBossArcher(transform.position.x - 7, transform.position.y + 3);
        }
        else if (currentphase == phase.phase2 && phase2count == 2)
        {
            phase2count++;
            sm.SpawnBossArcher(transform.position.x + 7.5f, transform.position.y + 3);
            sm.SpawnBossArcher(transform.position.x - 7, transform.position.y + 3);
            sm.SpawnMiniBoss(transform.position.x, transform.position.y + 3);

        }
    }
}
