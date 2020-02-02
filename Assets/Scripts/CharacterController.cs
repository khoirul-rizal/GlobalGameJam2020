using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject corpse;
    Vector2 velocity;
    BoxCollider2D boxCollider;
    float speed = 5;
    float walkAcceleration = 10;
    float groundDeceleration = 5;

    GameObject objectiveGO;
    [SerializeField]
    GameObject currentGO;
    PublicEnum.typeObjective currentObj;
    public int playerState;
    /*
     0 = not carrying
     1 = caryying body
     2 = caryying mop
     3 = mopping
    */
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
        if (hit.gameObject.tag == "Finish") return;
        if (hit.gameObject.tag == "Trigger") return;
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
      if(Input.GetButtonUp("Interact") && playerState == 3) playerState = 0;
      if(Input.GetButton("Interact")){
        if(objectiveGO != null){
          ObjectInteraction objectInteraction = objectiveGO.GetComponent<ObjectInteraction>();

          if(playerState != 1)
          {
            corpse.SetActive(false);
            if(objectInteraction.TypeObjective == PublicEnum.typeObjective.cleanBlood) InteractionBlood(objectInteraction);
            if(objectInteraction.TypeObjective == PublicEnum.typeObjective.cleanBody) InteractionBody();
          }else{
            if(objectInteraction.TypeObjective == PublicEnum.typeObjective.truckDumper) InteractionTruckDumper();
            corpse.SetActive(true);
            Debug.Log("You carrying body");
          }

        }
      }
    }
    void InteractionTruckDumper()
    {
      playerState = 0;
      currentGO.SetActive(false);
      currentGO = null;
    }
    void InteractionBody()
    {
      currentGO = objectiveGO;
      currentGO.GetComponent<SpriteRenderer>().enabled = false;
      playerState = 1;
    }
    void InteractionBlood(ObjectInteraction oi)
    {
      playerState = 3;
      float a = 0.1f;
      oi.cleanTreshold = oi.cleanTreshold - a;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.gameObject.tag == "Finish" || collision.gameObject.tag == "Trigger"){
        Debug.Log("Iam in");
        objectiveGO = collision.gameObject;
      }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
      if(collision.gameObject.tag == "Finish" || collision.gameObject.tag == "Trigger"){
        Debug.Log("Iam out");
        objectiveGO = null;
      }
    }
    public void ResetPlayerState()
    {
      currentGO = null;
      corpse.SetActive(false);
      playerState = 0;
    }
    public Vector2 Velocity(){
      return velocity;
    }

    public int PlayerState(){
      return playerState;
    }
}
