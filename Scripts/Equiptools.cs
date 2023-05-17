using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equiptools : Equip {
    public float attackRate;
    public bool attacking;
    public float attackDistance;
    public bool doesGatherResources;
    public bool doesdealDamage;
    public int damage;

    private Animator itemAnim;
    private Camera cam;

    private void Awake(){
        itemAnim = GetComponent<Animator>();
        cam = Camera.main;
    }
    public override void OnAttackInput(){
        attacking = true;
        itemAnim.SetTrigger("Attack");
        Invoke("OnCanAttack", attackRate);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            itemAnim.SetTrigger("Attack");
        }
    }
    void OnCanAttack(){
        attacking = true;
    }
    public void OnHit(){
        Debug.Log("Hit");
    }
}
