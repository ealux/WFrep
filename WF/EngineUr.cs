using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using OfficeOpenXml;
using System.Windows.Forms;

namespace WF
{
    static class EngineUr
    {
        public static void Перебор(List<string> Paths)
        {
            Stopwatch s = Stopwatch.StartNew();

            //Файл сводного итогда
            ExcelPackage Buffer = new ExcelPackage();
            Buffer.Workbook.Worksheets.Add("Sheet1");

            //Создание массивов и общего списка по входным данным
            List<List<UserUr>> books = new List<List<UserUr>>();
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
                    var e = ex.ConverterToListUr();
                    books.Add(e);
                    //books.Add(e.Intersect(e, new MyEqualityComparerWithoutData()).ToList());
                }
                Label5_Update("");
                logTextBox_Update("Обработан: " + file[0] + "\\..\\" + file[file.Length - 3]
                                  + "\\" + file[file.Length - 2]
                                  + "\\" + file[file.Length - 1]);
            }
            // MessageBox.Show(s.Elapsed.ToString() + " Filled"); s.Restart();

            Label4_Update("Этап 2 из 4");
            logTextBox_Update("\nПоиск\n");
            //TODO Реализовать логику отбора
            //var b = Classif(books);

            Label4_Update("Этап 4 из 4");
            logTextBox_Update("\nСохраняем выходной файл\n");
            PrintToExcelUr(Buffer, books[0], 1);
            Buffer.SaveAs(new FileInfo(Form1.SavePath));

            logTextBox_Update("\nФайл сохранен: " + Form1.SavePath);

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
        private static void PrintToExcelUr(ExcelPackage bookToSave, List<UserUr> users, int sheetNumber)
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

            int rows = 17;
            foreach (UserUr u in users)
            {
                for (int i = 0; i < 37; i++)
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
            else Form1._LogTextBox.Text += ("\n" + s);
        }

        /// <summary>
        /// Преобразователь Excel файла в List Users
        /// </summary>
        /// <param name="p">Excel документ</param>
        /// <returns></returns>
        private static List<UserUr> ConverterToListUr(this ExcelPackage p)
        {
            List<UserUr> buf = new List<UserUr>();
            ExcelWorksheet sh = p.Workbook.Worksheets[1];
            int j = 17;

            while (true)
            {
                string orgName = sh.Cells[j, 2].Text;
                string flowType = sh.Cells[j + 1, 2].Text;
                j += 2;
                while (sh.Cells[j, 2].Text.NotInvalidText())
                {
                    buf.Add(CreateUserUr(sh, orgName, flowType, j));
                    j++;
                }

                if (sh.Cells[j + 1, 2].Text.NotInvalidText())
                {
                    flowType = sh.Cells[j + 1, 2].Text;
                    j += 2;
                    while (sh.Cells[j, 2].Text.NotInvalidText())
                    {
                        buf.Add(CreateUserUr(sh, orgName, flowType, j));
                        j++;
                    }
                    j += 3;
                }
                else
                {
                    j += 3;
                }

                if (!sh.Cells[j, 1].Text.NotInvalidText() &&
                    !sh.Cells[j + 1, 1].Text.NotInvalidText()) break;
            }

            return buf;
        }

        /// <summary>
        /// Создает объект UserUr по переданной книге, листу и строке Excel
        /// </summary>
        /// <param name="p">Лист книги Excel</param>
        /// <param name="orgName">Имя организациии для записи в объект</param>
        /// <param name="flowType">Тип перетока для записи в объект</param>
        /// <param name="timer">Номер строки из книги, откуда производится запись</param>
        /// <returns></returns>
        private static UserUr CreateUserUr(this ExcelWorksheet p, string orgName, string flowType, int timer)
        {
            UserUr user = new UserUr();

            user.SetUserParams(0, orgName);
            user.SetUserParams(1, flowType);

            for (int i = 2; i < 37; i++)
            {
                user.SetUserParams(i, p.Cells[timer, i-1].Text ?? null);
            }

            return user;
        }

        //
        //bool HELPERs
        //
        /// <summary>
        /// Булева оценка содержимого строки
        /// </summary>
        /// <param name="str">Входящая строка</param>
        /// <returns></returns>
        //public static bool NotInvalidText(this string str)
        //{
        //    if (!string.IsNullOrWhiteSpace(str) &&
        //        !string.IsNullOrEmpty(str) &&
        //        str != "б/п" &&
        //        str != "-" &&
        //        str != "0" &&
        //        str != "б/н" &&
        //        str != "" &&
        //        !str.StartsWith("Кор")) return true;
        //    return false;
        //}

        //Main method
        private static List<UserUr> Classif(List<List<UserUr>> lst)
        {
            //TODO Продукт логики отбора
            List<UserUr> worker = lst[0];

            for (int i = 1; i < lst.Count; i++)
            {
                string[] file = Form1.Paths[i].Split('\\');
                Label5_Update("Проверяется: " + file[0] + "\\..\\" + file[file.Length - 3]
                              + "\\" + file[file.Length - 2]
                              + "\\" + file[file.Length - 1]);
                List<UserUr> workerCurrent = worker;
                List<UserUr> workerNext = lst[i];

                List<UserUr> bufRangeCurrent = new List<UserUr>();
                List<UserUr> bufRangeNext = new List<UserUr>();

                //Stopwatch s = Stopwatch.StartNew();
                var current = workerCurrent;
                Task task1 = new Task(() => bufRangeCurrent = current.Intersect(workerNext, new MyEqualityComparer2to1Ur()).OrderBy(u => u.UserParams(1)).ToList());
                Task task2 = new Task(() => bufRangeNext = workerNext.Intersect(current, new MyEqualityComparer1to2Ur()).OrderBy(u => u.UserParams(1)).ToList());
                task1.Start();
                task2.Start();
                Task.WaitAll(task1, task2);

                //ExcelPackage Buffer = new ExcelPackage();
                //Buffer.Workbook.Worksheets.Add("Shit");

                //PrintToExcel(Buffer, bufRangeCurrent, 1);
                //PrintToExcel(Buffer, bufRangeNext, 2);
                //Buffer.SaveAs(new FileInfo(Form1.SavePath));

                //break;

                var NextRemains = workerNext.Except(bufRangeNext, new MyEqualityComparerWithoutDataUr()).ToList();
                //MessageBox.Show(s.Elapsed.ToString() + $" 1"); s.Restart();

                current = current.Except(bufRangeCurrent, new MyEqualityComparerWithoutDataUr()).ToList();

                for (int j = 0; j < bufRangeCurrent.Count; j++)
                {
                    if (!bufRangeNext[j].UserParams(22).NotInvalidText()) continue;
                    bufRangeCurrent[j].SetUserParams(22, bufRangeNext[j].UserParams(22));
                    bufRangeCurrent[j].SetUserParams(23, bufRangeNext[j].UserParams(23));
                }

                // MessageBox.Show(s.Elapsed.ToString() + $" 2"); s.Restart();

                current = current.Concat(bufRangeCurrent).Concat(NextRemains).ToList();
                //MessageBox.Show(s.Elapsed.ToString() + $" 3"); s.Restart();

                worker = current.Distinct(new MyEqualityComparerFullUr()).ToList();
                //MessageBox.Show(s.Elapsed.ToString() + $" 4"); s.Restart();

                logTextBox_Update("Проверен: " + file[0] + "\\..\\" + file[file.Length - 3]
                                  + "\\" + file[file.Length - 2]
                                  + "\\" + file[file.Length - 1]);
                Label5_Update("");

            }

            Label4_Update("Этап 3 из 4");
            logTextBox_Update("\nЗавершена проверка. Готовим выходныой файл...\n");
            worker = worker.Where((u) => { return u.КонПокДата.NotInvalidText(); }).OrderBy(n => n.UserParams(0)).ToList();

            //foreach (var u in worker)
            //{
            //    try
            //    {
            //        u.SetUserParams(24,
            //            (Convert.ToDouble(u.UserParams(23)) - Convert.ToDouble(u.UserParams(21))).ToString());
            //    }
            //    catch (Exception)
            //    {
            //        u.SetUserParams(24, "");
            //        continue;
            //    }
            //}

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
