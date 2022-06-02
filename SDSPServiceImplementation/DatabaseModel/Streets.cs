using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "Streets"), DataContract(IsReference = true)]
    [Serializable]
    public class Streets : EntityObject
    {
        private int _ID;
        private int? _Place_ID;
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
        public int? Place_ID
        {
            get
            {
                return this._Place_ID;
            }
            set
            {
                this.ReportPropertyChanging("Place_ID");
                this._Place_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Place_ID");
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
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_StreetsHouses", "Houses"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<Houses> Houses
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Houses>("AskueModel.fgn_key_StreetsHouses", "Houses");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Houses>("AskueModel.fgn_key_StreetsHouses", "Houses", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_PlacesStreets", "Places"), DataMember, SoapIgnore, XmlIgnore]
        public Places Places
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Places>("AskueModel.fgn_key_PlacesStreets", "Places").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Places>("AskueModel.fgn_key_PlacesStreets", "Places").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<Places> PlacesReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Places>("AskueModel.fgn_key_PlacesStreets", "Places");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Places>("AskueModel.fgn_key_PlacesStreets", "Places", value);
                }
            }
        }
        public static Streets CreateStreets(int id)
        {
            return new Streets
            {
                ID = id
            };
        }
    }
}
