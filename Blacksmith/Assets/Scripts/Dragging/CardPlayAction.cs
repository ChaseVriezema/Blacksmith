using System.Collections;
using System.Diagnostics.CodeAnalysis;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Manabound.Card.Dragging
{
    public class CardPlayAction : DragAction 
    {
        
        private float zDisplacement;
        private Vector3 pointerDisplacement;
        [SerializeField] private float returnTime;
        [SerializeField] private float speed;
        [SerializeField] private float cardScale;
        
        private Vector3 scaleDisplacement;

        private bool isReturning = false;
        private bool isDragging = false;

        private Vector3 startPosition;
        public override void BeginDragAction(Vector3 mousePosition)
        {
            if (isReturning)
                return;

            isDragging = true;
            
            startPosition = transform.position;
            Debug.Log(this.GetType() + " Started");
            zDisplacement = -Camera.main.transform.position.z + transform.position.z;
            scaleDisplacement = transform.localScale;
            transform.DOScale(transform.localScale * cardScale, 0.2f).SetEase(Ease.InQuad);
            pointerDisplacement = -transform.position + MouseInWorldCoords(mousePosition);
        }
        public override void DraggingAction(Vector3 mousePosition)
        {
            if (!isDragging)
                return;
            var worldPosition = MouseInWorldCoords(mousePosition);
            var targetPosition = new Vector3(worldPosition.x - pointerDisplacement.x,
                worldPosition.y - pointerDisplacement.y, 
                transform.position.z);

            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }

        public override void EndDragAction(Vector3 mousePosition)
        {
            if (!isDragging)
                return;
            Debug.Log(this.GetType() + " End");
            StartCoroutine(ReturnToStartingPosition(startPosition));

        }

        private IEnumerator ReturnToStartingPosition(Vector3 startingPosition)
        {
            isReturning = true;
            isDragging = false;
            var moveTime = 0.0f;
            while (moveTime <= returnTime)
            {
                transform.position = Vector3.Lerp(transform.position, startingPosition, moveTime/returnTime);
                transform.localScale = Vector3.Lerp(transform.localScale, scaleDisplacement, moveTime / returnTime);
                moveTime += Time.deltaTime;
                yield return null;
            }
            isReturning = false;

        }
        
        private Vector3 MouseInWorldCoords(Vector3 mousePosition2)
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = zDisplacement;
            return Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }
}