using UnityEngine;
public class Bullet : MonoBehaviour {
    public int playerIndex;
    protected float elapsed;
    public virtual void Reset() => Destroy(gameObject);
    public virtual int LifeTime => 2;
    protected virtual void FixedUpdate() {
        transform.position = new Vector2((transform.position.x + 1240 + 640) % 1240 - 640, (transform.position.y + 720 + 360) % 720 - 360);//screen wrap
        elapsed += Time.deltaTime;
        if (elapsed > LifeTime) Expode();
        if (elapsed > LifeTime + 0.1666667f) Reset();
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Ship>() is Ship otherShip && otherShip.playerIndex != playerIndex) elapsed = LifeTime;
    }
    protected virtual void Expode()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<ParticleSystem>().Play();
    }
}
