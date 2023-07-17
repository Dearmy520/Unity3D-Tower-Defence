using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Wave[] waves;
    public int Size;
    public GameObject StartPos;
    [SerializeField] float WaveDuration;
    public static int WaveEnemyCount;
    Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = StartCoroutine(Enemyspawner());

    }

    public void Stop()
    {
        StopCoroutine(coroutine);
    }

    IEnumerator Enemyspawner()
    {
        foreach (Wave wave in waves)
        {
            for (int i = 0; i < wave.Count; i++)
            {
                WaveEnemyCount++;

                GameObject.Instantiate(wave.EnemyPrefebs, StartPos.transform.position, Quaternion.identity);

                if (i != wave.Count - 1) yield return new WaitForSeconds(wave.Rate);
            }

            while (WaveEnemyCount > 0) yield return 0;//如果场上存在敌人的时候，则将不会出一个种类敌人

            yield return new WaitForSeconds(WaveDuration);
        }
        while (WaveEnemyCount > 0) yield return 0;
        GameManager.instance.WinGame();
    }
}
