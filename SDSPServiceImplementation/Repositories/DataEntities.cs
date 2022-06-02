using System;
namespace SDSPServiceImplementation.Repositories.DataEntities
{
    public class AccountPoint
    {
        public int ColId
        {
            get;
            set;
        }
        public int CalcId
        {
            get;
            set;
        }
        public string CounterName
        {
            get;
            set;
        }
        public string CounterTypeName
        {
            get;
            set;
        }
        public string CounterSerial
        {
            get;
            set;
        }
        public string TpName
        {
            get;
            set;
        }
        public string DialNumber
        {
            get;
            set;
        }
        public string NetworkAdress
        {
            get;
            set;
        }
        public int? ColStatus
        {
            get;
            set;
        }
        public DateTime? LastSession
        {
            get;
            set;
        }
        public int? CounterStatus
        {
            get;
            set;
        }
        public string Street
        {
            get;
            set;
        }
    }
}
