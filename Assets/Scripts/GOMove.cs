using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 挂在每个Cube上，用于寻路
public class GOMove : MonoBehaviour {

    public Transform[] target;
    public float speed = 50; //  寻路速度 

	// Use this for initialization
	void Start () {
        transform.position = new Vector3(Random.Range(-9, 9), 0.5f, Random.Range(-9, 9));
        StartCoroutine(MoveToPath());   // 启动协程
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    private IEnumerator MoveToPath()
    {
        while(true)
        {
            for(int i = 0; i < target.Length; i++)
            {
                // 嵌套协程，依次向寻路点寻路
                yield return StartCoroutine(MoveToTarget(target[i].position));
            }
        }
    }

    IEnumerator MoveToTarget(Vector3 target)
    {
        while((transform.position - target).magnitude > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();  // 每帧执行一次
        }
    }
}
