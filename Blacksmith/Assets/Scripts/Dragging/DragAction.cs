using UnityEngine;
using UnityEngine.EventSystems;

namespace Manabound.Card.Dragging
{
    public abstract class DragAction : MonoBehaviour
    {
        public bool CanDrag { get; set; }
        public abstract void BeginDragAction(Vector3 mousePosition);
        public abstract void DraggingAction(Vector3 mousePosition);
        public abstract void EndDragAction(Vector3 mousePosition);

    }
}