using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BulletManager : MonoBehaviour {
	public static BulletManager instance;           //자신의 객체(인스턴스)
	public GameObject BulletPrefab;                 //생성한 총알 prefab
	public Transform m_Player;                      //플레이어의 위치값
	public int maxBullet = 100;                     //최대 총알 수

	List<BulletMove> BulletList;                    //생성된 총알을 저장할 배열 변수
	Vector2 m_LeftBottom;                           //화면의 왼쪽 좌표 저장
	Vector2 m_RightTop;                             //화면의 오른쪽 좌표 저장

    //스태틱은 선언과 동시에 메모리 올라감
    //스태틱이여서 인스턴스가 하나여야하지만 두개이상이면 안된다.
    //자기 자신을 바로 참조하개 하여 외부에서 참조할수 있게 한다(싱글톤)
	void Awake(){
		if (instance) {
			Debug.Log ("다중인스턴스 실행중입니다. 주의하세요");
		}
		instance = this;
	}
	
	void Start () {
		m_LeftBottom = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0));
		m_RightTop = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, 0));
		BulletList = new List<BulletMove> ();
		for(int i = 0; i < maxBullet; i++){
			GameObject temp = (GameObject) Instantiate(BulletPrefab, new Vector3(0,m_RightTop.y + 2, 0), Quaternion.identity);
			//총알을 생성하고
			BulletList.Add(temp.GetComponent<BulletMove>());
			//해당 총알의 BulletMove 스크립트를 리스트에 넣는다.
		}
	}

	public void CreateBullet(){
        //플레이어가 사라지면 실행x
        if (!m_Player)
        {
            return;
        }
		Vector2 pos = GetRandomPosition (); //랜덤한 위치를 받아와서
		Vector2 direction = (Vector2)m_Player.position - pos; //플레이어 에게 향하도록 방향 조절 

		BulletMove selectedBullet = BulletList.Find (o => o.m_isUsed == false);
		//현재 미사용중인 총알을 찾아서
		if (!selectedBullet) {
			Debug.Log ("화면에 생성가능한 최대 총알수를 초과했습니다!, 최대 총알수를 늘려주세요");
		} else {
			//방향과 속도를 설정해준다
			selectedBullet.SetDirection(direction.normalized);
			selectedBullet.SetPosition(pos);
			selectedBullet.m_isUsed = true;
		}
	}

	Vector2 GetRandomPosition(){
		int caseNum = Random.Range (0, 4); //0~3까지의 랜덤한 숫자를 만들어냄.
		Vector2 pos = Vector2.zero;
		switch (caseNum) {
		case 0: //좌측
			pos.x = m_LeftBottom.x -1;
			pos.y = Random.Range(m_LeftBottom.y, m_RightTop.y);
			break;
		case 1: //우측
			pos.x = m_RightTop.x +1;
			pos.y = Random.Range(m_LeftBottom.y, m_RightTop.y);
			break;
		case 2: //상단
			pos.x = Random.Range (m_LeftBottom.x, m_RightTop.x);
			pos.y = m_RightTop.y +1;
			break;
		case 3: //하단
			pos.x = Random.Range (m_LeftBottom.x, m_RightTop.x);
			pos.y = m_LeftBottom.y -1;
			break;
		}
		return pos;
	}
	
	public bool IsInScreen(Vector2 target){
		if (target.x > m_LeftBottom.x && target.x < m_RightTop.x && target.y > m_LeftBottom.y && target.y < m_RightTop.y) {
			return true;
		} else {
			return false;
		}
	}
}
