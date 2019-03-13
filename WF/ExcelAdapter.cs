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
                Files = new string[] { };
                Files = fileDialog.FileNames;
                fileDialog.Dispose();
                return Files;
            }
            fileDialog.Dispose();
            return null;
        }

        /// <summary>
        /// Enumerates a collection in parallel and calls an async method on each item. Useful for making 
        /// parallel async calls, e.g. independent web requests when the degree of parallelism needs to be
        /// limited.
        /// </summary> 
        public static Task ForEachAsync<T>(this IEnumerable<T> source, int degreeOfParalellism, Func<T, Task> action)
        {
            return Task.WhenAll(Partitioner.Create(source).GetPartitions(degreeOfParalellism).Select(partition => Task.Run(async () =>
            {
                using (partition)
                    while (partition.MoveNext())
                        await action(partition.Current);
            })));
        }
    }
}
