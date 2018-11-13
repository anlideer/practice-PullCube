using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyRotate : MonoBehaviour {

    public Ray ray;
    private RaycastHit hit;
    public LayerMask layerName;
    public GameObject body;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!GetComponent<ReleaseSkill>().pullStar)
            body.transform.LookAt(InputMove());
	}

    private Vector3 InputMove() // 获取鼠标在世界中的移动
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);//镜头射出一条射线,跟随鼠标位置        
        if (Physics.Raycast(ray, out hit, 100, layerName))  // 照射层只照地面
            return new Vector3(hit.point.x, hit.point.y + body.transform.position.y, hit.point.z);
        else
            return new Vector3(0, 0, 0);
    }
}
