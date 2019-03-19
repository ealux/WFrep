namespace WF
{
    partial class SelectorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFiz = new System.Windows.Forms.Button();
            this.btnUr = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFiz
            // 
            this.btnFiz.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnFiz.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnFiz.Location = new System.Drawing.Point(12, 12);
            this.btnFiz.Name = "btnFiz";
            this.btnFiz.Size = new System.Drawing.Size(115, 23);
            this.btnFiz.TabIndex = 0;
            this.btnFiz.Text = "Физические лица";
            this.btnFiz.UseVisualStyleBackColor = false;
            this.btnFiz.Click += new System.EventHandler(this.btnFiz_Click);
            // 
            // btnUr
            // 
            this.btnUr.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnUr.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnUr.Location = new System.Drawing.Point(161, 12);
            this.btnUr.Name = "btnUr";
            this.btnUr.Size = new System.Drawing.Size(115, 23);
            this.btnUr.TabIndex = 1;
            this.btnUr.Text = "Юридические лица";
            this.btnUr.UseVisualStyleBackColor = false;
            this.btnUr.Click += new System.EventHandler(this.btnUr_Click);
            // 
            // SelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 48);
            this.Controls.Add(this.btnUr);
            this.Controls.Add(this.btnFiz);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectorForm";
            this.Text = "Выберите тип потребителя";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFiz;
        private System.Windows.Forms.Button btnUr;
    }
}