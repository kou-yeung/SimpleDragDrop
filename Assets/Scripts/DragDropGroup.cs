using UnityEngine;

namespace SimpleDragDrop
{
    public sealed class DragDropGroup : MonoBehaviour
    {
        [SerializeField] DragGhost ghost;

        readonly DragDropContext context = new();
        public DragDropContext Context => context;
        public DragGhost Ghost => ghost;
    }
}