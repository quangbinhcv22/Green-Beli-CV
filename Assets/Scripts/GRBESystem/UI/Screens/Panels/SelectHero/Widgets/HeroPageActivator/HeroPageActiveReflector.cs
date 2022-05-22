using UnityEngine;
using UnityEngine.UI;


namespace GRBESystem.UI.Screens.Panels.HeroPageActiveReflector
{
    public class HeroPageActiveReflector : MonoBehaviour
    {
        [SerializeField] private Image sprite;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color unActiveColor;

        public void UpdateView(bool enable)
        {
            sprite.color = enable ? activeColor : unActiveColor;
        }
    }
}
