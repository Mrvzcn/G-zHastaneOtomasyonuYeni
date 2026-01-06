using DevExpress.XtraEditors;
using System.Drawing;
using System.Windows.Forms;

namespace GözHastaneOtomasyonu
{
    public static class UIHelper
    {
        // GROUPCONTROL
        public static void GroupStandart(GroupControl group)
        {
            group.Appearance.BackColor = Color.White;
            group.Appearance.Options.UseBackColor = true;

            group.AppearanceCaption.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            group.AppearanceCaption.ForeColor =
                ColorTranslator.FromHtml("#0F2854");

            group.Padding = new Padding(8);
        }

        // PANEL
        public static void PanelStandart(Panel panel)
        {
            panel.BackColor = Color.White;
            panel.BorderStyle = BorderStyle.FixedSingle;
        }

        // LABELCONTROL
        public static void LabelStandart(LabelControl lbl)
        {
            lbl.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lbl.Appearance.ForeColor = ColorTranslator.FromHtml("#0F2854");
        }

        // TEXTEDIT
        public static void TextEditStandart(TextEdit txt)
        {
            txt.Properties.Appearance.Font = new Font("Segoe UI", 10);
            txt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
        }
        public static void ButtonPrimary(SimpleButton btn)
        {
            btn.Appearance.BackColor = ColorTranslator.FromHtml("#4988C4"); // Giriş butonu rengi
            btn.Appearance.ForeColor = Color.White;
            btn.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Bold);

            btn.Appearance.Options.UseBackColor = true;
            btn.Appearance.Options.UseForeColor = true;
            btn.Appearance.Options.UseFont = true;

            btn.Height = 50;
        }
       
    }
}
