using UnityEngine;
using Ilumisoft.HealthSystem;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;

    protected virtual void Start()
    {
        FindPlayer();
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.isGameOver) return;

        if (player == null)
        {
            FindPlayer();
            if (player == null) return;
        }

        Vector3 dir = (player.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    void FindPlayer()
    {
        if (player != null) return;
        var po = GameObject.FindWithTag("Player");
        if (po != null) player = po.transform;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var hc = collision.gameObject.GetComponent<HealthComponent>();
            if (hc != null)
                hc.ApplyDamage(1);
        }
    }
}
