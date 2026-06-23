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

        public DragDropGroup Group => group;

        /// <summary>
        /// インスペクタから指定できますが、動的生成を想定してスクリプトからも更新できるようにします
        /// </summary>
        /// <param name="group"></param>
        public void Initialize(DragDropGroup group)
        {
            this.group = group;
        }

        protected abstract IDragPayload CreatePayload();

        bool Validate()
        {
            if (group != null)
            {
                return true;
            }

            Debug.LogError(
                $"{GetType().Name} requires DragDropGroup. " +
                "Set it in Inspector or call Initialize().",
                this);

            return false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!Validate())
            {
                return;
            }

            var payload = CreatePayload();
            group.Context.BeginDrag(this, payload);
            group.Ghost?.Show(payload, eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!Validate())
            {
                return;
            }
            group.Ghost?.Move(eventData.position);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!Validate())
            {
                return;
            }
            group.Ghost?.Hide();
            group.Context.EndDrag();
        }
    }
}