using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "Counters_type"), DataContract(IsReference = true)]
    [Serializable]
    public partial class Counters_type : EntityObject
    {
        private int _ID;
        private int? _Manufacturer_ID;
        private string _Name;
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
        public int? Manufacturer_ID
        {
            get
            {
                return this._Manufacturer_ID;
            }
            set
            {
                this.ReportPropertyChanging("Manufacturer_ID");
                this._Manufacturer_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Manufacturer_ID");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this.ReportPropertyChanging("Name");
                this._Name = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Name");
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_Counters_typeCounters", "Counters"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<Counters> Counters
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Counters>("AskueModel.fgn_key_Counters_typeCounters", "Counters");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Counters>("AskueModel.fgn_key_Counters_typeCounters", "Counters", value);
                }
            }
        }
        public static Counters_type CreateCounters_type(int id)
        {
            return new Counters_type
            {
                ID = id
            };
        }
    }
}
