using UnityEngine;

public class StageMaster : MonoBehaviour
{
    public float time = 30f;
    public bool isActive = false;
    [SerializeField]
    Transform[] objectives;
    void Awake()
    {
        objectives = GetComponentsInChildren<Transform>();
        // SetActive();
        SetDeactive();
    }
    public void SetActive()
    {
        foreach(Transform objective in objectives)
        {
            objective.gameObject.SetActive(true);
        }
        Debug.Log("SetActive Called");
    }

    public void SetDeactive()
    {
        
        foreach(Transform objective in objectives)
        {
            objective.gameObject.SetActive(false);
        }
        Debug.Log("SetDeactive Called");

    }


}
