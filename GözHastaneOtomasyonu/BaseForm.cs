using DevExpress.XtraEditors;
using DevExpress.LookAndFeel;
using System.Drawing;

namespace GözHastaneOtomasyonu
{
    public class BaseForm : XtraForm
    {
        public BaseForm()
        {
            // FORM GENEL STİL
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.BackColor = ColorTranslator.FromHtml("#BDE8F5");
            this.Font = new Font("Segoe UI", 9F);

            // DEVEXPRESS SKIN
            this.LookAndFeel.UseDefaultLookAndFeel = true;
            UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");

            // PENCERE AYARLARI
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
        }
    }
}
