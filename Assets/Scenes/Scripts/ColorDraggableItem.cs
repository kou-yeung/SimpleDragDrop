using R3;
using SimpleDragDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class ColorDragPayload : IDragPayload
    {
        public Color Color { get; private set; }

        public ColorDragPayload(Color color)
        {
            this.Color = color;
        }
    }

    [RequireComponent(typeof(Image))]
    public sealed class ColorDraggableItem : DraggableItem
    {
        readonly CompositeDisposable disposables = new();

        private void Awake()
        {
            // グループ全体に反応するイベント
            Group.Context.OnDragStartedAsObservable()
                .Subscribe(OnDragStarted)
                .AddTo(disposables);

            Group.Context.OnDragEndedAsObservable()
                .Subscribe(OnDragEnded)
                .AddTo(disposables);
        }

        private void OnDragStarted(DragStartedEvent @event)
        {
            var image = GetComponent<Image>();
            var color = image.color;
            color.a = @event.Source == this ? 1f : 0.75f;
            image.color = color;
        }

        private void OnDragEnded(DragEndedEvent @event)
        {
            var image = GetComponent<Image>();
            var color = image.color;
            color.a = 1;
            image.color = color;
        }

        protected override IDragPayload CreatePayload()
        {
            return new ColorDragPayload(GetComponent<Image>().color);
        }
        void OnDestroy()
        {
            disposables.Dispose();
        }
    }
}