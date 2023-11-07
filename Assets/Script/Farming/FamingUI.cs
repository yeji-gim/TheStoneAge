using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FamingUI : MonoBehaviour
{
    public GameObject UItext;
    public Queue<string> Itemqueue = new Queue<string>();
    public bool canShow = true;

    void Update()
    {
        if (canShow && Itemqueue.Count>0)
        {
            canShow = false;
            StartCoroutine(Typing(Itemqueue.Dequeue()));
        }
    }
    IEnumerator Typing(string text)
    {
        gameObject.GetComponent<Animator>().SetTrigger("isFaming");
        UItext.GetComponent<TextMeshProUGUI>().text = "<color=yellow>" + text +"</color>" + "<color=white> À»(¸¦) È¹µæÇÏ¿´½À´Ï´Ù.</color>";
        yield return new WaitForSeconds(3f);
        canShow = true;
    }


}
