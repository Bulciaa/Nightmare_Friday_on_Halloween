using UnityEngine;
using UnityEngine.EventSystems;

public class AnimacjaTekstu : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public AudioSource select;
public Animator anim;

 void Start()
    {
	 anim = GetComponent<Animator>();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
	
     anim.SetBool("isTouching", true);
	select.Play();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        anim.SetBool("isTouching", false);
    }
}
