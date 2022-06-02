using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "FES"), DataContract(IsReference = true)]
    [Serializable]
    public class FES : EntityObject
    {
        private int _ID;
        private int? _Region_ID;
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
        public int? Region_ID
        {
            get
            {
                return this._Region_ID;
            }
            set
            {
                this.ReportPropertyChanging("Region_ID");
                this._Region_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Region_ID");
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
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_FESRES", "RES"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<RES> RES
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<RES>("AskueModel.fgn_key_FESRES", "RES");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<RES>("AskueModel.fgn_key_FESRES", "RES", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_RegionFES", "Regions"), DataMember, SoapIgnore, XmlIgnore]
        public Regions Regions
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Regions>("AskueModel.fgn_key_RegionFES", "Regions").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Regions>("AskueModel.fgn_key_RegionFES", "Regions").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<Regions> RegionsReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Regions>("AskueModel.fgn_key_RegionFES", "Regions");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Regions>("AskueModel.fgn_key_RegionFES", "Regions", value);
                }
            }
        }
        public static FES CreateFES(int id)
        {
            return new FES
            {
                ID = id
            };
        }
    }
}
