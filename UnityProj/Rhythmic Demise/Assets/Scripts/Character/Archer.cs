﻿using UnityEngine;
using System.Collections;

public class Archer : Character {

    public GameObject arrow;

	// Use this for initialization
	protected new void Start () {
        base.Start();

        isAttacking = false;
        job = Enums.JobType.Archer;
        
        //1 is archer index
        currentHealth = maxHealth = PlayerScript.playerdata.troopData[1].maxHealth;
        damage = PlayerScript.playerdata.troopData[1].damage;
        armor = PlayerScript.playerdata.troopData[1].armor;
        skill = PlayerScript.playerdata.skillSelected;

        if (skill == Enums.SkillName.ArcherAtkBuff)
        {
            ArmyController.armyController.initSkillBonus(1); // 1 for archer
        }
	}
	
	// Update is called once per frame
	protected new void Update () {
        base.Update();
	}

    public override void attack()
    {
        if (!isAttacking)
        {
            if (ArmyController.armyController.enemyList.Count > 0)
            {
                isAttacking = true;

                //trigger attack animation
                anim.SetTrigger("Attack");
            }
            else
            {
                
            }
        }
    }

    public override void useSkill()
    {
        if (Time.time >= nextSkillTime)
        {
            if (skill == Enums.SkillName.ArcherHigh)
            {
                nextSkillTime = Time.time + PlayerScript.playerdata.troopData[0].skills[1].skillCooldown;

                if (!isAttacking)
                {
                    if (ArmyController.armyController.enemyList.Count > 0)
                    {
                        isAttacking = true;

                        anim.SetTrigger("SkillHigh");
                    }
                    else
                    {
                        
                    }
                }
            }
            else if (skill == Enums.SkillName.ArcherAOE)
            {
                nextSkillTime = Time.time + PlayerScript.playerdata.troopData[0].skills[0].skillCooldown;

                if (!isAttacking)
                {
                    if (ArmyController.armyController.enemyList.Count > 0)
                    {
                        isAttacking = true;

                        anim.SetTrigger("SkillAOE");
                    }
                    else
                    {
                        
                    }
                }
            }
        }
    }

    void spawnArrow()
    {
        if (ArmyController.armyController.closestEnemy != null)
        {
            Vector3 dir = ArmyController.armyController.closestEnemy.transform.position - this.transform.position;
            float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

            GameObject shoot = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
            shoot.SendMessage("Initialize", ArmyController.armyController.closestEnemy.transform.position);
            shoot.SendMessage("initDamage", damage);
        }
    }

    void skillAOE()
    {
        Vector3 dir = ArmyController.armyController.closestEnemy.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject shoot1 = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        GameObject shoot2 = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        GameObject shoot3 = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        GameObject shoot4 = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        GameObject shoot5 = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        shoot1.SendMessage("Initialize", ArmyController.armyController.closestEnemy.transform.position + new Vector3(1, 1, 0));
        shoot2.SendMessage("Initialize", ArmyController.armyController.closestEnemy.transform.position + new Vector3(1, -1, 0));
        shoot3.SendMessage("Initialize", ArmyController.armyController.closestEnemy.transform.position + new Vector3(0, 0, 0));
        shoot4.SendMessage("Initialize", ArmyController.armyController.closestEnemy.transform.position + new Vector3(-1, -1, 0));
        shoot5.SendMessage("Initialize", ArmyController.armyController.closestEnemy.transform.position + new Vector3(-1, 1, 0));
        shoot1.SendMessage("initDamage", damage);
        shoot2.SendMessage("initDamage", damage);
        shoot3.SendMessage("initDamage", damage);
        shoot4.SendMessage("initDamage", damage);
        shoot5.SendMessage("initDamage", damage);
    }

    public override void defend()
    {
        if (!isDefending)
        {
            isDefending = true;

            armor *= 2;

            //anim.SetTrigger("Defend");
        }
    }
}
