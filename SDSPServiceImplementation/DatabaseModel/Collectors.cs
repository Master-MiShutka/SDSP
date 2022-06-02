using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "Collectors"), DataContract(IsReference = true)]
    [Serializable]
    public class Collectors : EntityObject
    {
        private int _ID;
        private int? _Collectors_Type_ID;
        private int? _Channel_ID;
        private int? _Collectors_ID;
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
        public int? Collectors_Type_ID
        {
            get
            {
                return this._Collectors_Type_ID;
            }
            set
            {
                this.ReportPropertyChanging("Collectors_Type_ID");
                this._Collectors_Type_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Collectors_Type_ID");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public int? Channel_ID
        {
            get
            {
                return this._Channel_ID;
            }
            set
            {
                this.ReportPropertyChanging("Channel_ID");
                this._Channel_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Channel_ID");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public int? Collectors_ID
        {
            get
            {
                return this._Collectors_ID;
            }
            set
            {
                this.ReportPropertyChanging("Collectors_ID");
                this._Collectors_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Collectors_ID");
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
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_CollectorsCalcPoints", "CalcPoints"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<CalcPoints> CalcPoints
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<CalcPoints>("AskueModel.fgn_key_CollectorsCalcPoints", "CalcPoints");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<CalcPoints>("AskueModel.fgn_key_CollectorsCalcPoints", "CalcPoints", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_ChannelsCollectors", "Channels"), DataMember, SoapIgnore, XmlIgnore]
        public Channels Channels
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Channels>("AskueModel.fgn_key_ChannelsCollectors", "Channels").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Channels>("AskueModel.fgn_key_ChannelsCollectors", "Channels").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<Channels> ChannelsReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Channels>("AskueModel.fgn_key_ChannelsCollectors", "Channels");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Channels>("AskueModel.fgn_key_ChannelsCollectors", "Channels", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_CollectorsCollectors_Statuses", "Collectors_Statuses"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<Collectors_Statuses> Collectors_Statuses
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Collectors_Statuses>("AskueModel.fgn_key_CollectorsCollectors_Statuses", "Collectors_Statuses");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Collectors_Statuses>("AskueModel.fgn_key_CollectorsCollectors_Statuses", "Collectors_Statuses", value);
                }
            }
        }
        public static Collectors CreateCollectors(int id)
        {
            return new Collectors
            {
                ID = id
            };
        }
    }
}
