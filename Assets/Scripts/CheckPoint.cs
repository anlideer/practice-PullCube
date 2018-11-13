using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour {
    public GameObject[] point;  // 所有的灯
    public GameObject panel1, panel2;

    private void Awake()
    {
        Time.timeScale = 1; // 程序运行时设定系统事件为正常
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (AllStar(point))
        {
            panel1.SetActive(true);
            Time.timeScale = 0;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!panel2.activeInHierarchy && !panel1.activeInHierarchy) // 如果两个panel都还没有启用
            {
                panel2.SetActive(true);
                Time.timeScale = 0;
            }
        }
	}

    bool AllStar(GameObject[] point)
    {
        for (int i = 0; i < point.Length; i++)
        {
            if (!point[i].GetComponent<StreetLamp>().lightStar)
                return false;
        }
        return true;
    }

    public void Continue()
    {
        panel2.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    
    public void SignOut()
    {
        Application.Quit();
    }
}
