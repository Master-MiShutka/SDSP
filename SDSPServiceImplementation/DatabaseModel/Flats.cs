using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "Flats"), DataContract(IsReference = true)]
    [Serializable]
    public class Flats : EntityObject
    {
        private int _ID;
        private int? _House_ID;
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
        public int? House_ID
        {
            get
            {
                return this._House_ID;
            }
            set
            {
                this.ReportPropertyChanging("House_ID");
                this._House_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("House_ID");
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
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_FlatsCalcPoints", "CalcPoints"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<CalcPoints> CalcPoints
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<CalcPoints>("AskueModel.fgn_key_FlatsCalcPoints", "CalcPoints");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<CalcPoints>("AskueModel.fgn_key_FlatsCalcPoints", "CalcPoints", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_HousesFlats", "Houses"), DataMember, SoapIgnore, XmlIgnore]
        public Houses Houses
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Houses>("AskueModel.fgn_key_HousesFlats", "Houses").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Houses>("AskueModel.fgn_key_HousesFlats", "Houses").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<Houses> HousesReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Houses>("AskueModel.fgn_key_HousesFlats", "Houses");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Houses>("AskueModel.fgn_key_HousesFlats", "Houses", value);
                }
            }
        }
        public static Flats CreateFlats(int id)
        {
            return new Flats
            {
                ID = id
            };
        }
    }
}
