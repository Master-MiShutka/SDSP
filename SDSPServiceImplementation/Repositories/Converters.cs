using SDSPServiceImplementation.DatabaseModel;
using SDSPServiceImplementation.Repositories.DataEntities;
using SDSPServiceInterface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SDSPServiceImplementation.Repositories.Converters
{
    public class IndicationsComparer : IComparer<Indications>
    {
        public int Compare(Indications x, Indications y)
        {
            int result;
            if (x.CalcPoint_ID > y.CalcPoint_ID)
            {
                result = 1;
            }
            else
            {
                if (x.CalcPoint_ID < y.CalcPoint_ID)
                {
                    result = -1;
                }
                else
                {
                    result = 0;
                }
            }
            return result;
        }
    }

    public class SdspContainersConverter
    {
        public IEnumerable<SdspContainer> ConvertFromIndicationsAndAccountPoints(IEnumerable<AccountPoint> accountPoints, IEnumerable<Indications> indications, DateTime firstDate, DateTime lastDate)
        {
            List<SdspContainer> list = new List<SdspContainer>();
            IEnumerable<IGrouping<int, AccountPoint>> enumerable =
                from p in accountPoints
                group p by p.ColId;
            Indications[] array = (indications as Indications[]) ?? indications.ToArray<Indications>();
            Array.Sort<Indications>(array, new IndicationsComparer());
            foreach (IGrouping<int, AccountPoint> current in enumerable)
            {
                SdspContainer sdspContainer = this.GetSdspContainer(current);
                sdspContainer.Counters = new List<Counter>();
                foreach (AccountPoint current2 in current)
                {
                    this.AddAccountPointToContainer(array, firstDate, lastDate, sdspContainer, current2);
                }
                list.Add(sdspContainer);
            }
            return list;
        }
        private SdspContainer GetSdspContainer(IGrouping<int, AccountPoint> collector)
        {
            SdspContainer sdspContainer = new SdspContainer();
            AccountPoint accountPoint = collector.First<AccountPoint>();
            sdspContainer.Street = accountPoint.Street;
            sdspContainer.TPName = accountPoint.TpName;
            sdspContainer.DialNumber = accountPoint.DialNumber;
            sdspContainer.LastSession = accountPoint.LastSession;
            int valueOrDefault = accountPoint.ColStatus.GetValueOrDefault();
            int? num = -1;
            if (num.HasValue)
            {
                switch (valueOrDefault)
                {
                    case 0:
                        sdspContainer.Status = "OK";
                        break;
                    case 1:
                        sdspContainer.Status = "Счетчики не отвечают";
                        break;
                    case 2:
                        sdspContainer.Status = "Модем не отвечает";
                        break;
                    case 3:
                        sdspContainer.Status = "Счетчики не отвечают";
                        break;
                }
            }
            return sdspContainer;
        }
        private void AddAccountPointToContainer(Indications[] indications, DateTime firstDate, DateTime lastDate, SdspContainer container, AccountPoint accountPoint)
        {
            Counter counter = new Counter();
            counter.Type = accountPoint.CounterTypeName;
            counter.SerialNumber = accountPoint.CounterSerial;
            counter.Name = accountPoint.CounterName;
            counter.NetworkAdress = accountPoint.NetworkAdress;
            if (accountPoint.CounterStatus.HasValue)
            {
                counter.Status = ((accountPoint.CounterStatus == 0) ? "OK" : "Не отвечает");
            }
            int num = -1;
            Indications previousIndication = SdspContainersConverter.GetPreviousIndication(indications, firstDate, accountPoint, ref num);
            Indications previousIndication2 = SdspContainersConverter.GetPreviousIndication(indications, firstDate, accountPoint, ref num);
            if (previousIndication2 != null)
            {
                counter.StartIndications = new float?((float)((int)previousIndication2.Tr0.Value));
            }
            Indications previousIndication3 = SdspContainersConverter.GetPreviousIndication(indications, lastDate, accountPoint, ref num);
            if (previousIndication3 != null)
            {
                counter.EndIndications = new float?((float)((int)previousIndication3.Tr0.Value));
            }
            if (counter.StartIndications.HasValue && counter.EndIndications.HasValue)
            {
                Counter arg_167_0 = counter;
                float? num2 = counter.EndIndications;
                float? previousIndications = counter.StartIndications;
                arg_167_0.IndicationsDifference = ((num2.HasValue & previousIndications.HasValue) ? new float?(num2.GetValueOrDefault() - previousIndications.GetValueOrDefault()) : null);
                num2 = counter.IndicationsDifference;
                if (num2.GetValueOrDefault() < 0f && num2.HasValue)
                {
                    counter.IndicationsDifference = new float?(float.NaN);
                }
            }
            if (counter.StartIndications.HasValue && previousIndication != null)
            {
                Counter arg_20C_0 = counter;
                float? num2 = counter.StartIndications;
                float num3 = (float)((int)previousIndication.Tr0.Value);
                arg_20C_0.PreviousIndicationsDifference = (num2.HasValue ? new float?(num2.GetValueOrDefault() - num3) : null);
                num2 = counter.PreviousIndicationsDifference;
                if (num2.GetValueOrDefault() < 0f && num2.HasValue)
                {
                    counter.PreviousIndicationsDifference = new float?(float.NaN);
                }
            }
            counter.Container = container;
            (container.Counters as List<Counter>).Add(counter);
        }
        private static Indications GetPreviousIndication(Indications[] indications, DateTime prevDate, AccountPoint accountPoint, ref int index)
        {
            if (index < 0)
            {
                index = Array.BinarySearch<Indications>(indications, new Indications
                {
                    CalcPoint_ID = new int?(accountPoint.CalcId)
                }, new IndicationsComparer());
            }
            Indications result;
            if (index < 0)
            {
                result = null;
            }
            else
            {
                for (int i = index; i >= 0; i--)
                {
                    if (indications[i].CalcPoint_ID != accountPoint.CalcId)
                    {
                        break;
                    }
                    if (indications[i].Ts.Value.CompareTo(prevDate) == 0)
                    {
                        result = indications[i];
                        return result;
                    }
                }
                for (int i = index; i < indications.Length; i++)
                {
                    if (indications[i].CalcPoint_ID != accountPoint.CalcId)
                    {
                        break;
                    }
                    if (indications[i].Ts.Value.CompareTo(prevDate) == 0)
                    {
                        result = indications[i];
                        return result;
                    }
                }
                result = null;
            }
            return result;
        }
    }
}
