using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;         //자신의 객체 참조변수 
	public float m_generateTerm = 1.5f;         //총알 생성 텀 
	public float[] levels;                      //시간에 따른 레벨 조정 변수
	public int[] bulletCount;                   //총알의 개수 

	int index = 0;                              //현재 levels의 수
	float m_currentTime;                        //현재 진행중인 시간
	bool m_gameover = false;                    //게임오버 상태인지 진행중인지 확인하는 변수

	void Awake(){
		if (instance) {
			Debug.Log ("다수의 인스턴스가 실행되고 있습니다");
		}
		instance = this;
	}

    //game시작 
	void Start () {
		m_currentTime = 0;
		m_gameover = false;
        //코루틴 사용, 실행 하지 않을때 중지했다가 다시 시작할때 멈춤 곳부터 실행 가능
		StartCoroutine ("CreateBullet");
	}

	void Update () {
		m_currentTime += Time.deltaTime;
        //설정한 시간이 지나면 levels을 up시킨다 
		if (m_currentTime > levels [index] && (index < levels.Length - 1)) {
			index++;
		}
	}

	IEnumerator CreateBullet()
    {
		while (!m_gameover) {
			yield return new WaitForSeconds(m_generateTerm);
			for(int i = 0; i < bulletCount[index]; i++){
				BulletManager.instance.CreateBullet();
			}
		}
	}
    public void SetGameOver()
    {
        m_gameover = true;

    }
}
