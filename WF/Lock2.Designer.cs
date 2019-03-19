namespace WF
{
    partial class Lock2
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.лстПапки = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.кнпОтмена = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // лстПапки
            // 
            this.лстПапки.FormattingEnabled = true;
            this.лстПапки.Location = new System.Drawing.Point(18, 51);
            this.лстПапки.Name = "лстПапки";
            this.лстПапки.Size = new System.Drawing.Size(173, 303);
            this.лстПапки.TabIndex = 0;
            this.лстПапки.SelectedIndexChanged += new System.EventHandler(this.лстПапки_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(208, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Выберите папку для добавления файла";
            // 
            // кнпОтмена
            // 
            this.кнпОтмена.Location = new System.Drawing.Point(116, 360);
            this.кнпОтмена.Name = "кнпОтмена";
            this.кнпОтмена.Size = new System.Drawing.Size(75, 23);
            this.кнпОтмена.TabIndex = 2;
            this.кнпОтмена.Text = "Отмена";
            this.кнпОтмена.UseVisualStyleBackColor = true;
            this.кнпОтмена.Click += new System.EventHandler(this.кнпОтмена_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(211, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "=>";
            // 
            // Lock2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.кнпОтмена);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.лстПапки);
            this.Name = "Lock2";
            this.Size = new System.Drawing.Size(272, 426);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.ListBox лстПапки;
        private System.Windows.Forms.Button кнпОтмена;
        private System.Windows.Forms.Label label2;
    }
}
