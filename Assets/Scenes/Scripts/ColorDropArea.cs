using R3;
using SimpleDragDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public sealed class ColorDropArea : DropArea
    {
        [SerializeField] Image image;
        [SerializeField] Image enableImage;
        readonly CompositeDisposable disposables = new();

        void Awake()
        {
            // グループ全体に反応するイベント
            Group.Context.OnDragStartedAsObservable()
                .Subscribe(OnDragStarted)
                .AddTo(disposables);

            Group.Context.OnDragEndedAsObservable()
                .Subscribe(OnDragEnded)
                .AddTo(disposables);

            image.color = Color.white;
            enableImage.enabled = false;
        }
        void OnDestroy()
        {
            disposables.Dispose();
        }
        protected override void OnDropped(IDragPayload payload)
        {
            if (payload is not ColorDragPayload colorDragPayload)
            {
                return;
            }
            image.color = colorDragPayload.Color;
        }
        private void OnDragStarted(DragStartedEvent @event)
        {
            enableImage.enabled = true;
        }
        private void OnDragEnded(DragEndedEvent @event)
        {
            enableImage.enabled = false;
        }
    }
}