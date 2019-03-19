using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using VB = Microsoft.VisualBasic;


namespace WF
{
    public partial class Form1 : Form
    {
        public static string SavePath;

        public static List<string> Paths = new List<string>();

        Lock2 locker2;

        public static TreeView Ttree { get; set; }
        public static Button _btnstart { get; set; }
        public static Label _label4 { get; set; }
        public static Label _label5 { get; set; }
        public static RichTextBox _LogTextBox { get; set; }

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            Ttree = tree;
            locker2 = new Lock2();

            Controls.Add(locker2);

            locker2.Hide();

            _label4 = label4;
            _label5 = label5;
            _btnstart = btnStart;
            _LogTextBox = logBox;
        }

        /// <summary>
        /// Событие После выбора узла TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (tree.SelectedNode.Name != "root") кнУдалить.Enabled = true;
            else кнУдалить.Enabled = false;
            NodeCount();
        }

        /// <summary>
        /// Кнопка Развернуть - Свернуть
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void РазСвер_Click(object sender, EventArgs e)
        {
            if (РазСвер.Text == "Развернуть")
            {
                tree.ExpandAll();
                РазСвер.Text = "Свернуть";
            }
            else
            {
                tree.CollapseAll();
                РазСвер.Text = "Развернуть";
            }
            NodeCount();
        }

        /// <summary>
        /// Событие После свёртки всех узлов TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            РазСвер.Text = "Развернуть";
        }

        /// <summary>
        /// Событие После разворота всех узлов TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            foreach (TreeNode n in tree.Nodes)
            {
                if (n.IsExpanded == false) return;
            }
            РазСвер.Text = "Свернуть";
            NodeCount();
        }

        /// <summary>
        /// Добавление каталога (Подбазисного узла TreeView)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void кнДобКат_Click(object sender, EventArgs e)
        {
            string s = Convert.ToString(VB.Interaction.InputBox("Введите имя каталога", "Создание каталога"));
            if (s == "") return;
            tree.PathSeparator = "/";
            tree.Nodes[0].Nodes.Add(s, s);
            locker2.лстПапки.Items.Add(s);
            tree.ExpandAll();
            NodeCount();
        }

        /// <summary>
        /// Удаление выбранного элемента TreeView (кроме Базисного узла)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void кнУдалить_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < locker2.лстПапки.Items.Count; i++)
            {
                if (tree.SelectedNode.Text == locker2.лстПапки.Items[i].ToString())
                {
                    locker2.лстПапки.Items.RemoveAt(i);
                }
            }
            locker2.лстПапки.Refresh();
            tree.SelectedNode.Remove();
            NodeCount();
        }

        /// <summary>
        /// Добавление файлов к выбранному каталогу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void кнДобФайл_Click(object sender, EventArgs e)
        {

            if (tree.Nodes[0].Nodes.Count == 0)
            {
                MessageBox.Show("Добавьте Каталоги!", "Ошибка ввода!", MessageBoxButtons.OK);
                return;
            }
            locker2.BringToFront();
            locker2.Show();
            locker2.Focus();
            locker2.лстПапки.VisibleChanged += async (ss, ev) =>
            {
                await locker2.лстПапки.WhenChange();
            };

            tree.Refresh();
            NodeCount();
        }

        /// <summary>
        /// Пересчет узлов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            NodeCount();
        }

        /// <summary>
        /// Подсчет количества элементов в TreeView
        /// </summary>
        private void NodeCount()
        {
            if (tree.Nodes[0].Nodes.Count == 0)
            {
                lblFiles.Text = $"Всего файлов: 0";
                lblFold.Text = $"Всего каталогов: 0";
                return;
            }

            int fd = 0;
            int fl = 0;

            foreach (TreeNode n in tree.Nodes[0].Nodes)
            {
                fd++;
                foreach (TreeNode nn in n.Nodes)
                {
                    fl++;
                }
            }

            lblFiles.Text = $"Всего файлов: {fl}";
            lblFold.Text = $"Всего каталогов: {fd}";
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            
            if ((SavePath == "")|(button1.Text == "Выбрать"))
            {
                MessageBox.Show("Выберите путь для сохранения!");
                return;
            }

            if (tree.Nodes[0].Nodes.Count < 1)
            {
                MessageBox.Show("Необходимо добавить каталоги!", "Ошибка!", MessageBoxButtons.OK);
                return;
            }

            int fd = 0;
            int fl = 0;

            foreach (TreeNode n in tree.Nodes[0].Nodes)
            {
                fd++;
                foreach (TreeNode nn in n.Nodes)
                {
                    fl++;
                }
            }

            if (fl < 2)
            {
                MessageBox.Show("Необходимо добавить более 1 файла!", "Ошибка!", MessageBoxButtons.OK);
                return;
            }

            foreach (TreeNode n in tree.Nodes[0].Nodes)
            {
                foreach (TreeNode nn in n.Nodes)
                {
                    Paths.Add(nn.Text);
                }
            }
            //TODO Form1 Start condition
            btnStart.Visible = false;
            Task t = Task.Factory.StartNew(() => EngineUr.Перебор(Paths));
            await t;      

            //Paths.Clear();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fd = new FolderBrowserDialog();
            if (fd.ShowDialog() != DialogResult.Cancel)
            {
                button1.Text = fd.SelectedPath;
                SavePath = button1.Text + "\\" + textBox1.Text;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if ((SavePath == "") | (button1.Text == "Выбрать"))
            {
                MessageBox.Show("Выберите путь для сохранения!");
                return;
            }
            SavePath = button1.Text + "\\" + textBox1.Text;
        }
    }

    public static class Ut
    {
        public static Task WhenChange(this ListBox lb)
        {
            var tcs = new TaskCompletionSource<object>();
            EventHandler onChange = null;
            onChange = (sender, e) =>
            {
                lb.SelectedIndexChanged -= onChange;
                tcs.TrySetResult(null);
            };
            lb.SelectedIndexChanged += onChange;
            return tcs.Task;
        }
    }

}
