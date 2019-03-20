using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace WF
{
    public static class ExcelAdapter
    {
        private static string[] Files;

        /// <summary>
        /// Возвращает массив строк-путей к выбранным файлам
        /// </summary>
        /// <param name="IsExcel">Выбор только Excel файлов</param>
        /// <returns></returns>
        public static string[] Open(bool IsExcel)
        {
            string filt = IsExcel ? "Excel Files|*.xls;*.xlsx;*.xlsm" : "All files|*.*";
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = filt,
                Multiselect = true
            };

            if (fileDialog.ShowDialog() != DialogResult.Cancel)
            {
                Files = fileDialog.FileNames;
                fileDialog.Dispose();
                return Files;
            }
            fileDialog.Dispose();
            return null;
        }
    }
}
