using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public PublicEnum.typeObjective TypeObjective = PublicEnum.typeObjective.cleanBlood;
    public PublicEnum.deadBody DeadBodyFace = PublicEnum.deadBody.up;
    public Sprite[] bloodList;
    public Sprite[] bodyList;
    [Range(0,1)]
    public float cleanTreshold = 1;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    int GetDeadBodyFace()
    {
        int indexDeadBody = 0;
        if(DeadBodyFace == PublicEnum.deadBody.up) indexDeadBody = 0;
        if(DeadBodyFace == PublicEnum.deadBody.left) indexDeadBody = 0;
        if(DeadBodyFace == PublicEnum.deadBody.right) indexDeadBody = 0;

        return indexDeadBody;
    }
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if (TypeObjective == PublicEnum.typeObjective.cleanBlood) spriteRenderer.sprite = bloodList[Random.Range(0,bloodList.Length)];
        if (TypeObjective == PublicEnum.typeObjective.cleanBody) spriteRenderer.sprite = bodyList[GetDeadBodyFace()];
    }

    void Update()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r,spriteRenderer.color.g,spriteRenderer.color.b, cleanTreshold);
        
        if(TypeObjective == PublicEnum.typeObjective.cleanBlood)
        {
            if(cleanTreshold < 0) Destroy(gameObject);
        }
    }

}
