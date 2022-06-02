using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "Channels"), DataContract(IsReference = true)]
    [Serializable]
    public class Channels : EntityObject
    {
        private int _ID;
        private int? _Type_ID;
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
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_ChannelsCollectors", "Collectors"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<Collectors> Collectors
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Collectors>("AskueModel.fgn_key_ChannelsCollectors", "Collectors");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Collectors>("AskueModel.fgn_key_ChannelsCollectors", "Collectors", value);
                }
            }
        }
        public static Channels CreateChannels(int id)
        {
            return new Channels
            {
                ID = id
            };
        }
    }
}
