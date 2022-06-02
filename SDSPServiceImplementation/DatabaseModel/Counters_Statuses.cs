using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "Counters_Statuses"), DataContract(IsReference = true)]
    [Serializable]
    public partial class Counters_Statuses : EntityObject
    {
        private int _id;
        private int? _Counter_id;
        private int? _code;
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
        public int? Counter_id
        {
            get
            {
                return this._Counter_id;
            }
            set
            {
                this.ReportPropertyChanging("Counter_id");
                this._Counter_id = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Counter_id");
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
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_CountersCounters_Statuses", "Counters"), DataMember, SoapIgnore, XmlIgnore]
        public Counters Counters
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Counters>("AskueModel.fgn_key_CountersCounters_Statuses", "Counters").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Counters>("AskueModel.fgn_key_CountersCounters_Statuses", "Counters").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<Counters> CountersReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Counters>("AskueModel.fgn_key_CountersCounters_Statuses", "Counters");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Counters>("AskueModel.fgn_key_CountersCounters_Statuses", "Counters", value);
                }
            }
        }
        public static Counters_Statuses CreateCounters_Statuses(int id)
        {
            return new Counters_Statuses
            {
                id = id
            };
        }
    }
}
