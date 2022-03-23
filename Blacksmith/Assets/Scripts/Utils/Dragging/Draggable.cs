using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace Manabound.Card.Dragging
{
    public class Draggable : MonoBehaviour 
    {
        [SerializeField] private DragAction dragAction;

        private void OnMouseDown()
        {
            dragAction.BeginDragAction(Input.mousePosition);
        }

        private void OnMouseDrag()
        {
            dragAction.DraggingAction(Input.mousePosition);
        }

        private void OnMouseUp()
        {
            dragAction.EndDragAction(Input.mousePosition);
        }
        

    }
}