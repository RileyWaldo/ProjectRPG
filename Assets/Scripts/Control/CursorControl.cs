using UnityEngine;

public enum CursorType
{
    None,
    Movement,
    Combat,
    UI,
    PickUp,
    Dialogue
}

namespace RPG.Control
{
    public class CursorControl : MonoBehaviour
    {
        [SerializeField] CursorMapping[] cursorMappings = default;

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotSpot;

            public CursorMapping(CursorType type, Texture2D texture, Vector2 hotSpot)
            {
                this.type = type;
                this.texture = texture;
                this.hotSpot = hotSpot;
            }
        }

        public void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotSpot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach(CursorMapping mapping in cursorMappings)
            {
                if(mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }
    }
}
