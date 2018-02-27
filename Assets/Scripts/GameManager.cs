using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
	public static GameManager instance;
	public float m_generateTerm = 1.5f;
	public float[] levels;
	public int[] bulletCount;

	int index = 0;
	float m_currentTime;
	bool m_gameover = false;

	void Awake(){
		if (instance) {
			Debug.Log ("다수의 인스턴스가 실행되고 있습니다");
		}
		instance = this;
	}

	void Start () {
		m_currentTime = 0;
		m_gameover = false;
		StartCoroutine ("CreateBullet");
	}

	void Update () {
		m_currentTime += Time.deltaTime;
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
