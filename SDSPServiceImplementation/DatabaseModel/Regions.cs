using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "Regions"), DataContract(IsReference = true)]
    [Serializable]
    public class Regions : EntityObject
    {
        private int _ID;
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
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_RegionFES", "FES"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<FES> FES
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<FES>("AskueModel.fgn_key_RegionFES", "FES");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<FES>("AskueModel.fgn_key_RegionFES", "FES", value);
                }
            }
        }
        public static Regions CreateRegions(int id)
        {
            return new Regions
            {
                ID = id
            };
        }
    }
}
