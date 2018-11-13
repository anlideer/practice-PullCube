using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float xSpeed = 0;
    public float zSpeed = 0;
    public float maxSpeed = 5;
    public GameObject foot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!GetComponent<ReleaseSkill>().pullStar)
        {
            if (Input.GetKey(KeyCode.A) && transform.position.x > -9)   // 具体边界是多少看造的世界的大小了
            {
                // 调控速度
                if (xSpeed > -maxSpeed)
                    xSpeed -= Time.deltaTime * 10;
                else if (xSpeed < -maxSpeed)
                    xSpeed = -maxSpeed;
            }
            else if (Input.GetKey(KeyCode.D) && transform.position.x < 9)
            {
                if (xSpeed < maxSpeed)
                    xSpeed += Time.deltaTime * 10;
                else if (xSpeed > maxSpeed)
                    xSpeed = maxSpeed;
            }
            else    // 超出边界
                xSpeed = 0;

            if (Input.GetKey(KeyCode.S) && transform.position.z > -9)
            {
                if (zSpeed > -maxSpeed)
                    zSpeed -= Time.deltaTime * 10;
                else if (zSpeed < -maxSpeed)
                    zSpeed = -maxSpeed;
            }
            else if (Input.GetKey(KeyCode.W) && transform.position.z < 9)
            {
                if (zSpeed < maxSpeed)
                    zSpeed += Time.deltaTime * 10;
                else if (zSpeed > maxSpeed)
                    zSpeed = maxSpeed;
            }
            else
                zSpeed = 0;

            transform.Translate(Time.deltaTime * xSpeed, 0, Time.deltaTime * zSpeed);
            foot.transform.Rotate(zSpeed * 2, 0, -xSpeed * 2, Space.World);
        }
	}
}
