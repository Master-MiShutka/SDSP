using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "Places"), DataContract(IsReference = true)]
    [Serializable]
    public class Places : EntityObject
    {
        private int _ID;
        private int? _Res_ID;
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
        public int? Res_ID
        {
            get
            {
                return this._Res_ID;
            }
            set
            {
                this.ReportPropertyChanging("Res_ID");
                this._Res_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Res_ID");
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
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_PlacesStreets", "Streets"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<Streets> Streets
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Streets>("AskueModel.fgn_key_PlacesStreets", "Streets");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Streets>("AskueModel.fgn_key_PlacesStreets", "Streets", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_RESPlaces", "RES"), DataMember, SoapIgnore, XmlIgnore]
        public RES RES
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<RES>("AskueModel.fgn_key_RESPlaces", "RES").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<RES>("AskueModel.fgn_key_RESPlaces", "RES").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<RES> RESReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<RES>("AskueModel.fgn_key_RESPlaces", "RES");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<RES>("AskueModel.fgn_key_RESPlaces", "RES", value);
                }
            }
        }
        public static Places CreatePlaces(int id)
        {
            return new Places
            {
                ID = id
            };
        }
    }
}
