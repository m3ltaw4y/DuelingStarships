using UnityEngine;
public class Mine : Bullet {
    public override int LifeTime => 10;
    public void Awake() => GetComponent<Rigidbody2D>().velocity = 2 * new Vector2(Random.Range(-1f,1f),Random.Range(-1f,1f)).normalized;
}
