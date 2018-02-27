using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int hp = 1;
    public GameObject explosion;

    //트리거와 충돌되면 자동으로 호출되는 함수 
    private void OnTriggerEnter2D(Collider2D other)
    {
        //충돌한대상이 "Bullet"이라는 태그 이름인지 
        if(other.tag == "Bullet")
        {
            Damage(1);
        }
    }
    public void Damage(int value)
    {
        hp -= value;
        if(hp <= 0)
        {
            GameManager.instance.SetGameOver();
            GameObject temp = (GameObject) Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(temp, 2.0f);
            Destroy(gameObject);        //gameObject는 자기 자신, GameObject는 객체 
        }
    }
}
