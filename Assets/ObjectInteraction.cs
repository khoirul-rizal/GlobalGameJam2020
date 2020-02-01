using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    
    public Sprite[] bloodList;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = bloodList[Random.Range(0,bloodList.Length)];

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collision2D collision2D)
    {
      Debug.Log("hellow1");
    }

}
