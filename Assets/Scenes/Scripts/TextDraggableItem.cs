using R3;
using SimpleDragDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class TextDragPayload : IDragPayload
    {
        public string Text { get; private set; }

        public TextDragPayload(string text)
        {
            this.Text = text;
        }
    }

    public sealed class TextDraggableItem : DraggableItem
    {
        [SerializeField] CanvasGroup canvasGroup;
        [SerializeField] Text label;
        readonly CompositeDisposable disposables = new();
        protected override IDragPayload CreatePayload()
        {
            return new TextDragPayload(label.text);
        }
        void OnDestroy()
        {
            disposables.Dispose();
        }

        protected override void WillBeginDrag()
        {
            canvasGroup.alpha = 0f;
        }

        protected override void WillEndDrag()
        {
            canvasGroup.alpha = 1f;
        }
    }
}