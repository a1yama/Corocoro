using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {

	Rigidbody2D rb;
	public int moveSpeed = 2;
    public LayerMask groundLayer; //地面のレイヤー
    float jumpForce = 300; //ジャンプ力
    bool isGrounded; //着地しているかの判定 
	float h = 0;

	void Start () {
		//GetComponentの処理をキャッシュしておく
		rb = GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D(Collision2D coll) {
		isGrounded = true;
	}
	void OnCollisionExit2D(Collision2D coll) {
		isGrounded = false;
	}
    void Update() {
        //isGrounded=true且つJumpボタンを押した時Jumpメソッド実行
#if UNITY_EDITOR
        if (isGrounded && Input.GetButtonDown("Jump")) {
            Jump();
        }
#else
		if (isGrounded && Input.touchCount >= 1) {
			Jump();
		}
#endif
    }
	void FixedUpdate () {
#if UNITY_EDITOR
		//左右キーの入力
		h = Input.GetAxis("Horizontal");
#else
		h = Input.acceleration.x;
#endif

		if (h < -0.1) {
			transform.Rotate (new Vector3 (0f, 0f, 10f));
		} else if (h > 0.1) {
			transform.Rotate (new Vector3 (0f, 0f, -10f));
		}
		//velocity: 速度
		//X方向へmoveSpeed分移動させる
		rb.velocity = new Vector2(h * 10, rb.velocity.y);
	}
	void Jump (){
			//上方向へ力を加える
			rb.AddForce (Vector2.up * jumpForce);
			//地面から離れるのでfalseにする
			isGrounded = false;
	}
}
