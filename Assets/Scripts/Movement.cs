using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float velocity = 10;

    private Rigidbody2D rigidbody;
  
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

 
    void FixedUpdate()
    {
        //rigidbody.linearVelocity = new Vector2(velocity, rigidbody.linearVelocity.y);
        transform.Translate(new Vector3(velocity, 0, 0) * Time.fixedDeltaTime);
    }
}
