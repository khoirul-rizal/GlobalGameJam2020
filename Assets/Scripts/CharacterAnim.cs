using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    [SerializeField]
    CharacterController characterController;
    Animator animator;
    float isFaceRight;
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {

        if(characterController.Velocity().x > 0.1) spriteRenderer.flipX = false;
        if(characterController.Velocity().x < -0.1) spriteRenderer.flipX = true;
        animator.SetInteger("playerState", characterController.PlayerState());
        
        if(characterController.Velocity().x != 0 || characterController.Velocity().y != 0){
            animator.SetBool("isWalk",true);
        }else{
            animator.SetBool("isWalk",false);
        }
    }
}
