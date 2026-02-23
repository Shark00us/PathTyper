namespace WinFormsApp1
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanelMain = new TableLayoutPanel();
            this.textBoxOutput = new TextBox();
            this.labelAlert = new Label();
            this.tableLayoutPanelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.BackColor = Color.Transparent;
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.textBoxOutput, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.labelAlert, 0, 2);
            this.tableLayoutPanelMain.Dock = DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new Point(10, 10);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 3;
            this.tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            this.tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new RowStyle());
            this.tableLayoutPanelMain.Size = new Size(564, 541);
            this.tableLayoutPanelMain.TabIndex = 0;
            this.tableLayoutPanelMain.Paint += this.tableLayoutPanel1_Paint;
            // 
            // textBoxOutput
            // 
            this.textBoxOutput.BorderStyle = BorderStyle.FixedSingle;
            this.textBoxOutput.Dock = DockStyle.Fill;
            this.textBoxOutput.Enabled = false;
            this.textBoxOutput.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBoxOutput.Location = new Point(3, 3);
            this.textBoxOutput.MaxLength = 999999;
            this.textBoxOutput.Name = "textBoxOutput";
            this.textBoxOutput.Size = new Size(558, 43);
            this.textBoxOutput.TabIndex = 9;
            this.textBoxOutput.TabStop = false;
            this.textBoxOutput.TextAlign = HorizontalAlignment.Center;
            this.textBoxOutput.TextChanged += this.textBox1_TextChanged;
            // 
            // labelAlert
            // 
            this.labelAlert.AutoSize = true;
            this.labelAlert.BorderStyle = BorderStyle.FixedSingle;
            this.labelAlert.Dock = DockStyle.Fill;
            this.labelAlert.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.labelAlert.ForeColor = SystemColors.Control;
            this.labelAlert.Location = new Point(4, 505);
            this.labelAlert.Margin = new Padding(4);
            this.labelAlert.Name = "labelAlert";
            this.labelAlert.Size = new Size(556, 32);
            this.labelAlert.TabIndex = 10;
            this.labelAlert.TextAlign = ContentAlignment.MiddleCenter;
            this.labelAlert.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(584, 561);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.MinimumSize = new Size(200, 200);
            this.Name = "Main";
            this.Padding = new Padding(10);
            this.Text = "Form1";
            this.KeyPress += this.Form1_KeyPress;
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.tableLayoutPanelMain.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private TextBox textBoxOutput;
        private Label labelAlert;
    }
}
