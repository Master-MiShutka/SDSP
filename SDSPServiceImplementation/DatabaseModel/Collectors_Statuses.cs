using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "Collectors_Statuses"), DataContract(IsReference = true)]
    [Serializable]
    public partial class Collectors_Statuses : EntityObject
    {
        private int _id;
        private int? _Collector_id;
        private int? _code;
        private DateTime? _time;
        [EdmScalarProperty(EntityKeyProperty = true, IsNullable = false), DataMember]
        public int id
        {
            get
            {
                return this._id;
            }
            set
            {
                if (this._id != value)
                {
                    this.ReportPropertyChanging("id");
                    this._id = StructuralObject.SetValidValue(value);
                    this.ReportPropertyChanged("id");
                }
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public int? Collector_id
        {
            get
            {
                return this._Collector_id;
            }
            set
            {
                this.ReportPropertyChanging("Collector_id");
                this._Collector_id = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Collector_id");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public int? code
        {
            get
            {
                return this._code;
            }
            set
            {
                this.ReportPropertyChanging("code");
                this._code = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("code");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public DateTime? time
        {
            get
            {
                return this._time;
            }
            set
            {
                this.ReportPropertyChanging("time");
                this._time = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("time");
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_CollectorsCollectors_Statuses", "Collectors"), DataMember, SoapIgnore, XmlIgnore]
        public Collectors Collectors
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Collectors>("AskueModel.fgn_key_CollectorsCollectors_Statuses", "Collectors").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Collectors>("AskueModel.fgn_key_CollectorsCollectors_Statuses", "Collectors").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<Collectors> CollectorsReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Collectors>("AskueModel.fgn_key_CollectorsCollectors_Statuses", "Collectors");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Collectors>("AskueModel.fgn_key_CollectorsCollectors_Statuses", "Collectors", value);
                }
            }
        }
        public static Collectors_Statuses CreateCollectors_Statuses(int id)
        {
            return new Collectors_Statuses
            {
                id = id
            };
        }
    }
}
