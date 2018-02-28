using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
	public float m_speed = 7f;                              //플레이어 움직이는 속도
	Vector2 move;                                           //움직이는 방향 
	Rigidbody2D m_rigid;                                    //rigid를 제어하는 변수  
	float screenTop, screenBottom, screenLeft, screenRight; //화면의 상,하,좌,우 좌표값 저장

	void Awake(){
		m_rigid = GetComponent<Rigidbody2D> ();
	}

	void Start(){
		screenTop = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, 0)).y;
		screenBottom = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0)).y;
		screenLeft = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, 0)).x;
		screenRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, 0)).x;
		//Debug.Log ( screenTop +"," + screenBottom +"," + screenLeft+"," + screenRight);
	}

	void Update () {
		CheckInput (); //사용자 입력감지
		CheckOutOfScreen (); //플레이어의 화면이탈 방지
	}

	void FixedUpdate(){
        //rigid의 방향 제어 
		m_rigid.velocity = move * m_speed;
	}

	void CheckInput(){
        //input을 통해 입력한 키의 좌표 가져오기 
        //raw있을 시 = -1, 0, 1 
        //raw없을 시 = -1  ~  1
		float xInput = Input.GetAxisRaw ("Horizontal");
		float yInput = Input.GetAxisRaw ("Vertical");
		
		move = new Vector2 (xInput, yInput).normalized; 
		//벡터의 정규화normalized를 통해 길이 1짜리 벡터 생성
	}

	void CheckOutOfScreen(){
		float nextX = Mathf.Clamp (transform.position.x, screenLeft, screenRight);
		float nextY = Mathf.Clamp (transform.position.y, screenBottom, screenTop);

		transform.position = new Vector3 (nextX, nextY, transform.position.z);
	}
}
