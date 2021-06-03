using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpwaner : MonoBehaviour
{
	//生成间隔
	public float spawnTime = 2f; 
	//生成延时
	public float spawnDelay = 1f;       
	//敌人数组
	public GameObject[] enemies;       

	void Start()
	{
		//延迟spawnDelay之后每隔spawnTime执行一次Spawn
		InvokeRepeating("Spawn", spawnDelay, spawnTime);
	}


	void Spawn()
	{
		int enemyIndex = Random.Range(0, enemies.Length);
		Instantiate(enemies[enemyIndex], transform.position, transform.rotation);
		//foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
		//{
		//	p.Play();
		//}
	}
}
