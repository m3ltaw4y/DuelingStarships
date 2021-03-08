using System;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    public int playerIndex;
    protected float elapsed;
    protected virtual void Reset() => Destroy(gameObject);
    protected virtual void FixedUpdate()
    {
        transform.position = new Vector2((Math.Abs(transform.position.x + 1240 + 640)) % 1240 - 640, Math.Abs((transform.position.y + 720 + 360)) % 720 - 360);//screen wrap
        elapsed += Time.deltaTime;
        if (elapsed > 2) Reset();
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.GetComponent<Ship>() is Ship otherShip && otherShip.playerIndex != playerIndex) Reset();
    }
}
