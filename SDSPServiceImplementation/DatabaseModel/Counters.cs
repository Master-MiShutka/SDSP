using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "Counters"), DataContract(IsReference = true)]
    [Serializable]
    public class Counters : EntityObject
    {
        private int _ID;
        private int? _CalcPoint_ID;
        private int? _Type_ID;
        private string _Serial;
        private string _Balance;
        private string _NetAdress;
        [EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
        public int ID
        {
            get
            {
                return this._ID;
            }
            set
            {
                if (this._ID != value)
                {
                    this.ReportPropertyChanging("ID");
                    this._ID = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("ID");
                }
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public int? CalcPoint_ID
        {
            get
            {
                return this._CalcPoint_ID;
            }
            set
            {
                this.ReportPropertyChanging("CalcPoint_ID");
                this._CalcPoint_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CalcPoint_ID");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public int? Type_ID
        {
            get
            {
                return this._Type_ID;
            }
            set
            {
                this.ReportPropertyChanging("Type_ID");
                this._Type_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Type_ID");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public string Serial
        {
            get
            {
                return this._Serial;
            }
            set
            {
                this.ReportPropertyChanging("Serial");
                this._Serial = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Serial");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public string Balance
        {
            get
            {
                return this._Balance;
            }
            set
            {
                this.ReportPropertyChanging("Balance");
                this._Balance = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Balance");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public string NetAdress
        {
            get
            {
                return this._NetAdress;
            }
            set
            {
                this.ReportPropertyChanging("NetAdress");
                this._NetAdress = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("NetAdress");
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_CalcPointsCounters", "CalcPoints"), DataMember, SoapIgnore, XmlIgnore]
        public CalcPoints CalcPoints
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<CalcPoints>("AskueModel.fgn_key_CalcPointsCounters", "CalcPoints").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<CalcPoints>("AskueModel.fgn_key_CalcPointsCounters", "CalcPoints").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<CalcPoints> CalcPointsReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<CalcPoints>("AskueModel.fgn_key_CalcPointsCounters", "CalcPoints");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<CalcPoints>("AskueModel.fgn_key_CalcPointsCounters", "CalcPoints", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_Counters_typeCounters", "Counters_type"), DataMember, SoapIgnore, XmlIgnore]
        public Counters_type Counters_type
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Counters_type>("AskueModel.fgn_key_Counters_typeCounters", "Counters_type").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Counters_type>("AskueModel.fgn_key_Counters_typeCounters", "Counters_type").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<Counters_type> Counters_typeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Counters_type>("AskueModel.fgn_key_Counters_typeCounters", "Counters_type");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Counters_type>("AskueModel.fgn_key_Counters_typeCounters", "Counters_type", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_CountersCounters_Statuses", "Counters_Statuses"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<Counters_Statuses> Counters_Statuses
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Counters_Statuses>("AskueModel.fgn_key_CountersCounters_Statuses", "Counters_Statuses");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Counters_Statuses>("AskueModel.fgn_key_CountersCounters_Statuses", "Counters_Statuses", value);
                }
            }
        }
        public static Counters CreateCounters(int id)
        {
            return new Counters
            {
                ID = id
            };
        }
    }
}
