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
                Primary.Blue500,     
                Primary.Blue700,     
                Primary.Blue50,      
                Accent.LightBlue200, 
                TextShade.WHITE      
            );
        }
    }
}
