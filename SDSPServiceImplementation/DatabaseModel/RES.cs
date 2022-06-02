using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "RES"), DataContract(IsReference = true)]
    [Serializable]
    public class RES : EntityObject
    {
        private int _ID;
        private int? _Fes_ID;
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
        public int? Fes_ID
        {
            get
            {
                return this._Fes_ID;
            }
            set
            {
                this.ReportPropertyChanging("Fes_ID");
                this._Fes_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Fes_ID");
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
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_FESRES", "FES"), DataMember, SoapIgnore, XmlIgnore]
        public FES FES
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<FES>("AskueModel.fgn_key_FESRES", "FES").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<FES>("AskueModel.fgn_key_FESRES", "FES").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<FES> FESReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<FES>("AskueModel.fgn_key_FESRES", "FES");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<FES>("AskueModel.fgn_key_FESRES", "FES", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_RESPlaces", "Places"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<Places> Places
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Places>("AskueModel.fgn_key_RESPlaces", "Places");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Places>("AskueModel.fgn_key_RESPlaces", "Places", value);
                }
            }
        }
        public static RES CreateRES(int id)
        {
            return new RES
            {
                ID = id
            };
        }
    }
}
