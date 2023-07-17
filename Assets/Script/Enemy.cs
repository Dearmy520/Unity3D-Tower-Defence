using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour
{
    private Transform[] wp;//Waypoint脚本的变量
    [SerializeField] private float Speed;//敌人移动速度
    int index = 0;
    public GameObject EndPos;
    public GameObject DestoryEffect;
    public float Health;
    public Slider HealthSlider;
    private float TotalHP;
    private void Start()
    {
        wp = WayPoint.Waypoint;

        TotalHP = Health;
    }

    // Update is called once per frame
    void Update()
    {

        Move();

    }

    void Move()
    {
        if (index >wp.Length-1) return;
        transform.Translate((wp[index].transform.position - transform.position).normalized * Time.deltaTime * Speed);

        if (Vector3.Distance(wp[index].position, transform.position) < 0.2f)
        {//当路程点与敌人之间的距离重合时则把index++
            index++;
        }
        if (index > wp.Length-1) ReachEndPos();
    }

    void ReachEndPos()
    {

        GameObject.Destroy(this.gameObject);
        GameManager.instance.LoseGame();

    }

    private void OnDestroy()
    {
        EnemySpawner.WaveEnemyCount--;
    }


    public void TakeDamaged(float Damage)
    {

        if (Health <=0) return;

        Health -= Damage;
        HealthSlider.value = (float)Health / TotalHP;
        if (Health <=0) Die();
    }

    void Die()
    {
        GameObject effect = GameObject.Instantiate(DestoryEffect, transform.position, transform.rotation);
        Destroy(effect, 1f);
        Destroy(this.gameObject);
    }
}
