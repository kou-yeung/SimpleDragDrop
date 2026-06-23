using SimpleDragDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public sealed class TextDragGhost : DragGhost
    {
        [SerializeField] Text label;
        protected override void ApplyGhost(IDragPayload payload)
        {
            if (payload is not TextDragPayload textDragPayload)
                return;

            label.text = textDragPayload.Text;
        }
    }
}