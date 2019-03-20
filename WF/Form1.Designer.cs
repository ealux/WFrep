namespace WF
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("Корень");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tree = new System.Windows.Forms.TreeView();
            this.label1 = new System.Windows.Forms.Label();
            this.кнДобКат = new System.Windows.Forms.Button();
            this.кнДобФайл = new System.Windows.Forms.Button();
            this.кнУдалить = new System.Windows.Forms.Button();
            this.РазСвер = new System.Windows.Forms.Button();
            this.lblFiles = new System.Windows.Forms.Label();
            this.lblFold = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.logBox = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // tree
            // 
            this.tree.Location = new System.Drawing.Point(281, 27);
            this.tree.Name = "tree";
            treeNode7.Checked = true;
            treeNode7.Name = "root";
            treeNode7.Tag = "0";
            treeNode7.Text = "Корень";
            treeNode7.ToolTipText = "Коррневая папка (программа)";
            this.tree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode7});
            this.tree.Size = new System.Drawing.Size(507, 343);
            this.tree.TabIndex = 1;
            this.tree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterCollapse);
            this.tree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterExpand);
            this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterSelect);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(278, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Дерево вхождений";
            // 
            // кнДобКат
            // 
            this.кнДобКат.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.кнДобКат.Location = new System.Drawing.Point(280, 376);
            this.кнДобКат.Name = "кнДобКат";
            this.кнДобКат.Size = new System.Drawing.Size(76, 34);
            this.кнДобКат.TabIndex = 3;
            this.кнДобКат.Text = "Добавить каталог";
            this.кнДобКат.UseVisualStyleBackColor = true;
            this.кнДобКат.Click += new System.EventHandler(this.кнДобКат_Click);
            // 
            // кнДобФайл
            // 
            this.кнДобФайл.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.кнДобФайл.Location = new System.Drawing.Point(362, 376);
            this.кнДобФайл.Name = "кнДобФайл";
            this.кнДобФайл.Size = new System.Drawing.Size(79, 34);
            this.кнДобФайл.TabIndex = 4;
            this.кнДобФайл.Text = "Добавить файл";
            this.кнДобФайл.UseVisualStyleBackColor = true;
            this.кнДобФайл.Click += new System.EventHandler(this.кнДобФайл_Click);
            // 
            // кнУдалить
            // 
            this.кнУдалить.Enabled = false;
            this.кнУдалить.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.кнУдалить.Location = new System.Drawing.Point(447, 376);
            this.кнУдалить.Name = "кнУдалить";
            this.кнУдалить.Size = new System.Drawing.Size(69, 22);
            this.кнУдалить.TabIndex = 5;
            this.кнУдалить.Text = "Удалить";
            this.кнУдалить.UseVisualStyleBackColor = true;
            this.кнУдалить.Click += new System.EventHandler(this.кнУдалить_Click);
            // 
            // РазСвер
            // 
            this.РазСвер.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.РазСвер.Location = new System.Drawing.Point(707, 376);
            this.РазСвер.Name = "РазСвер";
            this.РазСвер.Size = new System.Drawing.Size(81, 22);
            this.РазСвер.TabIndex = 6;
            this.РазСвер.Text = "Развенуть";
            this.РазСвер.UseVisualStyleBackColor = true;
            this.РазСвер.Click += new System.EventHandler(this.РазСвер_Click);
            // 
            // lblFiles
            // 
            this.lblFiles.AutoSize = true;
            this.lblFiles.Location = new System.Drawing.Point(647, 9);
            this.lblFiles.Name = "lblFiles";
            this.lblFiles.Size = new System.Drawing.Size(90, 13);
            this.lblFiles.TabIndex = 7;
            this.lblFiles.Text = "Всего фалйов: 0";
            // 
            // lblFold
            // 
            this.lblFold.AutoSize = true;
            this.lblFold.Location = new System.Drawing.Point(463, 9);
            this.lblFold.Name = "lblFold";
            this.lblFold.Size = new System.Drawing.Size(104, 13);
            this.lblFold.TabIndex = 8;
            this.lblFold.Text = "Всего каталогов: 0";
            // 
            // btnStart
            // 
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnStart.Location = new System.Drawing.Point(13, 376);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(116, 33);
            this.btnStart.TabIndex = 9;
            this.btnStart.Text = "Старт Расчёта";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Расходы ЭЭ";
            this.notifyIcon1.Visible = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(93, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Лог обработки";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "_";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 334);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 23);
            this.button1.TabIndex = 15;
            this.button1.Text = "Выбрать";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(146, 337);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(120, 20);
            this.textBox1.TabIndex = 17;
            this.textBox1.Text = "Test.xlsx";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(143, 312);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(115, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Имя для сохранения:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 312);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(117, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Путь для сохранения:";
            // 
            // logBox
            // 
            this.logBox.Location = new System.Drawing.Point(15, 97);
            this.logBox.Name = "logBox";
            this.logBox.ReadOnly = true;
            this.logBox.Size = new System.Drawing.Size(251, 199);
            this.logBox.TabIndex = 20;
            this.logBox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 424);
            this.Controls.Add(this.logBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblFold);
            this.Controls.Add(this.lblFiles);
            this.Controls.Add(this.РазСвер);
            this.Controls.Add(this.кнУдалить);
            this.Controls.Add(this.кнДобФайл);
            this.Controls.Add(this.кнДобКат);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tree);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Расходы ЭЭ";
            this.MouseEnter += new System.EventHandler(this.Form1_MouseEnter);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TreeView tree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button кнДобКат;
        private System.Windows.Forms.Button кнДобФайл;
        public System.Windows.Forms.Button кнУдалить;
        public System.Windows.Forms.Button РазСвер;
        private System.Windows.Forms.Label lblFiles;
        private System.Windows.Forms.Label lblFold;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox logBox;
    }
}

