using UnityEngine;

public class CharacterAnim : MonoBehaviour
{
    [SerializeField]
    CharacterController characterController;
    Animator animator;
    float isFaceRight;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if(characterController.Velocity().x > 0.1) isFaceRight= 1;
        if(characterController.Velocity().x < -0.1) isFaceRight= -1;

        animator.SetFloat("isFaceRight", isFaceRight);
        if(characterController.Velocity().x != 0 || characterController.Velocity().y != 0){
            animator.SetBool("isWalk",true);
        }else{
            animator.SetBool("isWalk",false);
        }
    }
}
