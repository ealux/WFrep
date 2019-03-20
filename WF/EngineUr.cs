using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using OfficeOpenXml;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;

namespace WF
{
    static class EngineUr
    {
        public static void Перебор(List<string> Paths)
        {
            //Файл сводного итогда
            ExcelPackage Buffer = new ExcelPackage();
            Buffer.Workbook.Worksheets.Add("Sheet1");

            //Создание массивов и общего списка по входным данным
            List<List<UserUr>> books = new List<List<UserUr>>();

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
                        var e = ex.ConverterToListUr();
                        books.Add(e);
                    }
                }
                catch (Exception)
                {
                    //Конвертируем файл в расширение .xlsx, используя Interop.Excel
                    var excelApp = new Microsoft.Office.Interop.Excel.Application();
                    excelApp.Visible = false;
                    excelApp.DisplayAlerts = false;
                    var book = excelApp.Workbooks.Add(v);
                    book.SaveAs(v + "x", XlFileFormat.xlOpenXMLWorkbook);
                    var p = book.Path + "\\" + book.Name;
                    book.Close();
                    excelApp.Quit();
                    //Обрабатываем пересохраненный файл
                    using (var ex = new ExcelPackage(new FileInfo(p)))
                    {
                        var e = ex.ConverterToListUr();
                        books.Add(e);
                        File.Delete(v);
                    }
                    //MessageBox.Show(
                    //    "Внимание!\n\nВы пытаетесь обработать файл формата (*xls).\nДля корректной работы преобразуйте выбраный файл в формат (*.xlsx)!",
                    //    "Ошибка!");
                    //Form1.Paths.Clear();
                    //butStart_Update(true);

                    //return;
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
            PrintToExcelUr(Buffer, b, 1);
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
                for (int i = 0; i < 24; i++)
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

        /// <summary>
        /// Функция для сравнения дат. Возвращает true если аргумент является более поздней датой.
        /// </summary>
        /// <param name="s1">Дата к которой применяется сравнение</param>
        /// <param name="s2">Дата, с которой производится сравнение (аргумент)</param>
        /// <returns></returns>
        public static bool IsEarlyThen(this string s1, string s2)
        {
            var str1 = s1.Split('.');
            var str2 = s2.Split('.');

            short[] num1 = {Convert.ToInt16(str1[0]), Convert.ToInt16(str1[1]), Convert.ToInt16(str1[2])};
            short[] num2 = {Convert.ToInt16(str2[0]), Convert.ToInt16(str2[1]), Convert.ToInt16(str2[2])};

            bool str = false;

            for (int i = 2; i >= 0; i--)
            {
                if(num2[i] == num1[i])
                {
                    if (i == 0)
                    {
                        return str;
                    }
                    continue;
                }

                if (num2[i] > num1[i]) return true;
                else return false;
            }
            return str;
        }

        /// <summary>
        /// Поиск самого пользователя по наиболее ранней или поздней дате показаний в заданном списке
        /// </summary>
        /// <param name="s">Список пользователей</param>
        /// <param name="paramNamber">Номер параметра (Дата Начальных или Конечных показаний)</param>
        /// <param name="EarlyRgm">Режим: true - Поиск самого раннего
        ///                               false - Поиск самого позднего</param>
        private static UserUr TheEarliest(this List<UserUr> s, int paramNamber, bool EarlyRgm)
        {
            if (s.Count == 1) return s[0];
            UserUr erl = s[0];

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

        //Main method
        private static List<UserUr> Classif(List<List<UserUr>> lst)
        {
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
                Task task1 = new Task(() => bufRangeCurrent = current.Intersect(workerNext, new MyEqualityComparer2to1Ur()).OrderBy(u => u.UserParams(0)).ToList());
                Task task2 = new Task(() => bufRangeNext = workerNext.Intersect(current, new MyEqualityComparer1to2Ur()).OrderBy(u => u.UserParams(0)).ToList());
                task1.Start();
                task2.Start();
                Task.WaitAll(task1, task2);

                var NextRemains = workerNext.Except(bufRangeNext, new MyEqualityComparerWithoutDataUr()).ToList();

                current = current.Except(bufRangeCurrent, new MyEqualityComparerWithoutDataUr()).ToList();

                for (int j = 0; j < bufRangeCurrent.Count; j++)
                {
                    if (!bufRangeNext[j].UserParams(21).NotInvalidText()) continue;
                    bufRangeCurrent[j].SetUserParams(21, bufRangeNext[j].UserParams(21));
                    bufRangeCurrent[j].SetUserParams(22, bufRangeNext[j].UserParams(22));
                }

                current = current.Concat(bufRangeCurrent).Concat(NextRemains).ToList();

                worker = current.Distinct(new MyEqualityComparerFullUr()).ToList();

                logTextBox_Update("Проверен: " + file[0] + "\\..\\" + file[file.Length - 3]
                                  + "\\" + file[file.Length - 2]
                                  + "\\" + file[file.Length - 1]);
                Label5_Update("");
            }

            Label4_Update("Этап 3 из 4");
            logTextBox_Update("\nЗавершена проверка. Готовим выходныой файл...\n");
            worker = worker.Where((u) => { return u.КонПокДата.NotInvalidText(); }).OrderBy(n => n.UserParams(0)).ThenBy(n => n.UserParams(3)).ToList();

            //Групировка по ЗаводскомуНомеруСчетчика + ТрафинойЗонеСуток
            if (Form1.CounterGroup)
            {
                List<UserUr> subWorker = new List<UserUr>();

                var grp = from u in worker
                    group u by new { p1 = u.UserParams(17), p2 = u.UserParams(15) };

                foreach (var us in grp)
                {
                    var lastDate = us.ToList().TheEarliest(21, false).UserParams(21);
                    var lastValue = us.ToList().TheEarliest(21, false).UserParams(22);
                    var u = us.ToList().TheEarliest(19, true);

                    u.SetUserParams(21, lastDate);
                    u.SetUserParams(22, lastValue);

                    subWorker.Add(u);
                }

                subWorker = subWorker.OrderBy(n => n.UserParams(0)).ThenBy(n => n.UserParams(3)).ToList();

                worker = subWorker;
            }

            //Вычисление суммарного расхода за найденый период показаний
            foreach (var u in worker)
            {
                if (u.UserParams(20).Contains(".")) u.SetUserParams(20, u.UserParams(20).Replace(".", ","));
                if (u.UserParams(22).Contains(".")) u.SetUserParams(22, u.UserParams(22).Replace(".", ","));
                if (!u.UserParams(20).NotInvalidText()) u.SetUserParams(20, "0");
                if (!u.UserParams(22).NotInvalidText()) u.SetUserParams(22, "0");

                u.SetUserParams(23,
                    (Convert.ToDouble(u.UserParams(22)) - Convert.ToDouble(u.UserParams(20))).ToString());
            }
            return worker;
        }

    }
}
