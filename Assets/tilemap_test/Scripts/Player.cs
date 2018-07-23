using UnityEngine;

// Tilemapのデモ用のコードです。
// 実際のゲームを想定したコード・構成にはなっていないので注意してください。
// 詳しくは、README.mdを見てください。
[RequireComponent(typeof(Collider), typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField] float maxSpeed = 10.0F;
    [SerializeField] float speedFactor = 5.0F;

    Rigidbody2D rigidbody2d;
    SpriteRenderer spriteRenderer;
    Animator animator;

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var coin = collision.GetComponent<Coin>();
        if (coin != null)
        {
            coin.Collect();
        }
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var direction = new Vector3(horizontal, vertical, 0.0F);
        var speed = Mathf.Min(direction.magnitude * speedFactor, maxSpeed);
        rigidbody2d.velocity = speed * direction.normalized;

        if (direction.magnitude > 0.0F)
        {
            if (direction.x != 0)
            {
                spriteRenderer.flipX = direction.x > 0;
            }
            animator.speed = 2.0F;
            animator.Play("Walk");
        }
        else
        {
            animator.Play("Default");
        }
    }
}
