using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed;
    public int Damage;

    private Transform target;
    public GameObject DamagedEffect;
    public float TargetBetweenDistance;
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }

        transform.LookAt(target.position);//转向敌人所在的position

        transform.Translate(Vector3.forward * Speed * Time.deltaTime);//发射

        Vector3 dir = target.position - transform.position;//用敌人-炮台自身的位置得到的位移

        if (dir.magnitude < TargetBetweenDistance)//得到的位移小于设定的话就辩定为攻击，触发特效
        {
            target.GetComponent<Enemy>().TakeDamaged(Damage);
            Die();
        }


    }
    void Die()
    {
        GameObject effect = GameObject.Instantiate(DamagedEffect, transform.position, transform.rotation);
        Destroy(effect, 1f);
        Destroy(this.gameObject);
    }

}
