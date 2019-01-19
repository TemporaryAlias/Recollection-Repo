using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

    Animator anim;

	void Start () {
        anim = GetComponent<Animator>();
	}

    public void OnPointerEnter(PointerEventData eventData) {
        anim.SetBool("Mouse Over", true);
    }

    public void OnPointerExit(PointerEventData eventData) {
        anim.SetBool("Mouse Over", false);
    }

}
