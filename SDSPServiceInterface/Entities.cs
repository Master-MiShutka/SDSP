using System;
using System.Collections.Generic;

namespace SDSPServiceInterface.Entities
{
    
    public class Profile
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }
        
    /// <summary>
    /// Счетчик
    /// </summary>
    public class Counter
    {
        /// <summary>
        /// Наименование расчётной точки
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Тип счетчика
        /// </summary>
        public string Type
        {
            get;
            set;
        }
        /// <summary>
        /// Заводской номер
        /// </summary>
        public string SerialNumber
        {
            get;
            set;
        }
        /// <summary>
        /// Сетевой адрес
        /// </summary>
        public string NetworkAdress
        {
            get;
            set;
        }
        /// <summary>
        /// Состояние
        /// </summary>
        public string Status
        {
            get;
            set;
        }
        /// <summary>
        /// Начальные показания
        /// </summary>
        public float? StartIndications
        {
            get;
            set;
        }
        /// <summary>
        /// Конечные показания
        /// </summary>
        public float? EndIndications
        {
            get;
            set;
        }
        /// <summary>
        /// Текущая разница показаний
        /// </summary>
        public float? IndicationsDifference
        {
            get;
            set;
        }
        public float? PreviousIndicationsDifference
        {
            get;
            set;
        }
        /// <summary>
        /// Шкаф СДСП
        /// </summary>
        public SdspContainer Container
        {
            get;
            set;
        }
    }
    /// <summary>
    /// Департамент
    /// </summary>
    public class Departament
    {
        /// <summary>
        /// ИД
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// Наименование
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// Тип
        /// </summary>
        public DepartamentType DepartamentType
        {
            get;
            set;
        }
        /// <summary>
        /// Подчиненные департаменты
        /// </summary>
        public IEnumerable<Departament> ChildDepartaments
        {
            get;
            set;
        }
    }
    /// <summary>
    /// Тип департамента
    /// </summary>
    [Serializable()]
    public enum DepartamentType
    {
        Region,
        Fes,
        Res
    }
    /// <summary>
    /// Шкаф СДСП
    /// </summary>
    public class SdspContainer
    {
        /// <summary>
        /// Номер по порядку
        /// </summary>
        public int OrderNumber
        {
            get;
            set;
        }
        /// <summary>
        /// Адрес
        /// </summary>
        public string Street
        {
            get;
            set;
        }
        /// <summary>
        /// Наименование ВЛ-10 кВ и ТП 10/0,4
        /// </summary>
        public string TPName
        {
            get;
            set;
        }
        /// <summary>
        /// Телефонный номер
        /// </summary>
        public string DialNumber
        {
            get;
            set;
        }
        /// <summary>
        /// Состояние
        /// </summary>
        public string Status
        {
            get;
            set;
        }
        /// <summary>
        /// Дата и время последнего успешного опроса
        /// </summary>
        public DateTime? LastSession
        {
            get;
            set;
        }
        /// <summary>
        /// Список счечтиков
        /// </summary>
        public IEnumerable<Counter> Counters
        {
            get;
            set;
        }
    }
    /// <summary>
    /// Отчетная информация
    /// </summary>
    public class SdspInformation
    {
        /// <summary>
        /// Список систем
        /// </summary>
        public IEnumerable<SdspContainer> SdspContainers
        {
            get;
            set;
        }
        /// <summary>
        /// Дата создания отчета
        /// </summary>
        public DateTime ReportingDate
        {
            get;
            set;
        }
        public DateTime StartDate
        {
            get;
            set;
        }
        public DateTime EndDate
        {
            get;
            set;
        }
        /// <summary>
        /// Департамент
        /// </summary>
        public Departament Departament
        {
            get;
            set;
        }
        public float ReportingSumm
        {
            get;
            set;
        }
        public float PreviousSumm
        {
            get;
            set;
        }
        public int AccountPointsCount
        {
            get;
            set;
        }
        /// <summary>
        /// Количество систем
        /// </summary>
        public int ModemsCount
        {
            get { return (this.AnsweringModemsCount + this.NotAnsweringModemsCount); }
        }
        /// <summary>
        /// Количество успешно опрошенных систем
        /// </summary>
        public int AnsweringModemsCount
        {
            get;
            set;
        }
        /// <summary>
        /// Количество не опрошенных систем
        /// </summary>
        public int NotAnsweringModemsCount
        {
            get;
            set;
        }
        /// <summary>
        /// Количество счетчиков
        /// </summary>
        public int CountersCount
        {
            get { return (this.AnsweringCountersCount + this.NotAnsweringCountersCount);}
        }
        /// <summary>
        /// Количество опрошенных счетчиков
        /// </summary>
        public int AnsweringCountersCount
        {
            get;
            set;
        }
        /// <summary>
        /// Количество не опрошенных счетчиков
        /// </summary>
        public int NotAnsweringCountersCount
        {
            get;
            set;
        }
    }
}
