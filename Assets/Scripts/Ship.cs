using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
public class Ship : Bullet {
     [SerializeField] private Bullet bullet, mine;
     [SerializeField] private TextMeshPro healthText;
     [SerializeField] private TextMeshProUGUI instructions, opponentScore;
     [SerializeField] private float angle;
     [SerializeField] private GameObject star;
     private float accel, firing;
     private Vector2 vector;
     void Start() {
          Reset();
          for (var i = 0; i < 50; i++) 
               Instantiate(star, new Vector3(UnityEngine.Random.Range(-640, 641), UnityEngine.Random.Range(-360, 361), 0), Quaternion.identity).GetComponent<SpriteRenderer>().color = new Color(UnityEngine.Random.Range(.5f,1), 1, 1);
     }
     protected override void FixedUpdate() {
          elapsed += 1;
          transform.position = new Vector2((transform.position.x + 1240 + 640) % 1240 - 640, (transform.position.y + 720 + 360) % 720 - 360);
          angle += vector.x * -3;
          transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
          GetComponent<Rigidbody2D>().AddForce(240 * accel * transform.right);
          healthText.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 60, 0);
          if (healthText.text != string.Empty && firing > 0 && (int) elapsed % 10 == 0)
               MakeBullet(bullet, transform.position).GetComponent<Rigidbody2D>().velocity = (Vector2)(transform.right.normalized * 310) + GetComponent<Rigidbody2D>().velocity;
     }
     protected void OnCollisionEnter2D(Collision2D col) => Explode(explosion);
     protected override void Explode(ParticleSystem explode) {
          gameNotOver = false;
          healthText.text = string.Empty;
          bigExplosion.Stop();
          base.Explode(explode);
          StartCoroutine(ResetAll());
     }
     protected override void OnTriggerEnter2D(Collider2D col) {
          if (gameNotOver && col.gameObject.GetComponent<Bullet>() is Bullet bullet && bullet.playerIndex != playerIndex) Hit();
     }
     private void Hit() {
          healthText.text = healthText.text.Substring(1);
          if (healthText.text == string.Empty) {
               opponentScore.text = (Convert.ToInt32(opponentScore.text) + 1).ToString();
               Explode(explosion);
          }
     } 
     public Bullet MakeBullet(Bullet bulPrefab, Vector3 pos) { 
          var bul = Instantiate(bulPrefab, pos, Quaternion.identity, transform.parent);
          bul.gameObject.SetActive(true);
          if (!gameNotOver) bul.fade.Play();
          return bul;
     }
     IEnumerator ResetAll() {
          foreach (var bul in FindObjectsOfType<Bullet>()) bul.fade.Play();
          yield return new WaitForSeconds(2);
          foreach (var bul in FindObjectsOfType<Bullet>()) bul.Reset();
     }
     public override void Reset() {
          fade.Play("FadeIn");
          GetComponent<SpriteRenderer>().enabled = GetComponent<Rigidbody2D>().simulated = gameNotOver = true;
          instructions.gameObject.SetActive(true);
          GetComponent<Rigidbody2D>().velocity = Vector3.zero;
          healthText.text = ".....";
          angle = playerIndex == 0 ? 0 : 180; 
          transform.SetPositionAndRotation(new Vector2(playerIndex == 0 ? -400 : 400, UnityEngine.Random.Range(-250, 250)), Quaternion.Euler(new Vector3(0,0,angle)));
     }
     public void OnFire(InputValue input) => firing = input.Get<float>();
     public void OnBomb(InputValue input) {
          if (healthText.text != string.Empty)
               MakeBullet(mine, mine.transform.position).GetComponent<Rigidbody2D>().velocity = 2 * new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
     }
     public void OnTurn(InputValue input) => vector = input.Get<Vector2>();
     public void OnAccel(InputValue input) {
          instructions.gameObject.SetActive(false);
          accel = input.Get<float>();
          if (accel > 0) bigExplosion.Play();
          else bigExplosion.Stop();
     }
}