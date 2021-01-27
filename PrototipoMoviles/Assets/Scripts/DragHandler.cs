using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragHandler : MonoBehaviour, IPointerDownHandler,IDragHandler,IBeginDragHandler
{
    private Vector2 pos;

    public void OnPointerDown(PointerEventData data)
    {
        //Debug.Log("click");
        pos = data.position;
    }

    public void OnBeginDrag(PointerEventData data)
    {
        //Debug.Log("begin");
        if (pos.x < data.position.x)
        {
            PlayerController.instance.Drag(false);
        }
        else
        {
            PlayerController.instance.Drag(true);
        }
    }
 
    // Start is called before the first frame update
    public void OnDrag(PointerEventData data)
    {
      //NO BORRAR

    }
}
