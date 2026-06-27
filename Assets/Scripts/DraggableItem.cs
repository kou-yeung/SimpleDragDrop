using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleDragDrop
{
    public abstract class DraggableItem :
        MonoBehaviour,
        IBeginDragHandler,
        IDragHandler,
        IEndDragHandler
    {
        [SerializeField] DragDropGroup group;
        [SerializeField] bool canDrag = true;

        public DragDropGroup Group => group;
        bool isDragging;

        public void Initialize(DragDropGroup group)
        {
            this.group = group;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!Validate())
                return;

            if (!CanBeginDrag())
                return;

            var payload = CreatePayload();

            isDragging = true;

            WillBeginDrag();

            group.Context.BeginDrag(this, payload);
            group.Ghost.Show(payload, eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isDragging || group == null)
                return;

            group.Ghost.Move(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDrag();
        }

        void OnDisable()
        {
            EndDrag();
        }

        void OnDestroy()
        {
            EndDrag();
        }

        void EndDrag()
        {
            if (!isDragging)
                return;

            isDragging = false;

            if (group != null)
            {
                group.Ghost.Hide();
                group.Context.EndDrag();
            }

            WillEndDrag();
        }

        bool Validate()
        {
            if (group == null)
            {
                Debug.LogError(
                    $"{GetType().Name} requires DragDropGroup. " +
                    "Set it in Inspector or call Initialize().",
                    this);

                return false;
            }

            if (group.Ghost == null)
            {
                Debug.LogError(
                    $"{GetType().Name} requires DragGhost.",
                    this);

                return false;
            }

            return true;
        }

        protected virtual bool CanBeginDrag()
        {
            return canDrag;
        }

        protected virtual void WillBeginDrag()
        {
        }

        protected virtual void WillEndDrag()
        {
        }

        protected abstract IDragPayload CreatePayload();
    }
}