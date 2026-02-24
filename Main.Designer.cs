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
            tableLayoutPanelMain = new TableLayoutPanel();
            textBoxOutput = new TextBox();
            labelAlert = new Label();
            tableLayoutPanelMain.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanelMain
            // 
            tableLayoutPanelMain.BackColor = Color.Transparent;
            tableLayoutPanelMain.ColumnCount = 1;
            tableLayoutPanelMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.Controls.Add(textBoxOutput, 0, 0);
            tableLayoutPanelMain.Controls.Add(labelAlert, 0, 2);
            tableLayoutPanelMain.Dock = DockStyle.Fill;
            tableLayoutPanelMain.Location = new Point(10, 10);
            tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            tableLayoutPanelMain.RowCount = 3;
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanelMain.RowStyles.Add(new RowStyle());
            tableLayoutPanelMain.Size = new Size(564, 541);
            tableLayoutPanelMain.TabIndex = 0;
            tableLayoutPanelMain.Paint += tableLayoutPanelMain_Paint;
            // 
            // textBoxOutput
            // 
            textBoxOutput.BorderStyle = BorderStyle.FixedSingle;
            textBoxOutput.Dock = DockStyle.Fill;
            textBoxOutput.Enabled = false;
            textBoxOutput.Font = new Font("Segoe UI", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxOutput.Location = new Point(3, 3);
            textBoxOutput.MaxLength = 999999;
            textBoxOutput.Name = "textBoxOutput";
            textBoxOutput.Size = new Size(558, 43);
            textBoxOutput.TabIndex = 9;
            textBoxOutput.TabStop = false;
            textBoxOutput.TextAlign = HorizontalAlignment.Center;
            textBoxOutput.TextChanged += textBoxOutput_TextChanged;
            // 
            // labelAlert
            // 
            labelAlert.AutoSize = true;
            labelAlert.BorderStyle = BorderStyle.FixedSingle;
            labelAlert.Dock = DockStyle.Fill;
            labelAlert.Font = new Font("Segoe UI", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelAlert.ForeColor = SystemColors.Control;
            labelAlert.Location = new Point(4, 505);
            labelAlert.Margin = new Padding(4);
            labelAlert.Name = "labelAlert";
            labelAlert.Size = new Size(556, 32);
            labelAlert.TabIndex = 10;
            labelAlert.TextAlign = ContentAlignment.MiddleCenter;
            labelAlert.Visible = false;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 561);
            Controls.Add(tableLayoutPanelMain);
            MinimumSize = new Size(200, 200);
            Name = "Main";
            Padding = new Padding(10);
            Text = "Form1";
            KeyDown += MainForm_KeyDown;
            KeyPress += MainForm_KeyPress;
            KeyUp += MainForm_KeyUp;
            tableLayoutPanelMain.ResumeLayout(false);
            tableLayoutPanelMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanelMain;
        private TextBox textBoxOutput;
        private Label labelAlert;
    }
}
