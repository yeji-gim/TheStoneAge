using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueEnding : MonoBehaviour
{ 
    public List<DialogueLine> firstdialogueLines;
    public List<DialogueLine> seconddialogueLines;
    public GameObject grandfathercamera;
    public GameObject momcamera;
    public GameObject dadcamera;
    public GameObject sistercamera;
    public GameObject boycamera;
    public float rotationSpeed = 60.0f;
    public Animator momanimator;
    public Animator dadanimator;
    public Animator grandfhateranimator;
    public Animator sisteranimator;
    public Animator boyanimator;

    private void Start()
    {
        StartCoroutine(ShowCharacters());
    }
    private IEnumerator ShowCharacters()
    {
        // 대화 시작
        DialogueManager.Instance.StartDialogue(firstdialogueLines);
        yield return new WaitForSeconds(2.0f); // 2초 대기

        // 활성화할 순서대로 캐릭터 및 카메라를 활성화하고 애니메이션 재생
        UIManager.Instance.ToggleDialoguePanel();
        grandfathercamera.SetActive(true);
        grandfhateranimator.SetBool("isPray", true);
        yield return new WaitForSeconds(3.0f); 

        sistercamera.SetActive(true);
        sisteranimator.SetBool("isPray", true);
        grandfathercamera.SetActive(false);
        yield return new WaitForSeconds(3.0f); 

        dadcamera.SetActive(true);
        dadanimator.SetBool("isPray", true);
        sistercamera.SetActive(false);
        yield return new WaitForSeconds(3.0f); 

        momcamera.SetActive(true);
        momanimator.SetBool("isPray", true);
        dadcamera.SetActive(false);
        yield return new WaitForSeconds(3.0f);

        // 대화 종료
        boycamera.SetActive(true);
        boyanimator.SetBool("isDance", true);
        momcamera.SetActive(false);
        yield return new WaitForSeconds(4.0f);
        
        boycamera.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        grandfathercamera.SetActive(true);
        grandfhateranimator.SetBool("isPray", false);
        yield return new WaitForSeconds(1.0f);
        DialogueManager.Instance.StartDialogue(seconddialogueLines);

    }
}


