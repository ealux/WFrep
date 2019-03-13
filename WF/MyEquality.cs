using System.Collections.Generic;
namespace WF
{
    //Проверка свойств по объекту, К КОТОРОМУ ПРИМЕНЯЕТСЯ РАСШИРЕНИЕ
    class MyEqualityComparer1to2 : IEqualityComparer<User>
    {
        public bool Equals(User item1, User item2)
        {
            return item1.Equals(item2) &&
                   item1.UserParams(15).NotInvalidText() &&
                   item2.UserParams(15).NotInvalidText() &&
                   item1.UserParams(22).NotInvalidText() &&
                   item1.UserParams(23).NotInvalidText();
        }

        public int GetHashCode(User obj)
        {
            return (obj.UserParams(1).Length + obj.UserParams(2).Length + obj.UserParams(15).Length).GetHashCode();
        }
    }

    //Проверка свойств по объекту, С КОТОРЫМ ПРОИЗВОДИТСЯ СРАВНЕНИЕ 
    class MyEqualityComparer2to1 : IEqualityComparer<User>
    {
        public bool Equals(User item1, User item2)
        {
            return item1.Equals(item2) &&
                   item1.UserParams(15).NotInvalidText() &&
                   item2.UserParams(15).NotInvalidText() &&
                   item2.UserParams(22).NotInvalidText() &&
                   item2.UserParams(23).NotInvalidText();
        }

        public int GetHashCode(User obj)
        {
            return (obj.UserParams(1).Length + obj.UserParams(2).Length + obj.UserParams(15).Length).GetHashCode();
        }
    }

    //Проверка без параметров показаний
    class MyEqualityComparerWithoutData : IEqualityComparer<User>
    {
        public bool Equals(User item1, User item2)
        {
            return item1.Equals(item2) &&
                   item1.UserParams(15).NotInvalidText() &&
                   item2.UserParams(15).NotInvalidText()&&
                   item1.UserParams(20).NotInvalidText()&&
                   item2.UserParams(20).NotInvalidText();
        }

        public int GetHashCode(User obj)
        {
            return (obj.UserParams(1).Length + obj.UserParams(2).Length + obj.UserParams(15).Length).GetHashCode();
        }
    }

    //Проверка по ВСЕМ* параметрам
    class MyEqualityComparerFull : IEqualityComparer<User>
    {
        public bool Equals(User item1, User item2)
        {
            return item1.Equals(item2, true) &&
                   item1.UserParams(15).NotInvalidText() &&
                   item2.UserParams(15).NotInvalidText();
        }

        public int GetHashCode(User obj)
        {
            return (obj.UserParams(1).Length + obj.UserParams(2).Length + obj.UserParams(15).Length).GetHashCode();
        }
    }
}
