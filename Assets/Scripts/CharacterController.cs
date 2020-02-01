﻿using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    GameObject player;
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
            if(objectInteraction.TypeObjective == PublicEnum.typeObjective.cleanBlood) InteractionBlood(objectInteraction);
            if(objectInteraction.TypeObjective == PublicEnum.typeObjective.cleanBody) InteractionBody(objectInteraction);
          }else{
            if(objectInteraction.TypeObjective == PublicEnum.typeObjective.truckDumper) InteractionTruckDumper(objectInteraction);

            Debug.Log("You carrying body");
          }

        }
      }
    }
    void InteractionTruckDumper(ObjectInteraction oi)
    {
      playerState = 0;
      Destroy(currentGO);
      currentGO = null;
    }
    void InteractionBody(ObjectInteraction oi)
    {
      currentGO = objectiveGO;
      playerState = 1;
    }
    void InteractionBlood(ObjectInteraction oi)
    {
      playerState = 3;
      oi.cleanTreshold -= float.Parse("0.2") * Time.deltaTime;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
      if(collision.gameObject.tag == "Finish"){
        Debug.Log("Iam in");
        objectiveGO = collision.gameObject;
      }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
      if(collision.gameObject.tag == "Finish"){
        Debug.Log("Iam out");
        objectiveGO = null;
      }
    }

    public Vector2 Velocity(){
      return velocity;
    }

    public int PlayerState(){
      return playerState;
    }
}
