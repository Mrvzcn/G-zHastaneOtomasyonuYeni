using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;

namespace GözHastaneOtomasyonu
{
    public static class GridHelper
    {
        public static void StandartAyarla(GridControl grid, GridView view)
        {
            // Genel görünüm
            view.OptionsView.ShowGroupPanel = false;
            view.OptionsView.ShowIndicator = false;
            view.OptionsBehavior.Editable = false;
            view.OptionsSelection.EnableAppearanceFocusedCell = false;

            // Zebra (satır çizgisi)
            view.OptionsView.EnableAppearanceEvenRow = true;
            view.OptionsView.EnableAppearanceOddRow = true;

            view.Appearance.EvenRow.BackColor = Color.FromArgb(245, 249, 252);
            view.Appearance.OddRow.BackColor = Color.White;

            // Seçili satır
            view.Appearance.FocusedRow.BackColor = Color.FromArgb(73, 136, 196);
            view.Appearance.FocusedRow.ForeColor = Color.White;

            // Başlıklar
            view.Appearance.HeaderPanel.Font =
                new Font("Tahoma", 9, FontStyle.Bold);
            view.Appearance.HeaderPanel.BackColor =
                Color.FromArgb(28, 77, 141);
            view.Appearance.HeaderPanel.ForeColor = Color.White;

            view.Appearance.HeaderPanel.Options.UseBackColor = true;
            view.Appearance.HeaderPanel.Options.UseForeColor = true;
            view.Appearance.HeaderPanel.Options.UseFont = true;

            // Grid genel
            grid.UseEmbeddedNavigator = true;
        }
    }
}
