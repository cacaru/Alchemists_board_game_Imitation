using UnityEngine;
using UnityEngine.UI;

namespace Alchemists_data
{
    public static class Scroll_to_Bottom
    {
        public static void Scroll_to_top(this ScrollRect scrollRect)
        {
            scrollRect.normalizedPosition = new Vector2(0, 1);
        }
        public static void Scroll_to_bottom(this ScrollRect scrollRect)
        {
            scrollRect.normalizedPosition = new Vector2(0, 0);
        }
    }
}
