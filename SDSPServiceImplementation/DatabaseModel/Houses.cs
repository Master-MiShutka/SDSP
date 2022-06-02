using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "Houses"), DataContract(IsReference = true)]
    [Serializable]
    public class Houses : EntityObject
    {
        private int _ID;
        private int? _Street_ID;
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
        public int? Street_ID
        {
            get
            {
                return this._Street_ID;
            }
            set
            {
                this.ReportPropertyChanging("Street_ID");
                this._Street_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Street_ID");
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
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_HousesFlats", "Flats"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<Flats> Flats
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Flats>("AskueModel.fgn_key_HousesFlats", "Flats");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Flats>("AskueModel.fgn_key_HousesFlats", "Flats", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_StreetsHouses", "Streets"), DataMember, SoapIgnore, XmlIgnore]
        public Streets Streets
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Streets>("AskueModel.fgn_key_StreetsHouses", "Streets").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Streets>("AskueModel.fgn_key_StreetsHouses", "Streets").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<Streets> StreetsReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Streets>("AskueModel.fgn_key_StreetsHouses", "Streets");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Streets>("AskueModel.fgn_key_StreetsHouses", "Streets", value);
                }
            }
        }
        public static Houses CreateHouses(int id)
        {
            return new Houses
            {
                ID = id
            };
        }
    }
}
