using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReleaseSkill : MonoBehaviour {

    public GameObject doubleHand;
    public float pullSpeed = 0; // 拉伸速度
    public float maxPull = 9;  // 最大拉伸距离
    Vector3 handPos;    //双手初始位置
    public bool pullStar = false;   // 技能是否正在释放
    bool holdGO = false;    // 是否拿着东西
    private RaycastHit hit; // 射线照射到的点
    public LayerMask mask;  // 射线能打到的层
    Vector3 hitPos; // 照射点坐标
    Vector3 originScale;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!pullStar && !holdGO)   // 没有放技能也没有拿着东西
        {
            if (Input.GetMouseButtonDown(0))    // 鼠标左键放技能
            {
                Release();
            }
        }
        else if (holdGO)    // 手里拿着东西
        {
            if (Input.GetMouseButtonDown(1))    // 鼠标右键放下CUBE
            {
                hit.transform.SetParent(null);  // 解除父子关系
                holdGO = false;
            }
        }
        Pull();
        KeepPos(hit.transform);

    }

    // 手掌的运动轨迹
    private void HandTrail(float trailTime)
    {
        Transform[] a = doubleHand.GetComponentsInChildren<Transform>();
        for (int i = 1; i < a.Length; i++)
        {
            if (a[i].GetComponent<TrailRenderer>() != null)
            {
                a[i].GetComponent<TrailRenderer>().time = trailTime;
            }
        }
    }

    void Release()  //释放技能
    {
        HandTrail(1);   // 显示轨迹
        handPos = doubleHand.transform.position;
        pullSpeed = 50; 
        pullStar = true;
        // 发射
        if (Physics.Raycast(handPos, doubleHand.transform.forward, out hit, maxPull, mask))
        {
            hitPos = hit.point; // 获取照射点坐标（如果打到东西了的话
        }

    }

    void Pull() //拉伸
    {
        if (pullStar)
        {
            doubleHand.transform.Translate(0, 0, Time.deltaTime * pullSpeed);
            // 没打到东西，伸到最长距离就回来
            if (hit.transform == null && (doubleHand.transform.position - handPos).magnitude > maxPull)
            {
                HandTrail(0);
                pullSpeed = -50;
            }
            // 照到东西了，抓住它返回
            else if (hit.transform != null && (doubleHand.transform.position - hitPos).magnitude < 0.5f)
            {
                // 抓住！(设为双手子物体)
                // 加了一大段是防止cube变形...
                originScale.x = hit.transform.lossyScale.x / doubleHand.transform.lossyScale.x;
                originScale.y = hit.transform.lossyScale.y / doubleHand.transform.lossyScale.y;
                originScale.z = hit.transform.lossyScale.z / doubleHand.transform.lossyScale.z;
                hit.transform.parent = doubleHand.transform;
                hit.transform.localScale = originScale;
                holdGO = true;
                HandTrail(0);
                pullSpeed = -50;    //双手向后移动
            }
            // 复位
            if (pullSpeed < 0 && (doubleHand.transform.position - handPos).magnitude < 0.5f)
            {
                pullSpeed = 0;
                doubleHand.transform.position = handPos;
                pullStar = false;
            }

        }
    }

    // 拿稳
    void KeepPos(Transform Cube)
    {
        if (Cube != null && Cube.parent != null)    // 判断是否是被抓住的状态
        {
            // 固定位置和旋转
            Cube.position = doubleHand.transform.position;
            Cube.rotation = doubleHand.transform.rotation;
        }
    }
}
