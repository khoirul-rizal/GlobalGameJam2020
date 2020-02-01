using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    Vector2 velocity;
    BoxCollider2D boxCollider;

    float speed = 5;
    float walkAcceleration = 10;
    float groundDeceleration = 5;
    void Start()
    {
      // player = gameObject.GetComponent<GameObject>();
      boxCollider = player.GetComponent<BoxCollider2D>();
    }
    void Update()
    {
        CharacterMovement();
        CharacterInteraction();
    }
   
    void CharacterMovement()
    {
      float moveInputX = Input.GetAxisRaw("Horizontal");
      velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInputX, walkAcceleration * Time.deltaTime);

      if (moveInputX != 0)
      {
        velocity.x = Mathf.MoveTowards(velocity.x, speed * moveInputX, walkAcceleration * Time.deltaTime);
      }
      else
      {
          velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
      }

      
      float moveInputY = Input.GetAxisRaw("Vertical");
      velocity.y = Mathf.MoveTowards(velocity.y, speed * moveInputY, walkAcceleration * Time.deltaTime);

      if (moveInputY != 0)
      {
        velocity.y = Mathf.MoveTowards(velocity.y, speed * moveInputY, walkAcceleration * Time.deltaTime);
      }
      else
      {
          velocity.y = Mathf.MoveTowards(velocity.y, 0, groundDeceleration * Time.deltaTime);
      }


      player.transform.Translate(velocity * Time.deltaTime);

      Collider2D[] hits = Physics2D.OverlapBoxAll(player.transform.position, boxCollider.size+boxCollider.offset , 0);
      foreach (Collider2D hit in hits)
      {
        if (hit.isTrigger) return;
        if (hit == boxCollider)
          continue;

        ColliderDistance2D colliderDistance = hit.Distance(boxCollider);

        if(colliderDistance.isOverlapped)
        {
          transform.Translate(colliderDistance.pointA - colliderDistance.pointB);
        }
      }
    }
    void CharacterInteraction()
    {
      if(Input.GetButton("Interact")){

      }
    }

    void OnCollisionEnter(Collision collision)
    {
      // foreach (ContactPoint contact in collision.contacts){

      // }
      Debug.Log("hellow1");
      if(collision.relativeVelocity.magnitude > 2) Debug.Log("hellow");
    }

    public Vector2 Velocity(){
      return velocity;
    }
}
