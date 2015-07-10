using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	Rigidbody2D rb;
	public int moveSpeed = 2;
    public LayerMask groundLayer; //地面のレイヤー
    float jumpForce = 300; //ジャンプ力
    bool isGrounded; //着地しているかの判定 

	void Start () {
		//GetComponentの処理をキャッシュしておく
		rb = GetComponent<Rigidbody2D>();
	}

    void Update() {
        transform.Rotate(new Vector3(0f, 0f, 10f));
        rb.velocity = new Vector2(transform.localScale.x * moveSpeed, rb.velocity.y);

        //接地判定用のラインを引く
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 1,transform.position - transform.up * 0.1f,groundLayer); //Linecastが判定するレイヤー

        //isGrounded=true且つJumpボタンを押した時Jumpメソッド実行
        if (isGrounded && Input.GetButtonDown("Jump")) {
            Jump();
        }
    }
	void FixedUpdate () {
		//velocity: 速度
		//X方向へmoveSpeed分移動させる
		rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
	}
	void Jump (){
			//上方向へ力を加える
			rb.AddForce (Vector2.up * jumpForce);
			//地面から離れるのでfalseにする
			isGrounded = false;
	}
}
