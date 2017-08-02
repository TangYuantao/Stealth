using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float playerBodyOffset = 0.5f;
    public float moveSpeed = 2;
    /// <summary>
    /// 相机旋转速度
    /// </summary>
    public float turnSpeed = 2;

    /// <summary>
    /// 俯视视角偏移
    /// </summary>
    public float offset = .5f;
    /// <summary>
    /// 档位
    /// </summary>
    public int gears = 5;
    /// <summary>
    /// 跟随目标
    /// </summary>
    public Transform followTarget;
    /// <summary>
    /// 方向向量
    /// </summary>
    private Vector3 dir;
    /// <summary>
    /// 待选观察点
    /// </summary>
    private Vector3[] readyPosition;
    /// <summary>
    /// 射线碰撞检测器
    /// </summary>
    private RaycastHit hit;

    void Start()
    {
        //获取方向向量
        dir = transform.position - followTarget.position;
        //实例化
        readyPosition = new Vector3[gears];
    }

    void Update()
    {
        //最佳观察视角
        Vector3 begin = dir + followTarget.position;
        //最差观察视角(俯视)
        Vector3 end = followTarget.position + Vector3.up * (dir.magnitude-offset);

        readyPosition[0] = begin;
        readyPosition[readyPosition.Length - 1] = end;
        //获取中间的点
        for (int i=1;i<readyPosition.Length;i++)
        {
            //求中间各点的坐标，比例i/q-1;
            readyPosition[i] = Vector3.Lerp(begin,end,(float)i/(readyPosition.Length-1)) ;
        }
        //备选方案
        Vector3 watchPoint = begin;
        //挑选所有点
        for (int i=0;i<readyPosition.Length;i++)
        {
            if (CheckWatchPoint(readyPosition[i]))
            {
                //设置观察点
                watchPoint = readyPosition[i];
                //跳出
                break;
            }
        }
        //平滑移动到目标
        transform.position = Vector3.Lerp(transform.position,watchPoint,Time.deltaTime*moveSpeed);
        //看向目标
        //transform.LookAt(followTarget);
        //
        Vector3 lookDir = followTarget.position - watchPoint;

        Quaternion lookQua = Quaternion.LookRotation(lookDir);
        transform.rotation = Quaternion.Lerp(transform.rotation,lookQua,Time.deltaTime*turnSpeed);
        //固定相机
       // transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 0);

    }

    /// <summary>
    /// 检测观察点是否可以看到目标
    /// </summary>
    /// <param name="point">待选的点</param>
    /// <returns>true 开得到 false 看</returns>
    bool CheckWatchPoint(Vector3 point)
    {
        //调试
        Debug.DrawLine(point, followTarget.position, Color.red);
        if (Physics.Raycast(point,followTarget.position+Vector3.up*playerBodyOffset-point,out hit))
        {
            if (hit.collider.CompareTag(Tags.Player))
            {
                return true;
            }
        }
        return false;
    }






}
