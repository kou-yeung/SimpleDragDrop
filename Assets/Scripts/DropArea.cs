using UnityEngine;
using UnityEngine.EventSystems;

namespace SimpleDragDrop
{
    public abstract class DropArea :
        MonoBehaviour,
        IDropHandler
    {
        [SerializeField] DragDropGroup group;
        public DragDropGroup Group => group;

        public void Initialize(DragDropGroup group)
        {
            this.group = group;
        }
        public void OnDrop(PointerEventData eventData)
        {
            if (group == null || !group.Context.IsDragging)
                return;

            var payload = group.Context.CurrentPayload;

            if (!CanDrop(payload))
                return;

            OnDropped(payload);

            group.Context.Drop(this);
        }
        protected virtual bool CanDrop(IDragPayload payload)
        {
            return true;
        }

        protected abstract void OnDropped(IDragPayload payload);
    }
}