using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Office2010.Word;
using OfficeOpenXml;

namespace WF
{
    public static class Engine3
    {
        public static void Перебор(List<string> Paths)
        {
            Stopwatch s = Stopwatch.StartNew();

            //Файл сводного итогда
            ExcelPackage Buffer = new ExcelPackage();
            Buffer.Workbook.Worksheets.Add("Shit");

            //Создание массивов и общего списка по входным данным
            List<List<User>> books = new List<List<User>>();
            List<Task> tasks = new List<Task>();

            Label4_Update("Этап 1 из 4");
            logTextBox_Update("Подготовка файлов\n");


            foreach (string v in Paths)
            {
                string[] file = v.Split('\\');
                Label5_Update("В обработке: " + file[0] + "\\..\\" + file[file.Length - 3]
                              + "\\" + file[file.Length - 2]
                              + "\\" + file[file.Length - 1]);
                using (var ex = new ExcelPackage(new FileInfo(v)))
                {
                    var e = ex.ConverterToList();
                    books.Add(e.Intersect(e, new MyEqualityComparerWithoutData()).ToList());
                }
                Label5_Update("");
                logTextBox_Update("Обработан: " + file[0] + "\\..\\" + file[file.Length - 3]
                                  + "\\" + file[file.Length - 2]
                                  + "\\" + file[file.Length - 1]);
            }
            // MessageBox.Show(s.Elapsed.ToString() + " Filled"); s.Restart();

            Label4_Update("Этап 2 из 4");
            logTextBox_Update("\nПоиск\n");

            var b = Classif(books);

            Label4_Update("Этап 4 из 4");
            logTextBox_Update("\nСохраняем выходной файл\n");
            PrintToExcel(Buffer, b, 1);
            Buffer.SaveAs(new FileInfo(Form1.SavePath));

            logTextBox_Update("\nФайл сохранен: "+ Form1.SavePath);

            Label4_Update("Готов!");

            MessageBox.Show("Готов!");
        }

        //<HELPERs>
   
        /// <summary>
        /// Запись данных в Excel из массива Users[]
        /// </summary>
        /// <param name="bookToSave">Документ Excel</param>
        /// <param name="users">Массив для записи</param>
        /// <param name="sheetNumber">Номер листа для записи. Создает новый при отсутствии</param>
        private static void PrintToExcel(ExcelPackage bookToSave, List<User> users, int sheetNumber)
        {
            if (sheetNumber <= 0) throw new ArgumentOutOfRangeException(nameof(sheetNumber));
            if (sheetNumber == 0) sheetNumber = 1;
            if (bookToSave.Workbook.Worksheets.Count < sheetNumber)
            {
                for (int i = bookToSave.Workbook.Worksheets.Count; i < sheetNumber; i++)
                {
                    bookToSave.Workbook.Worksheets.Add("Sheet" + sheetNumber);
                }

            }

            int rows = 14;
            foreach (User u in users)
            {
                for (int i = 0; i < 25; i++)
                {
                    bookToSave.Workbook.Worksheets[sheetNumber].Cells[rows, i + 1].Value = u.UserParams(i);
                }
                rows++;
            }
        }

        /// <summary>
        /// 4_Лог найденых пользователей
        /// </summary>
        /// <param name="s">Информация для вывода на Label4</param>
        static void Label4_Update(string s)
        {
            if (Form1._label4.InvokeRequired) Form1._label4.Invoke((Action<string>)Label4_Update, s);
            else Form1._label4.Text = s;
        }

        /// <summary>
        /// 5_Лог найденых пользователей
        /// </summary>
        /// <param name="s">Информация для вывода на Label5</param>
        static void Label5_Update(string s)
        {
            if (Form1._label5.InvokeRequired) Form1._label5.Invoke((Action<string>)Label5_Update, s);
            else Form1._label5.Text = s;
        }

        /// <summary>
        /// Лог
        /// </summary>
        /// <param name="s">Информация для вывода на logTextBox</param>
        static void logTextBox_Update(string s)
        {
            if (Form1._LogTextBox.InvokeRequired) Form1._LogTextBox.Invoke((Action<string>)logTextBox_Update, s);
            else Form1._LogTextBox.Text += ("\n"+s);
        }

        /// <summary>
        /// Преобразователь Excel файла в List Users
        /// </summary>
        /// <param name="p">Excel документ</param>
        /// <returns></returns>
        private static List<User> ConverterToList(this ExcelPackage p)
        {
            var mas = from c in p.Workbook.Worksheets[1].Cells[14, 1, p.GetLastRow(), 1].AsParallel()
                      select CreateUser(p.Workbook.Worksheets[1], c.End.Row);

            List<User> buf = mas.ToList();
            return buf;
        }

        /// <summary>
        /// Создает объект юзер по переданной книге Excel
        /// </summary>
        /// <param name="p">Книга для извлечения объекта User</param>
        /// <param name="timer">Номер строки</param>
        /// <returns>Возвращаемый объект User</returns>
        private static User CreateUser(this ExcelWorksheet p, int timer)
        {
            User user = new User();

            for (int i = 0; i < 31; i++)
            {
                user.SetUserParams(i, p.Cells[timer, i+1].Text ?? null);
            }

            return user;
        }

        /// <summary>
        /// Определяет номер последней значащей строки массива данных
        /// </summary>
        /// <param name="p">Книга Excel</param>
        /// <returns></returns>
        private static int GetLastRow(this ExcelPackage p)
        {
            //Stopwatch s = Stopwatch.StartNew();
            var list = p.Workbook.Worksheets[1];
            int lastRow = 0;

            for (int i = 14; i < list.Dimension.End.Row; i++)
            {
                lastRow = Convert.ToInt32(list.Cells[i, 3].Value ?? 0);

                if (lastRow == 0) { lastRow = --i; break; }
            }
            //MessageBox.Show(s.Elapsed.ToString()+"GetLastRow");
            return lastRow;
        }
        
        //
        //bool HELPERs
        //
        /// <summary>
        /// Булева оценка содержимого строки
        /// </summary>
        /// <param name="str">Входящая строка</param>
        /// <returns></returns>
        public static bool NotInvalidText(this string str)
        {
            if (!string.IsNullOrWhiteSpace(str) &&
                !string.IsNullOrEmpty(str) &&
                str != "б/п" &&
                str != "-" &&
                str != "0" &&
                str != "б/н" &&
                str != "" &&
                !str.StartsWith("Кор")) return true;
            return false;
        }

        //Main method
        private static List<User> Classif(List<List<User>> lst)
        {
            List<User> worker = lst[0];

            for (int i = 1; i < lst.Count; i++)
            {
                string[] file = Form1.Paths[i].Split('\\');
                Label5_Update("Проверяется: " + file[0] + "\\..\\" + file[file.Length - 3]
                              + "\\" + file[file.Length - 2]
                              + "\\" + file[file.Length - 1]);
                List<User> workerCurrent = worker;
                List<User> workerNext = lst[i];

                List<User> bufRangeCurrent = new List<User>();
                List<User> bufRangeNext = new List<User>();

                Stopwatch s = Stopwatch.StartNew();
                var current = workerCurrent;
                Task task1 = new Task(() => bufRangeCurrent = current.Intersect(workerNext, new MyEqualityComparer2to1()).OrderBy(u=>u.UserParams(1)).ToList());
                Task task2 = new Task(() => bufRangeNext = workerNext.Intersect(current, new MyEqualityComparer1to2()).OrderBy(u => u.UserParams(1)).ToList());
                task1.Start();
                task2.Start();
                Task.WaitAll(task1, task2);

                //ExcelPackage Buffer = new ExcelPackage();
                //Buffer.Workbook.Worksheets.Add("Shit");

                //PrintToExcel(Buffer, bufRangeCurrent, 1);
                //PrintToExcel(Buffer, bufRangeNext, 2);
                //Buffer.SaveAs(new FileInfo(Form1.SavePath));

                //break;

                var NextRemains = workerNext.Except(bufRangeNext, new MyEqualityComparerWithoutData()).ToList();
                //MessageBox.Show(s.Elapsed.ToString() + $" 1"); s.Restart();

                current = current.Except(bufRangeCurrent, new MyEqualityComparerWithoutData()).ToList();

                for (int j = 0; j < bufRangeCurrent.Count; j++)
                {
                    if(!bufRangeNext[j].UserParams(22).NotInvalidText()) continue;
                    bufRangeCurrent[j].SetUserParams(22, bufRangeNext[j].UserParams(22));
                    bufRangeCurrent[j].SetUserParams(23, bufRangeNext[j].UserParams(23));
                }

               // MessageBox.Show(s.Elapsed.ToString() + $" 2"); s.Restart();

                current = current.Concat(bufRangeCurrent).Concat(NextRemains).ToList();
                //MessageBox.Show(s.Elapsed.ToString() + $" 3"); s.Restart();

                worker = current.Distinct(new MyEqualityComparerFull()).ToList();
                //MessageBox.Show(s.Elapsed.ToString() + $" 4"); s.Restart();
                
                logTextBox_Update("Проверен: " + file[0] + "\\..\\" + file[file.Length - 3]
                                  + "\\" + file[file.Length - 2]
                                  + "\\" + file[file.Length - 1]);
                Label5_Update("");

            }

            Label4_Update("Этап 3 из 4");
            logTextBox_Update("\nЗавершена проверка. Готовим выходныой файл...\n");
            worker = worker.Where((u) => { return u.КонПокДата.NotInvalidText(); }).OrderBy(n=>n.UserParams(0)).ToList();

            foreach (var u in worker)
            {
                try
                {
                    u.SetUserParams(24,
                        (Convert.ToDouble(u.UserParams(23)) - Convert.ToDouble(u.UserParams(21))).ToString());
                }
                catch (Exception)
                {
                    u.SetUserParams(24, "");
                    continue;
                }
            }

            //try
            //{
            //    worker.ForEach(u =>
            //    {
            //        u.SetUserParams(24,
            //            (Convert.ToDouble(u.UserParams(23)) - Convert.ToDouble(u.UserParams(21))).ToString());
            //    });
            //}
            //catch (Exception) { }


            return worker;
        }
    }
}