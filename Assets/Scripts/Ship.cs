using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : Bullet
{
     [SerializeField] private Bullet bullet;
     [SerializeField] private TextMeshPro healthText;
     [SerializeField] private GameObject instructions;
     [SerializeField] private TextMeshProUGUI opponentScore;
     [SerializeField] private float angle;
     private float accel;
     private float firing;
     private Vector2 vector;
     private void Awake() => Reset();

     protected override void FixedUpdate()
     {
          elapsed += 1;
          transform.position = new Vector2((Math.Abs(transform.position.x + 1240 + 640)) % 1240 - 640, Math.Abs((transform.position.y + 720 + 360)) % 720 - 360);//screen wrap
          angle += vector.x * -3;
          transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
          GetComponent<Rigidbody2D>().AddForce(240 * accel * transform.right);
          
          healthText.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 60, 0);
          if (firing > 0 && (int) elapsed % 10 == 0)
          {
               var bul = Instantiate(bullet, transform.position, Quaternion.identity, transform.parent);
               bul.gameObject.SetActive(true);
               bul.GetComponent<Rigidbody2D>().velocity = transform.right.normalized * 310;
          }
     }
     
     protected void OnCollisionEnter2D(Collision2D col)
     {
          Debug.Log("OnCollisionEnter2D");
          if (col.otherRigidbody.GetComponent<Ship>() is Ship otherShip)
               Reset();
     }

     protected override void OnTriggerEnter2D(Collider2D col)
     {
          if (col.gameObject.GetComponent<Bullet>() is Bullet bullet && bullet.playerIndex != playerIndex)
               Hit();
     }
     
     private void Hit()
     {
          healthText.text = healthText.text.Substring(1);
          if (healthText.text == string.Empty)
          {
               opponentScore.text = (Convert.ToInt32(opponentScore.text) + 1).ToString();
               Reset();
          }
     }
     
     protected override void Reset()
     {
          instructions.SetActive(true);
          GetComponent<Rigidbody2D>().velocity = Vector3.zero;
          healthText.text = ".....";
          angle = playerIndex == 0 ? 0 : 180; 
          transform.SetPositionAndRotation(new Vector2(playerIndex == 0 ? -400 : 400, UnityEngine.Random.Range(-250, 250)), Quaternion.Euler(new Vector3(0,0,angle)));
     }

     public void OnFire(InputValue input) => firing = input.Get<float>();
     public void OnBomb(InputValue input) => Debug.Log("Bomb!");
     public void OnTurn(InputValue input) => vector = input.Get<Vector2>();
     public void OnAccel(InputValue input)
     {
          instructions.SetActive(false);
          accel = input.Get<float>();
     }
}
