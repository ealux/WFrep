using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //_Tester t = new _Tester();
            //t.StartTest();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Form1 basic = new Form1();
            //basic.Visible = false;
            Application.Run(new SelectorForm());
            Application.Run(new Form1());
        }
    }
}
