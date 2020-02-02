using UnityEngine;
using UnityEngine.UI;

public class CountScore : MonoBehaviour
{
    public Text time,body,blood,title,total,interact;
    public bool isAnimateActive = false;
    float transparent = 0;
    // Update is called once per frame
    Image image;
    void Start()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        AnimateActive();
    }

    void AnimateActive()
    {
        if(isAnimateActive && transparent < 1)
        {
            
            transparent += 2* Time.deltaTime;
            image.color = new Color(image.color.r,image.color.g,image.color.b, transparent);
            interact.color = new Color(interact.color.r,interact.color.g,interact.color.b, transparent);
            total.color = new Color(total.color.r,total.color.g,total.color.b, transparent);
            title.color = new Color(title.color.r,title.color.g,title.color.b, transparent);
            time.color = new Color(time.color.r,time.color.g,time.color.b, transparent);
            body.color = new Color(body.color.r,body.color.g,body.color.b, transparent);
            blood.color = new Color(blood.color.r,blood.color.g,blood.color.b, transparent);
        }
        if(!isAnimateActive && transparent > 0){
            transparent -= 2 * Time.deltaTime;
            image.color = new Color(image.color.r,image.color.g,image.color.b, transparent);
            interact.color = new Color(interact.color.r,interact.color.g,interact.color.b, transparent);
            total.color = new Color(total.color.r,total.color.g,total.color.b, transparent);
            title.color = new Color(title.color.r,title.color.g,title.color.b, transparent);
            time.color = new Color(time.color.r,time.color.g,time.color.b, transparent);
            body.color = new Color(body.color.r,body.color.g,body.color.b, transparent);
            blood.color = new Color(blood.color.r,blood.color.g,blood.color.b, transparent);
        }
    }
    public void ShowScore(float timer, GameObject[] objectives)
    {
        Debug.Log("ShowScore");
        timer = Mathf.Round(timer * 10f) / 10f;
        int blood = 0;
        int body = 0;
        int total = 0;

        foreach(GameObject objective in objectives){
           ObjectInteraction oi =  objective.GetComponent<ObjectInteraction>();
           if(oi.TypeObjective == PublicEnum.typeObjective.cleanBlood) blood++;
           if(oi.TypeObjective == PublicEnum.typeObjective.cleanBody) body++;
        }

        total += (int) timer * 10;
        total -= blood * 10;
        total -= body * 20;    

        this.time.text = "Time Left : "+timer+" X "+" 10 = "+(int) timer * 10;
        this.total.text = "Total Score : "+total;
        if(blood == 0) this.blood.text = "No Blood Pool Missed";
        else this.blood.text = "Blood Miss : -"+ blood +" X "+" 10 = -"+blood * 10;
        if(body == 0) this.body.text = "No Corpse Missed";
        else this.body.text = "Corpse Miss : -"+body+" X "+" 20 = -"+body * 10;

        isAnimateActive = true;
    }

    public void HideScore()
    {
        isAnimateActive = false;
        // transparent = 0;
    }
}
