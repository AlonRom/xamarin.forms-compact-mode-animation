using CompactMode.Enums;
using Xamarin.Forms;

namespace CompactMode.CustomControls
{
    public class CustomGradientLayout : StackLayout
    {
        public string ColorsList { get; set; }

        public Color[] Colors
        {
            get
            {
                string[] hex = ColorsList.Split(',');
                Color[] colors = new Color[hex.Length];

                for (int i = 0; i < hex.Length; i++)
                {
                    colors[i] = Color.FromHex(hex[i].Trim());
                }

                return colors;
            }
        }

        public GradientColorStackMode Mode { get; set; }
    }
}
