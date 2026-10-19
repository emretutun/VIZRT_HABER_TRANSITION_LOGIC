using System.Drawing;
using System.Windows.Forms;

namespace SHOW_TV_ANAHABER.Ui
{
    /// <summary>Tek satırlık metin soran küçük pencere (spiker adı, ihbar numarası gibi).</summary>
    public static class Prompt
    {
        /// <summary>Kullanıcı iptal ederse null döner.</summary>
        public static string Show(IWin32Window owner, string title, string label, string defaultText)
        {
            using (var form = new Form())
            {
                form.Text = title;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ClientSize = new Size(460, 120);
                form.Font = new Font("Segoe UI", 9.75F);

                var lbl = new Label { Text = label, AutoSize = true, Location = new Point(12, 12) };
                var txt = new TextBox { Location = new Point(12, 36), Size = new Size(436, 25), Text = defaultText ?? string.Empty };
                var ok = new Button { Text = "Tamam", DialogResult = DialogResult.OK, Location = new Point(264, 76), Size = new Size(90, 30) };
                var cancel = new Button { Text = "İptal", DialogResult = DialogResult.Cancel, Location = new Point(358, 76), Size = new Size(90, 30) };

                form.Controls.AddRange(new Control[] { lbl, txt, ok, cancel });
                form.AcceptButton = ok;
                form.CancelButton = cancel;
                txt.SelectAll();

                return form.ShowDialog(owner) == DialogResult.OK ? txt.Text : null;
            }
        }
    }
}
