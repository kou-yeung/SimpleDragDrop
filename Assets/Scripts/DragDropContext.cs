using R3;

namespace SimpleDragDrop
{
    public sealed class DragDropContext
    {
        readonly Subject<DragStartedEvent> onDragStarted = new();
        readonly Subject<DragEndedEvent> onDragEnded = new();
        readonly Subject<DroppedEvent> onDropped = new();

        public DraggableItem CurrentSource { get; private set; }
        public IDragPayload CurrentPayload { get; private set; }
        public bool IsDragging => CurrentSource != null;

        public Observable<DragStartedEvent> OnDragStartedAsObservable() => onDragStarted;
        public Observable<DragEndedEvent> OnDragEndedAsObservable() => onDragEnded;
        public Observable<DroppedEvent> OnDroppedAsObservable() => onDropped;

        public void BeginDrag(DraggableItem source, IDragPayload payload)
        {
            CurrentSource = source;
            CurrentPayload = payload;
            onDragStarted.OnNext(new DragStartedEvent(source, payload));
        }

        public void Drop(DropArea target)
        {
            if (CurrentSource == null || CurrentPayload == null)
                return;

            onDropped.OnNext(new DroppedEvent(CurrentSource, target, CurrentPayload));
        }

        public void EndDrag()
        {
            if (CurrentSource != null && CurrentPayload != null)
            {
                onDragEnded.OnNext(new DragEndedEvent(CurrentSource, CurrentPayload));
            }

            CurrentSource = null;
            CurrentPayload = null;
        }
    }
}