using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Forms;
using OfficeOpenXml;
using Microsoft.Office.Interop.Excel;

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

            Label4_Update("Этап 1 из 4");
            logTextBox_Update("Подготовка файлов\n");


            foreach (string v in Paths)
            {
                string[] file = v.Split('\\');
                Label5_Update("В обработке: " + file[0] + "\\..\\" + file[file.Length - 3]
                              + "\\" + file[file.Length - 2]
                              + "\\" + file[file.Length - 1]);

                try
                {
                    using (var ex = new ExcelPackage(new FileInfo(v)))
                    {
                        var e = ex.ConverterToList();
                        books.Add(e.Intersect(e, new MyEqualityComparerWithoutData()).ToList());
                    }
                }
                catch (Exception)
                {
                    //Конвертируем файл в расширение .xlsx, используя Interop.Excel
                    var excelApp = new Microsoft.Office.Interop.Excel.Application
                    {
                        Visible = false,
                        DisplayAlerts = false
                    };
                    var book = excelApp.Workbooks.Add(v);
                    book.SaveAs(v + "x", XlFileFormat.xlOpenXMLWorkbook);
                    var p = book.Path + "\\" + book.Name;
                    book.Close();
                    excelApp.Quit();
                    //Обрабатываем пересохраненный файл
                    using (var ex = new ExcelPackage(new FileInfo(p)))
                    {
                        var e = ex.ConverterToList();
                        books.Add(e.Intersect(e, new MyEqualityComparerWithoutData()).ToList());
                        File.Delete(v);
                    }
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

            //Stopwatch s = Stopwatch.StartNew();
            List<User> buf = new List<User>();

            var sh = p.Workbook.Worksheets[1];
            int row = 14;

            while (sh.Cells[row, 3].Text.NotInvalidText())
            {
                buf.Add(CreateUser(sh, row));
                row++;
            }
            //
            //var mas = from c in p.Workbook.Worksheets[1].Cells[14, 1, p.GetLastRow(), 1].AsParallel()
            //          select CreateUser(p.Workbook.Worksheets[1], c.End.Row);
            //List<User> buf = mas.ToList();
            //MessageBox.Show(s.Elapsed.ToString());
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

        /// <summary>
        /// Поиск самого пользователя по наиболее ранней или поздней дате показаний в заданном списке
        /// </summary>
        /// <param name="s">Список пользователей</param>
        /// <param name="paramNamber">Номер параметра (Дата Начальных или Конечных показаний)</param>
        /// <param name="EarlyRgm">Режим: true - Поиск самого раннего
        ///                               false - Поиск самого позднего</param>
        private static User TheEarliest(this List<User> s, int paramNamber, bool EarlyRgm)
        {
            if (s.Count == 1) return s[0];
            User erl = s[0];

            if (EarlyRgm)
            {
                for (int i = 1; i < s.Count; i++)
                {
                    if (s[i].UserParams(paramNamber).IsEarlyThen(erl.UserParams(paramNamber))) erl = s[i];
                }
            }
            else
            {
                for (int i = 1; i < s.Count; i++)
                {
                    if (erl.UserParams(paramNamber).IsEarlyThen(s[i].UserParams(paramNamber))) erl = s[i];
                }
            }
            return erl;
        }

        /// <summary>
        /// Конвертация показания и перемножение на коэффициент пересчета
        /// </summary>
        /// <param name="pok">Показания (начальные/конечные)</param>
        /// <param name="koef">Коэффициент пересчета</param>
        /// <returns></returns>
        private static double ConvertStrToDbleWithMult(string pok, string koef)
        {
            double doubl = 0;
            string opr = pok;
            string kf = koef;

            if (!opr.NotInvalidText()) return 0;
            if (opr.Contains(".")) opr = opr.Replace(".", ",");
            if (kf.Contains(".")) kf = kf.Replace(".", ",");

            if (kf.NotInvalidText()) doubl = Convert.ToDouble(opr) * Convert.ToDouble(kf);

            return doubl;
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

                var NextRemains = workerNext.Except(bufRangeNext, new MyEqualityComparerWithoutData()).ToList();

                current = current.Except(bufRangeCurrent, new MyEqualityComparerWithoutData()).ToList();

                for (int j = 0; j < bufRangeCurrent.Count; j++)
                {
                    if(!bufRangeNext[j].UserParams(22).NotInvalidText()) continue;
                    bufRangeCurrent[j].SetUserParams(22, bufRangeNext[j].UserParams(22));
                    bufRangeCurrent[j].SetUserParams(23, bufRangeNext[j].UserParams(23));
                }


                current = current.Concat(bufRangeCurrent).Concat(NextRemains).ToList();

                worker = current.Distinct(new MyEqualityComparerFull()).ToList();
                
                logTextBox_Update("Проверен: " + file[0] + "\\..\\" + file[file.Length - 3]
                                  + "\\" + file[file.Length - 2]
                                  + "\\" + file[file.Length - 1]);
                Label5_Update("");

            }

            Label4_Update("Этап 3 из 4");
            logTextBox_Update("\nЗавершена проверка. Готовим выходныой файл...\n");
            worker = worker.Where(u => u.НачПокДата.NotInvalidText()).OrderBy(n => n.UserParams(0)).ToList();
            worker = worker.Where(u => u.КонПокДата.NotInvalidText()).OrderBy(n => n.UserParams(0)).ToList();

            //Прогон по КоэфПересчета
            foreach (var u in worker)
            {
                u.SetUserParams(21, ConvertStrToDbleWithMult(u.UserParams(21), u.UserParams(25)).ToString());
                u.SetUserParams(23, ConvertStrToDbleWithMult(u.UserParams(23), u.UserParams(25)).ToString());
            }

            //Групировка по ЗаводскомуНомеруСчетчика + ТрафинойЗонеСуток
            if (Form1.CounterGroup)
            {
                List<User> subWorker = new List<User>();

                var grp = from u in worker
                    group u by new { p1 = u.UserParams(18), p2 = u.UserParams(17) };

                foreach (var us in grp)
                {
                    var lastDate = us.ToList().TheEarliest(22, false).UserParams(22);
                    var lastValue = us.ToList().TheEarliest(22, false).UserParams(23);
                    var u = us.ToList().TheEarliest(20, true);

                    u.SetUserParams(22, lastDate);
                    u.SetUserParams(23, lastValue);

                    subWorker.Add(u);
                }

                subWorker = subWorker.OrderBy(n => n.UserParams(0)).ToList();

                worker = subWorker;
            }

            //Вычисление суммарного расхода за найденый период показаний
            foreach (var u in worker)
            {
                if (u.UserParams(21).Contains(".")) u.SetUserParams(21, u.UserParams(21).Replace(".", ","));
                if (u.UserParams(23).Contains(".")) u.SetUserParams(23, u.UserParams(23).Replace(".", ","));
                if (!u.UserParams(21).NotInvalidText()) u.SetUserParams(21, "0");
                if (!u.UserParams(23).NotInvalidText()) u.SetUserParams(23, "0");

                u.SetUserParams(24,
                    (Convert.ToDouble(u.UserParams(23)) - Convert.ToDouble(u.UserParams(21))).ToString());
            }

            return worker;
        }
    }
}