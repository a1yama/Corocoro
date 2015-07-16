using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour {
	public Text text;

	Rigidbody2D rb;
    public LayerMask groundLayer; //地面のレイヤー
    float jumpForce = 300; //ジャンプ力
    bool isGrounded; //着地しているかの判定 
	float h = 0;
    public GameObject mainCamera;

	void Start () {
		//GetComponentの処理をキャッシュしておく
		rb = GetComponent<Rigidbody2D>();
	}

	void OnCollisionEnter2D(Collision2D coll) {
        isGrounded = true;
	}
	void OnCollisionExit2D(Collision2D coll) {
//        isGrounded = false;
	}
	void FixedUpdate () {

#if UNITY_EDITOR
        if (isGrounded && Input.GetButtonDown("Jump")) {
            Jump();
        }
        //左右キーの入力
		h = Input.GetAxis("Horizontal");
#else
		if (isGrounded && Input.touchCount >= 1) {
			Jump();
		}
		h = Input.acceleration.x;
#endif
		text.text = isGrounded.ToString ();
        if (h < -0.02) {
			transform.Rotate (new Vector3 (0f, 0f, 10f));
		} else if (h > 0.02) {
			transform.Rotate (new Vector3 (0f, 0f, -10f));
		}
		//velocity: 速度
		rb.velocity = new Vector2(h * 10, rb.velocity.y);

        if (transform.position.x > -8) {
            Vector3 cameraPos = mainCamera.transform.position;
            cameraPos.x = transform.position.x + 4;
            mainCamera.transform.position = cameraPos;
        }
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        Vector2 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, min.x + 0.5f, max.x);
        transform.position = pos;

	}
	void Jump (){
			//上方向へ力を加える
			rb.AddForce (Vector2.up * jumpForce);
			//地面から離れるのでfalseにする
			isGrounded = false;
	}
}
