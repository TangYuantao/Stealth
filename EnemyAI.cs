using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {

	/// <summary>
	/// 巡逻点数组
	/// </summary>
	public Transform[] wayPoints;
	/// <summary>
	/// 追捕速度
	/// </summary>
	public float chashingSpeed = 5f;
	/// <summary>
	/// 巡逻速度
	/// </summary>
	public float patrollingSpeed = 2f;
	/// <summary>
	/// 剩余距离偏移量
	/// </summary>
	public float remainingOffset = 0.5f;
	/// <summary>
	/// 机器人等待时间
	/// </summary>
	public float waitingTime = 3f;
	/// <summary>
	/// 玩家
	/// </summary>
	private Transform player;
	/// <summary>
	/// 玩家血量
	/// </summary>
	private PlayerHealth playerHealth;
	/// <summary>
	/// 玩家视觉脚本
	/// </summary>
	private EnemySight enemySight;
	/// <summary>
	/// 公共警报脚本
	/// </summary>
	private LastPlayerSighting lastPlayerSighting;
	/// <summary>
	/// 导航组件
	/// </summary>
	private NavMeshAgent nav;
	/// <summary>
	/// 计时器
	/// </summary>
	private float timer;
	/// <summary>
	/// 索引号
	/// </summary>
	private int index;

	void Start()
	{
		player = GameObject.FindWithTag (Tags.Player).transform;
		playerHealth = player.GetComponent<PlayerHealth> ();
		enemySight = GetComponent<EnemySight> ();
		lastPlayerSighting = GameObject.FindWithTag (Tags.GameController).
			GetComponent<LastPlayerSighting> ();
		nav = GetComponent<NavMeshAgent> ();
	}

	void Update()
	{
		//看到玩家，且玩家活着
		if (enemySight.playerInSight && playerHealth.health > 0) {
			//射击
			Shooting();
		} 
		//是否有一个警报位置
		else if (enemySight.personalAlarmPosition != lastPlayerSighting.normalPosition) {
			//追捕
			Chashing ();
		} else {
			//巡逻
			Patrolling();
		}
	}
	/// <summary>
	/// 射击
	/// </summary>
	void Shooting()
	{
		//停止导航
		nav.isStopped = true;
	}
	/// <summary>
	/// 追捕
	/// </summary>
	void Chashing()
	{
		//恢复导航
		nav.isStopped = false;
		//设置追捕速度
		nav.speed = chashingSpeed;
		//设置导航目标
		nav.SetDestination (enemySight.personalAlarmPosition);
		//达到的目标
		if (nav.remainingDistance < nav.stoppingDistance + remainingOffset) {
			//开始计时
			timer += Time.deltaTime;
			//等待时间结束
			if (timer >= waitingTime) {
				//警报解除
				lastPlayerSighting.alarmPosition = lastPlayerSighting.normalPosition;
				//解除机器人警报
				enemySight.personalAlarmPosition = lastPlayerSighting.normalPosition;
				//重启计时器
				timer = 0;
			}
		} else {
			//重启计时器
			timer = 0;
		}
	}
	/// <summary>
	/// 巡逻
	/// </summary>
	void Patrolling()
	{
		//恢复导航
		nav.isStopped = false;
		//设置导航速度
		nav.speed = patrollingSpeed;
		//设置导航目标
		nav.SetDestination (wayPoints [index].position);
		//如果到达了目标
		if (nav.remainingDistance < nav.stoppingDistance + remainingOffset) {
			//开始计时
			timer += Time.deltaTime;
			//计时结束
			if (timer >= waitingTime) {
				//切换到下一个巡逻点
				index = ++index % wayPoints.Length;
				//重启计时器
				timer = 0;
			}
		}
	}
}