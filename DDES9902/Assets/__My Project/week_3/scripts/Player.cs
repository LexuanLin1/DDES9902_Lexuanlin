using UnityEngine;

public class Player: MonoBehaviour
{

    public Rigidbody rd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Debug.Log("Game beginning");
        //rd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Game is running");
        //rd.AddForce( new Vector3(2,0,0) );

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        //Debug.Log(h);
        rd.AddForce(new Vector3(h, 0, v)*2);
    }
}

