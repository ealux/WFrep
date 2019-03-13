using System;
using System.Collections.Generic;

namespace WF
{
    public class User : IEquatable<User>

    {
        public string НомерЛицСчета { get; set; }
        public string ФИО { get; set; }
        public string КодТочкиУчета { get; set; }
        public string Район_Город { get; set; }
        public string Поселок_Пункт { get; set; }
        public string Улица { get; set; }
        public string ДомНомер { get; set; }
        public string ДомЛитера { get; set; }
        public string Кватира { get; set; }
        public string ПС { get; set; }
        public string ВЛ_110кВ { get; set; }
        public string ТП { get; set; }
        public string Фидер { get; set; }
        public string КатегорияСтроения { get; set; }
        public string ВидУчета { get; set; }
        public string ТипСч { get; set; }
        public string Значность { get; set; }
        public string ЗавНомерСч { get; set; }
        public string ТарифЗонаСуток { get; set; }
        public string ТарифНапр { get; set; }
        public string НачПокДата { get; set; }
        public string НачПок { get; set; }
        public string КонПокДата { get; set; }
        public string КончПок { get; set; }
        public string РазностьПок { get; set; }
        public string КоэфТранс { get; set; }
        public string Потери { get; set; }
        public string Расход { get; set; }
        public string ВидРабот { get; set; }
        public string Примечание { get; set; }
        public string ТСО { get; set; }
        public string ПервВхождение { get; set; }
        public string ПослВхождение { get; set; }

        /// <summary>
        /// Устанавливает свойства объекта User в порядке итерации
        /// </summary>
        /// <param name="i">Номер для обращения</param>
        /// <param name="value">Устанавливаемое значение</param>
        public void SetUserParams(int i, string value)
        {
            switch (i)
            {
                case 0:   НомерЛицСчета = value; break;
                case 1:   ФИО = value; break;
                case 2:   КодТочкиУчета = value; break;
                case 3:   Район_Город = value;  break;
                case 4:   Поселок_Пункт = value; break;
                case 5:   Улица = value; break;
                case 6:   ДомНомер = value; break;
                case 7:   ДомЛитера = value; break;
                case 8:   Кватира = value; break;
                case 9:   ПС = value; break;
                case 10:  ВЛ_110кВ = value; break;
                case 11:  ТП = value; break;
                case 12:  Фидер = value; break;
                case 13:  КатегорияСтроения = value; break;
                case 14:  ВидУчета = value; break;
                case 15:  ТипСч = value; break;
                case 16:  Значность = value; break;
                case 17:  ЗавНомерСч = value; break;
                case 18:  ТарифЗонаСуток = value; break;
                case 19:  ТарифНапр = value; break;
                case 20:  НачПокДата = value; break;
                case 21:  НачПок = value; break;
                case 22:  КонПокДата = value; break;
                case 23:  КончПок = value; break;
                case 24:  РазностьПок = value; break;
                case 25:  КоэфТранс = value; break;
                case 26:  Потери = value; break;
                case 27:  Расход = value; break;
                case 28:  ВидРабот = value;  break;
                case 29:  Примечание = value; break;
                case 30:  ТСО = value; break;
                case 31:  ПервВхождение = value; break;
                case 32:  ПослВхождение = value; break;
            }
        }

        /// <summary>
        /// Возвращает значение свойства объекта User по заданному номеру
        /// </summary>
        /// <param name="i">Номер свойства от 0 до 30</param>
        /// <returns></returns>
        public string UserParams(int i)
        {
            string obj = "";
            switch (i)
            {
                case 0: obj =  НомерЛицСчета; break;
                case 1: obj =  ФИО; break;
                case 2: obj =  КодТочкиУчета; break;
                case 3: obj =  Район_Город; break;
                case 4: obj =  Поселок_Пункт; break;
                case 5: obj =  Улица; break;
                case 6: obj =  ДомНомер; break;
                case 7: obj =  ДомЛитера; break;
                case 8: obj =  Кватира; break;
                case 9: obj =  ПС; break;
                case 10: obj = ВЛ_110кВ; break;
                case 11: obj = ТП; break;
                case 12: obj = Фидер; break;
                case 13: obj = КатегорияСтроения; break;
                case 14: obj = ВидУчета; break;
                case 15: obj = ТипСч; break;
                case 16: obj = Значность; break;
                case 17: obj = ЗавНомерСч; break;
                case 18: obj = ТарифЗонаСуток; break;
                case 19: obj = ТарифНапр; break;
                case 20: obj = НачПокДата; break;
                case 21: obj = НачПок; break;
                case 22: obj = КонПокДата; break;
                case 23: obj = КончПок; break;
                case 24: obj = РазностьПок; break;
                case 25: obj = КоэфТранс; break;
                case 26: obj = Потери; break;
                case 27: obj = Расход; break;
                case 28: obj = ВидРабот; break;
                case 29: obj = Примечание; break;
                case 30: obj = ТСО; break;
                case 31: obj = ПервВхождение; break;
                case 32: obj = ПослВхождение; break;
            }
            return obj;
        }

        public bool Equals(User other)
        {
            return НомерЛицСчета == other.НомерЛицСчета &&
                   ФИО == other.ФИО &&
                   КодТочкиУчета == other.КодТочкиУчета &&
                   ТарифЗонаСуток == other.ТарифЗонаСуток &&
                   ТипСч == other.ТипСч &&
                   ЗавНомерСч == other.ЗавНомерСч;
        }

        public bool Equals(User other, bool full)
        {
            if (full)
            {
                return НомерЛицСчета == other.НомерЛицСчета &&
                       ФИО == other.ФИО &&
                       КодТочкиУчета == other.КодТочкиУчета &&
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
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(НомерЛицСчета);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ФИО);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(КодТочкиУчета);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ТипСч);
            return hashCode;
        }
    }
}
