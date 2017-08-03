using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour {

	/// <summary>
	/// 死角空间
	/// </summary>
	public float deadZone = 4f;
	/// <summary>
	/// 动画组件
	/// </summary>
	private Animator ani;
	/// <summary>
	/// 导航组件
	/// </summary>
	private NavMeshAgent nav;
	/// <summary>
	/// 玩家
	/// </summary>
	private Transform player;

	void Start()
	{
		ani = GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();
		player = GameObject.FindWithTag (Tags.Player).transform;
	}

	void Update()
	{
		//速度=期望速度在机器人前方的投影向量的模
		float speed = Vector3.Project (nav.desiredVelocity, transform.forward).magnitude;
		//角速度大小=期望速度与机器人前方的夹角
		float angularSpeed = Vector3.Angle(nav.desiredVelocity,transform.forward);
		//求真正的角速度（有大小有方向）
		angularSpeed = FindAngle (angularSpeed);
		//如果没有导航目标，让机器人Idle
		if (nav.desiredVelocity == Vector3.zero) {
			angularSpeed = 0;
		}
		//设置到动画参数
		ani.SetFloat (HashIDs.Speed, speed);
		ani.SetFloat (HashIDs.AngularSpeed, angularSpeed);
	}

	void OnAnimatorMove()
	{
		nav.velocity = ani.deltaPosition / Time.deltaTime;
		transform.rotation = ani.rootRotation;
	}

	/// <summary>
	/// 计算弧度.
	/// </summary>
	/// <returns>弧度.</returns>
	/// <param name="angular">角度.</param>
	float FindAngle(float angular)
	{
		//进入死角
		if (angular <= deadZone) {
			//直接看向目标
			transform.LookAt (transform.position + nav.desiredVelocity);
			//停止转身动画
			return 0;
		}
		//求法向量
		Vector3 normal = Vector3.Cross (transform.forward, nav.desiredVelocity);
		//目标在左侧
		if (normal.y < 0) {
			//角度取反
			angular = -angular;
		}
		//角度转弧度
		angular *= Mathf.Deg2Rad;
		//返回
		return angular;
	}
}