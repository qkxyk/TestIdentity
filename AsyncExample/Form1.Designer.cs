namespace AsyncExample
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
            this.SyncButton = new System.Windows.Forms.Button();
            this.AsyncButton = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsl = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SyncButton
            // 
            this.SyncButton.Location = new System.Drawing.Point(254, 33);
            this.SyncButton.Name = "SyncButton";
            this.SyncButton.Size = new System.Drawing.Size(337, 34);
            this.SyncButton.TabIndex = 0;
            this.SyncButton.Text = "同步方法";
            this.SyncButton.UseVisualStyleBackColor = true;
            this.SyncButton.Click += new System.EventHandler(this.SyncButton_Click);
            // 
            // AsyncButton
            // 
            this.AsyncButton.Location = new System.Drawing.Point(254, 73);
            this.AsyncButton.Name = "AsyncButton";
            this.AsyncButton.Size = new System.Drawing.Size(337, 34);
            this.AsyncButton.TabIndex = 0;
            this.AsyncButton.Text = "异步方法";
            this.AsyncButton.UseVisualStyleBackColor = true;
            this.AsyncButton.Click += new System.EventHandler(this.AsyncButton_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(23, 119);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.Size = new System.Drawing.Size(867, 356);
            this.txtResult.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(639, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "第一个";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(746, 33);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "第二个";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(639, 73);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "第三个";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 451);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(902, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsl
            // 
            this.tsl.Name = "tsl";
            this.tsl.Size = new System.Drawing.Size(131, 17);
            this.tsl.Text = "toolStripStatusLabel1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 473);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.AsyncButton);
            this.Controls.Add(this.SyncButton);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button SyncButton;
        private Button AsyncButton;
        private TextBox txtResult;
        private Button button1;
        private Button button2;
        private Button button3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel tsl;
    }
}