using MaterialSkin;
using MaterialSkin.Controls;

namespace MusicRecognitionApp.Forms
{
    public partial class BaseForm : MaterialForm
    {
        public BaseForm()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue500,     // Основной - кнопки, FAB
                Primary.Blue700,     // Темный - ховер эффекты
                Primary.Blue50,      // Очень светлый - фоны
                Accent.LightBlue200, // Акцент - переключатели
                TextShade.WHITE      // Текст
            );
        }
    }
}
