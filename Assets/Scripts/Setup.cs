using UnityEngine;
public class Setup : MonoBehaviour
{
    [SerializeField] private GameObject star;
    void Start() {
        for (var i = 0; i < 100; i++)
            Instantiate(star, new Vector3(Random.Range(-640, 641), Random.Range(-360, 361), 0), Quaternion.identity).GetComponent<SpriteRenderer>().color = new Color(Random.Range(.5f,1), 1, 1);
    }
}