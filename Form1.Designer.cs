namespace GoldCard
{
    partial class Form1
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
            button1 = new Button();
            textBox3 = new TextBox();
            textBox1 = new TextBox();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.White;
            button1.Location = new Point(227, 466);
            button1.Name = "button1";
            button1.Size = new Size(229, 29);
            button1.TabIndex = 2;
            button1.Text = "Start Server";
            button1.UseVisualStyleBackColor = false;
            button1.Click += startServer_btn;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(12, 12);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(0, 0);
            textBox3.TabIndex = 3;
            textBox3.Text = "Norr om Söder";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(24, 31);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(634, 396);
            textBox1.TabIndex = 4;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button2
            // 
            button2.Location = new Point(227, 524);
            button2.Name = "button2";
            button2.Size = new Size(229, 29);
            button2.TabIndex = 5;
            button2.Text = "Update Userlist";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(227, 580);
            button3.Name = "button3";
            button3.Size = new Size(229, 29);
            button3.TabIndex = 6;
            button3.Text = "Update Cardlist";
            button3.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(684, 664);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(textBox1);
            Controls.Add(textBox3);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button button1;
        private TextBox textBox3;
        private TextBox textBox1;
        private Button button2;
        private Button button3;
    }
}