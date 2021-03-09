using UnityEngine;
public class Ship : Bullet {
     [SerializeField] private Bullet bullet, mine;
     [SerializeField] private TMPro.TMP_Text healthText, instructions, opponentScore;
     [SerializeField] private float angle;
     [SerializeField] private GameObject star;
     [SerializeField] private Transform[] flames;
     private Vector2 vector;
     void Start() {
          Reset();
          for (var i = 0; i < 50; i++) 
               Instantiate(star, new Vector3(UnityEngine.Random.Range(-640, 641), UnityEngine.Random.Range(-360, 361), 90), Quaternion.identity).GetComponent<SpriteRenderer>().color = new Color(UnityEngine.Random.Range(.5f,1), 1, 1);
     }
     protected override void FixedUpdate() {
          transform.position = new Vector2((transform.position.x + 1280 + 640) % 1280 - 640, (transform.position.y + 720 + 360) % 720 - 360);
          angle += vector.x * -3;
          foreach (var trans in flames)
               trans.rotation = Quaternion.Euler(new Vector3(0,0,angle));
          GetComponent<Rigidbody2D>().AddForce(240 * accel * transform.right);
          healthText.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 60, 0);
          if (firing > 0 && (int) ++elapsed % 10 == 0)
               MakeBullet(bullet, transform.position, (Vector2)(transform.right.normalized * 310) + GetComponent<Rigidbody2D>().velocity);
     }
     protected void OnCollisionEnter2D(Collision2D col) => Explode(explosion);
     protected override void Explode(ParticleSystem[] explode) {
          gameNotOver = false;
          healthText.text = string.Empty;
          base.Explode(explode);
          StartCoroutine(ResetAll());
     }
     protected override void OnTriggerEnter2D(Collider2D col) {
          if (gameNotOver && col.gameObject.GetComponent<Bullet>() is Bullet bullet && bullet.playerIndex != playerIndex) Hit();
     }
     private void Hit() {
          healthText.text = healthText.text.Substring(1);
          opponentScore.text = healthText.text == string.Empty ? (System.Convert.ToInt32(opponentScore.text) + 1).ToString() : opponentScore.text;
          if (healthText.text == string.Empty) 
               Explode(explosion);
     } 
     public void MakeBullet(Bullet bulPrefab, Vector3 pos, Vector2 velocity) {
          if (healthText.text == string.Empty) return;
          var bul = Instantiate(bulPrefab, pos, Quaternion.identity, transform.parent);
          bul.gameObject.SetActive(true);
          bul.GetComponent<Rigidbody2D>().velocity = velocity;
          if (!gameNotOver) bul.fade.Play();
     }
     System.Collections.IEnumerator ResetAll() {
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
     public void OnFire(UnityEngine.InputSystem.InputValue input) => firing = input.Get<float>();
     public void OnBomb(UnityEngine.InputSystem.InputValue input)=> MakeBullet(mine, mine.transform.position,2 * new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized);
     public void OnTurn(UnityEngine.InputSystem.InputValue input) => vector = input.Get<Vector2>();
     public void OnAccel(UnityEngine.InputSystem.InputValue input) {
          instructions.gameObject.SetActive(false);
          accel = input.Get<float>();
          foreach (var system in bigExplosion)
               if (accel > 0 && healthText.text != string.Empty) system.Play(false);
               else system.Stop();
     }
}