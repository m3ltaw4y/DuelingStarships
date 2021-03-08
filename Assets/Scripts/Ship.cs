using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class Ship : Bullet {
     [SerializeField] private Bullet bullet;
     [SerializeField] private Mine mine;
     [SerializeField] private TextMeshPro healthText;
     [SerializeField] private GameObject instructions;
     [SerializeField] private TextMeshProUGUI opponentScore;
     [SerializeField] private float angle;
     private float accel;
     private float firing;
     private Vector2 vector;
     private void Awake() => Reset();
     protected override void FixedUpdate() {
          elapsed += 1;
          transform.position = new Vector2((transform.position.x + 1240 + 640) % 1240 - 640, (transform.position.y + 720 + 360) % 720 - 360);//screen wrap
          angle += vector.x * -3;
          transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
          GetComponent<Rigidbody2D>().AddForce(240 * accel * transform.right);
          healthText.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 60, 0);
          if (firing > 0 && (int) elapsed % 10 == 0)
               MakeBullet(bullet, transform.position).GetComponent<Rigidbody2D>().velocity = (Vector2)(transform.right.normalized * 310) + GetComponent<Rigidbody2D>().velocity;
     }
     protected void OnCollisionEnter2D(Collision2D col) => ResetAll();
     protected override void OnTriggerEnter2D(Collider2D col) {
          if (col.gameObject.GetComponent<Bullet>() is Bullet bullet && bullet.playerIndex != playerIndex)
               Hit();
     }
     private void Hit() {
          healthText.text = healthText.text.Substring(1);
          if (healthText.text == string.Empty) {
               opponentScore.text = (Convert.ToInt32(opponentScore.text) + 1).ToString();
               ResetAll();
          }
     } 
     public Bullet MakeBullet(Bullet bulPrefab, Vector3 pos) { 
          var bul = Instantiate(bulPrefab, pos, Quaternion.identity, transform.parent);
          bul.gameObject.SetActive(true);
          return bul;
     }
     void ResetAll() {
          foreach (var bul in FindObjectsOfType<Bullet>())
               bul.Reset();
     }
     public override void Reset() {
          instructions.SetActive(true);
          GetComponent<Rigidbody2D>().velocity = Vector3.zero;
          healthText.text = ".....";
          angle = playerIndex == 0 ? 0 : 180; 
          transform.SetPositionAndRotation(new Vector2(playerIndex == 0 ? -400 : 400, UnityEngine.Random.Range(-250, 250)), Quaternion.Euler(new Vector3(0,0,angle)));
     }
     public void OnFire(InputValue input) => firing = input.Get<float>();
     public void OnBomb(InputValue input) => MakeBullet(mine, mine.transform.position);
     public void OnTurn(InputValue input) => vector = input.Get<Vector2>();
     public void OnAccel(InputValue input) {
          instructions.SetActive(false);
          accel = input.Get<float>();
          if (accel > 0) GetComponentInChildren<ParticleSystem>().Play();
          else GetComponentInChildren<ParticleSystem>().Stop();
     }
}
