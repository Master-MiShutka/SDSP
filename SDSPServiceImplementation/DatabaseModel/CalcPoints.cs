using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "CalcPoints"), DataContract(IsReference = true)]
    [Serializable]
    public class CalcPoints : EntityObject
    {
        private int _ID;
        private int? _Collector_id;
        private int? _Flat_id;
        private string _Personal_acc;
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
        public int? Collector_id
        {
            get
            {
                return this._Collector_id;
            }
            set
            {
                this.ReportPropertyChanging("Collector_id");
                this._Collector_id = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Collector_id");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public int? Flat_id
        {
            get
            {
                return this._Flat_id;
            }
            set
            {
                this.ReportPropertyChanging("Flat_id");
                this._Flat_id = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Flat_id");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public string Personal_acc
        {
            get
            {
                return this._Personal_acc;
            }
            set
            {
                this.ReportPropertyChanging("Personal_acc");
                this._Personal_acc = StructuralObject.SetValidValue(value, true);
                this.ReportPropertyChanged("Personal_acc");
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_CalcPointsCounters", "Counters"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<Counters> Counters
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Counters>("AskueModel.fgn_key_CalcPointsCounters", "Counters");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Counters>("AskueModel.fgn_key_CalcPointsCounters", "Counters", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_CalcPointsIndications", "Indications"), DataMember, SoapIgnore, XmlIgnore]
        public EntityCollection<Indications> Indications
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Indications>("AskueModel.fgn_key_CalcPointsIndications", "Indications");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Indications>("AskueModel.fgn_key_CalcPointsIndications", "Indications", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_CollectorsCalcPoints", "Collectors"), DataMember, SoapIgnore, XmlIgnore]
        public Collectors Collectors
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Collectors>("AskueModel.fgn_key_CollectorsCalcPoints", "Collectors").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Collectors>("AskueModel.fgn_key_CollectorsCalcPoints", "Collectors").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<Collectors> CollectorsReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Collectors>("AskueModel.fgn_key_CollectorsCalcPoints", "Collectors");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Collectors>("AskueModel.fgn_key_CollectorsCalcPoints", "Collectors", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_FlatsCalcPoints", "Flats"), DataMember, SoapIgnore, XmlIgnore]
        public Flats Flats
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Flats>("AskueModel.fgn_key_FlatsCalcPoints", "Flats").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Flats>("AskueModel.fgn_key_FlatsCalcPoints", "Flats").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<Flats> FlatsReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Flats>("AskueModel.fgn_key_FlatsCalcPoints", "Flats");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Flats>("AskueModel.fgn_key_FlatsCalcPoints", "Flats", value);
                }
            }
        }
        public static CalcPoints CreateCalcPoints(int id)
        {
            return new CalcPoints
            {
                ID = id
            };
        }
    }
}
