using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Ship : MonoBehaviour
{
     [SerializeField] private int playerIndex;
     [SerializeField] private float angle;
     private Vector2 inputVec;
     private float accel;
     private void Awake()
     {
          transform.position = new Vector2(playerIndex == 0 ? -400 : 400, UnityEngine.Random.Range(-250, 250));
     }

     private void FixedUpdate()
     {
          transform.position = new Vector2((Math.Abs(transform.position.x + 1240 + 640)) % 1240 - 640, Math.Abs((transform.position.y + 720 + 360)) % 720 - 360);//screen wrap
          angle += inputVec.x * -3;
          transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
          GetComponent<Rigidbody2D>().AddForce(240 * accel * transform.right);
     }

     private void Update()
     {
          //if (Input.GetButtonDown("Fire1"))
          //     Instantiate(projectile, transform.position, transform.rotation);
          //if (Input.GetButtonDown(accel))
          //     GetComponent<Rigidbody2D>().AddForce(new Vector2(0.1f, 0.1f));
     }
     
     
     public void OnFire()
     {
          Debug.Log("Fire!");
     }
    
     public void OnBomb()
     {
          Debug.Log("Bomb!");
     }
    
     public void OnTurn(InputValue input)
     {
          inputVec = input.Get<Vector2>();
     }

     public void OnAccel(InputValue input)
     {
          accel = input.Get<float>();
          Debug.Log("Accel!");
     }
}
