namespace WinFormsApp1
{
    public partial class CharSelector : Form
    {
        public char SelectedChar { get; private set; } = (char)0;
        public CharSelector(char[] chors)
        {
            InitializeComponent();
            this.KeyPreview = true;
            if (chors != null && chors.Length > 0)
            {
                this.Text = "Select Char";
                int fontSize = 108 / chors.Length;
                tableLayoutPanel1.RowStyles.Clear();
                tableLayoutPanel1.RowCount = chors.Length;
                foreach (char c in chors)
                {
                    Button btn = new Button()
                    {
                        Text = c.ToString(),
                        Dock = DockStyle.Fill,
                        Font = new Font("Consalos", fontSize)
                    };
                    btn.Click += B_Click;
                    tableLayoutPanel1.Controls.Add(btn);
                }
                for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
                {
                    tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
                }
            }
            else
            {
                this.Close();
            }
        }
        private void B_Click(object? sender, EventArgs e)
        {
            if (sender == null) return;
            Button btn = (Button)sender;
            if (string.IsNullOrEmpty(btn.Text) == false)
            {
                SelectedChar = btn.Text[0];
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void Form2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == '\u001b')
            {
                this.DialogResult = DialogResult.Cancel;
                this.SelectedChar = (char)0;
                this.Close();
            }
        }
    }
}
