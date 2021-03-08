﻿using UnityEngine;
public class Bullet : MonoBehaviour {
    protected static bool gameNotOver = true;
    [SerializeField] protected ParticleSystem explosion, bigExplosion;
    [SerializeField] public Animation fade;
    [SerializeField] public int lifeTime;
    public int playerIndex;
    protected float elapsed;
    public virtual void Reset() => Destroy(gameObject);
    protected virtual void FixedUpdate() {
        transform.position = new Vector2((transform.position.x + 1240 + 640) % 1240 - 640, (transform.position.y + 720 + 360) % 720 - 360);//screen wrap
        elapsed += Time.deltaTime;
        if (GetComponent<SpriteRenderer>().enabled && elapsed > lifeTime) Explode(explosion);
    }
    protected virtual void OnTriggerEnter2D(Collider2D col) {
        if (gameNotOver && col.gameObject.GetComponent<Ship>() is Ship otherShip && otherShip.playerIndex != playerIndex) Explode(bigExplosion);
    }
    protected virtual void Explode(ParticleSystem explode) {
        GetComponent<SpriteRenderer>().enabled = GetComponent<Rigidbody2D>().simulated = false;
        explode.Play(false);
    }
}