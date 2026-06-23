using SimpleDragDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public sealed class ColorDragGhost : DragGhost
    {
        [SerializeField] Image image;

        protected override void ApplyGhost(IDragPayload payload)
        {
            if (payload is not ColorDragPayload colorDragPayload)
                return;
            image.enabled = true;
            image.color = colorDragPayload.Color;
        }
    }
}
