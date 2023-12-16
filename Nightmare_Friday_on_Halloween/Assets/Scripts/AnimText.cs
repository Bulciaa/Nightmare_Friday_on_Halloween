using UnityEngine;
using UnityEngine.EventSystems;

public class AnimText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  
public Animator anim;

 void Start()
    {
	 anim = GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
	
     anim.SetBool("isTouching", true);
	
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("isTouching", false);
    }
}
