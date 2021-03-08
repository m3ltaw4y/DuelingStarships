using UnityEngine;

public class Setup : MonoBehaviour
{
    [SerializeField] private GameObject star;
    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < 100; i++)
        {
            var newStar = Instantiate(star, new Vector3(Random.Range(-640, 641), Random.Range(-360, 361), 0), Quaternion.identity);
            newStar.GetComponent<SpriteRenderer>().color = new Color(Random.Range(.5f,1), 1, 1);
        }
    }
}

//Screen.SetResolution(1280, 720, true);
//Camera.main.aspect = 1280 / 720f;
//Camera.main.pixelRect = new Rect(0,0,1280,720);
//GetComponent<Camera>().pixelRect = new Rect(0,0,1280,720);