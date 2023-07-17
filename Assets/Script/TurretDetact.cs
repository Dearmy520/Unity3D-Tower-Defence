using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretDetact : MonoBehaviour
{
    private float AttackDuration = 1;
    private float AttackTimer;
    public GameObject bulletPrefab;
    public Transform bulletPosition;
    public Transform head;
    public List<GameObject> enemys = new List<GameObject>();
    public float LayserAttackRate;
    public bool isUseLayser;
    public LineRenderer Layser;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }

    private void Start()
    {
        AttackTimer = AttackDuration;
    }
    private void Update()
    {
        AttackCondition();

    }


    void AttackCondition()
    {
        //让炮台指向敌人
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPostion = enemys[0].transform.position;
            targetPostion.y = head.position.y;
            head.LookAt(targetPostion);
        }
        if (!isUseLayser)
        {

            AttackTimer += Time.deltaTime;
            if (enemys.Count > 0 && AttackTimer >= AttackDuration)
            {
                AttackTimer =0;
                Attack();
            }
        }
        else if (enemys.Count > 0)
        {//Layser Attack Form
            if (Layser.enabled == false) Layser.enabled = true;
            if (enemys[0] == null) UpdateEnemys();
            if (enemys.Count > 0)
            {
                Layser.SetPositions(new Vector3[] { bulletPosition.position, enemys[0].transform.position });//射激光
                enemys[0].GetComponent<Enemy>().TakeDamaged(LayserAttackRate*Time.deltaTime);

            }
        }
        else
        {
            Layser.enabled = false;
        }

    }
    void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        if (enemys.Count > 0)
        {
            GameObject bullet = GameObject.Instantiate(bulletPrefab, bulletPosition.position, bulletPosition.rotation);
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);//把敌人队列中第一个的位置传过去给bullet脚本
        }
        else
        {
            AttackTimer = AttackDuration;
        }

    }

    void UpdateEnemys()
    {
        List<int> EmptyIndex = new List<int>();

        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == null)
            {
                EmptyIndex.Add(i);
            }
        }

        for (int i = 0; i < EmptyIndex.Count; i++)
        {
            enemys.RemoveAt(EmptyIndex[i] - i);
        }
    }

}
