using System;
using System.Collections.Generic;

namespace WF
{
    public class UserUr : IEquatable<UserUr>
    {
        public string Организация { get; set; }
        public string ТипПеретока { get; set; }
        public string НомерЛицСчета { get; set; }
        public string КодТочкиУчета { get; set; }
        public string Наименование { get; set; }
        public string ПС { get; set; }
        public string ВЛ_110кВ { get; set; }
        public string ТП { get; set; }
        public string Фидер { get; set; }
        public string ОбъектПотребления { get; set; }
        public string МетодУчета { get; set; }
        public string МестоУстУчета { get; set; }
        public string ВидУчета { get; set; }
        public string ВидЭнергии { get; set; }
        public string ТипСч { get; set; }
        public string ЗавНомерСч { get; set; }
        public string ГруппаПотр { get; set; }
        public string ТарифЗонаСуток { get; set; }
        public string ТарифНапр { get; set; }
        public string НачПокДата { get; set; }
        public string НачПок { get; set; }
        public string КонПокДата { get; set; }
        public string КончПок { get; set; }
        public string РазностьПок { get; set; }
        public string КоэфПересч { get; set; }
        public string Расход { get; set; }
        public string ПроцПотерь { get; set; }
        public string Потери { get; set; }
        public string ВН { get; set; }
        public string СН1 { get; set; }
        public string СН2 { get; set; }
        public string НН { get; set; }
        public string ВсегоЭЭ { get; set; }
        public string ЕдИзм { get; set; }
        public string ВидРабот { get; set; }
        public string Примечание { get; set; }
        public string ТСО { get; set; }

        /// <summary>
        /// Устанавливает свойства объекта UserUr в порядке итерации
        /// </summary>
        /// <param name="i">Номер для обращения</param>
        /// <param name="value">Устанавливаемое значение</param>
        public void SetUserParams(int i, string value)
        {
            switch (i)
            {
                case 0:  Организация = value; break;
                case 1:  ТипПеретока = value; break;
                case 2:  НомерЛицСчета = value; break;
                case 3:  КодТочкиУчета = value; break;
                case 4:  Наименование = value; break;
                case 5:  ПС = value; break;
                case 6:  ВЛ_110кВ = value; break;
                case 7:  ТП = value; break;
                case 8:  Фидер = value; break;
                case 9:  ОбъектПотребления = value; break;
                case 10: МетодУчета = value; break;
                case 11: МестоУстУчета = value; break;
                case 12: ВидУчета = value; break;
                case 13: ВидЭнергии = value; break;
                case 14: ТипСч = value; break;
                case 15: ЗавНомерСч = value; break;
                case 16: ГруппаПотр = value; break;
                case 17: ТарифЗонаСуток = value; break;
                case 18: ТарифНапр = value; break;
                case 19: НачПокДата = value; break;
                case 20: НачПок = value; break;
                case 21: КонПокДата = value; break;
                case 22: КончПок = value; break;
                case 23: РазностьПок = value; break;
                case 24: КоэфПересч = value; break;
                case 25: Расход = value; break;
                case 26: ПроцПотерь = value; break;
                case 27: Потери = value; break;
                case 28: ВН = value; break;
                case 29: СН1 = value; break;
                case 30: СН2 = value; break;
                case 31: НН = value; break;
                case 32: ВсегоЭЭ = value; break;
                case 33: ЕдИзм = value; break;
                case 34: ВидРабот = value; break;
                case 35: Примечание = value; break;
                case 36: ТСО = value; break;
            }
        }

        /// <summary>
        /// Возвращает значение свойства объекта UserUr по заданному номеру
        /// </summary>
        /// <param name="i">Номер свойства от 0 до 36</param>
        /// <returns></returns>
        public string UserParams(int i)
        {
            string obj = "";
            switch (i)
            {
                case 0: obj =  Организация; break;
                case 1: obj =  ТипПеретока; break;
                case 2: obj =  НомерЛицСчета; break;
                case 3: obj =  КодТочкиУчета; break;
                case 4: obj =  Наименование; break;
                case 5: obj =  ПС; break;
                case 6: obj =  ВЛ_110кВ; break;
                case 7: obj =  ТП; break;
                case 8: obj =  Фидер; break;
                case 9: obj =  ОбъектПотребления; break;
                case 10: obj = МетодУчета; break;
                case 11: obj = МестоУстУчета; break;
                case 12: obj = ВидУчета; break;
                case 13: obj = ВидЭнергии; break;
                case 14: obj = ТипСч; break;
                case 15: obj = ЗавНомерСч; break;
                case 16: obj = ГруппаПотр; break;
                case 17: obj = ТарифЗонаСуток; break;
                case 18: obj = ТарифНапр; break;
                case 19: obj = НачПокДата; break;
                case 20: obj = НачПок; break;
                case 21: obj = КонПокДата; break;
                case 22: obj = КончПок; break;
                case 23: obj = РазностьПок; break;
                case 24: obj = КоэфПересч; break;
                case 25: obj = Расход; break;
                case 26: obj = ПроцПотерь; break;
                case 27: obj = Потери; break;
                case 28: obj = ВН; break;
                case 29: obj = СН1; break;
                case 30: obj = СН2; break;
                case 31: obj = НН; break;
                case 32: obj = ВсегоЭЭ; break;
                case 33: obj = ЕдИзм; break;
                case 34: obj = ВидРабот; break;
                case 35: obj = Примечание; break;
                case 36: obj = ТСО; break;
            }
            return obj;
        }

        public bool Equals(UserUr other)
        {
            return ТипПеретока == other.ТипПеретока &&
                   НомерЛицСчета == other.НомерЛицСчета &&
                   КодТочкиУчета == other.КодТочкиУчета &&
                   Наименование == other.Наименование &&
                   ТарифЗонаСуток == other.ТарифЗонаСуток &&
                   ТипСч == other.ТипСч &&
                   ЗавНомерСч == other.ЗавНомерСч;
        }

        public bool Equals(UserUr other, bool full)
        {
            if (full)
            {
                return ТипПеретока == other.ТипПеретока &&
                       НомерЛицСчета == other.НомерЛицСчета &&
                       КодТочкиУчета == other.КодТочкиУчета &&
                       Наименование == other.Наименование &&
                       ТарифЗонаСуток == other.ТарифЗонаСуток &&
                       ТипСч == other.ТипСч &&
                       ЗавНомерСч == other.ЗавНомерСч &&
                       НачПок == other.НачПок &&
                       НачПокДата == other.НачПокДата &&
                       КончПок == other.КончПок &&
                       КонПокДата == other.КонПокДата;
            }

            return false;

        }

        public override int GetHashCode()
        {
            var hashCode = -1541803655;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ТипПеретока);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(НомерЛицСчета);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(КодТочкиУчета);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Наименование);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ТипСч);
            return hashCode;
        }
    }
}
/*
Организация
ТипПеретока
НомерЛицСчета
КодТочкиУчета
Наименование
ПС
ВЛ_110кВ
ТП
Фидер
ОбъектПотребления
МетодУчета
МестоУстУчета
ВидУчета
ВидЭнергии
ТипСч
ЗавНомерСч
ГруппаПотр
ТарифЗонаСуток
ТарифНапр
НачПокДата
НачПок
КонПокДата
КончПок
РазностьПок
КоэфПересч
Расход
ПроцПотерь
Потери
ВН
СН1
СН2
НН
ВсегоЭЭ
ЕдИзм
ВидРабот
Примечание
ТСО
*/