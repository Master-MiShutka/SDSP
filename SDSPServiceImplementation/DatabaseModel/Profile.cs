using System;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "Profile"), DataContract(IsReference = true)]
    [Serializable]
    public partial class Profile : EntityObject
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
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_ProfileIndications", "Indications"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<Indications> Indications
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Indications>("AskueModel.fgn_key_ProfileIndications", "Indications");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Indications>("AskueModel.fgn_key_ProfileIndications", "Indications", value);
                }
            }
        }
        public static Profile CreateProfile(int id)
        {
            return new Profile
            {
                ID = id
            };
        }
    }
}
