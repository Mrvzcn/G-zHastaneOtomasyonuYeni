using DevExpress.XtraEditors;
using System.Drawing;

namespace GözHastaneOtomasyonu
{
    public static class UIHelper
    {
        // GroupControl standardı
        public static void GroupStandart(GroupControl group)
        {
            group.Appearance.BackColor = Color.White;
            group.Appearance.Options.UseBackColor = true;

            group.AppearanceCaption.Font =
                new Font("Segoe UI", 9F, FontStyle.Bold);

            group.AppearanceCaption.ForeColor =
                ColorTranslator.FromHtml("#0F2854");

            group.Padding = new System.Windows.Forms.Padding(8);
        }

        // Panel standardı
        public static void PanelStandart(System.Windows.Forms.Panel panel)
        {
            panel.BackColor = Color.White;
            panel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        }
    }
}
