﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF
{
    public partial class Lock2 : UserControl
    {
        public Lock2()
        {
            InitializeComponent();
        }

        private void лстПапки_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (лстПапки.SelectedItem == null) return;
            if (Visible == false) return;

            string[] Paths = new string[]{ };

            Visible = false;

            Paths = ExcelAdapter.Open(true);

            if (Paths == null) return;
            
            foreach(TreeNode node in Form1.Ttree.Nodes[0].Nodes)
            {
                if (лстПапки.SelectedItem.ToString() == node.Text)
                {
                    node.Nodes.Clear();
                    foreach (string p in Paths)
                    {
                        node.Nodes.Add(p);
                    }
                }
            }
            лстПапки.ClearSelected();
        }

        private void кнпОтмена_Click(object sender, EventArgs e)
        {
            Visible = false;

        }
    }
}