using System;
using System.ComponentModel;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.Xml.Serialization;
namespace SDSPServiceImplementation.DatabaseModel
{
    [EdmEntityType(NamespaceName = "AskueModel", Name = "Indications"), DataContract(IsReference = true)]
    [Serializable]
    public class Indications : EntityObject
    {
        private int _ID;
        private int? _CalcPoint_ID;
        private int? _Profile_ID;
        private DateTime? _Ts;
        private float? _Tr0;
        private float? _Tr1;
        private float? _Tr2;
        private float? _Tr3;
        private float? _Tr4;
        private int? _Da;
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
        public int? CalcPoint_ID
        {
            get
            {
                return this._CalcPoint_ID;
            }
            set
            {
                this.ReportPropertyChanging("CalcPoint_ID");
                this._CalcPoint_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("CalcPoint_ID");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public int? Profile_ID
        {
            get
            {
                return this._Profile_ID;
            }
            set
            {
                this.ReportPropertyChanging("Profile_ID");
                this._Profile_ID = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Profile_ID");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public DateTime? Ts
        {
            get
            {
                return this._Ts;
            }
            set
            {
                this.ReportPropertyChanging("Ts");
                this._Ts = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Ts");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public float? Tr0
        {
            get
            {
                return this._Tr0;
            }
            set
            {
                this.ReportPropertyChanging("Tr0");
                this._Tr0 = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Tr0");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public float? Tr1
        {
            get
            {
                return this._Tr1;
            }
            set
            {
                this.ReportPropertyChanging("Tr1");
                this._Tr1 = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Tr1");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public float? Tr2
        {
            get
            {
                return this._Tr2;
            }
            set
            {
                this.ReportPropertyChanging("Tr2");
                this._Tr2 = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Tr2");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public float? Tr3
        {
            get
            {
                return this._Tr3;
            }
            set
            {
                this.ReportPropertyChanging("Tr3");
                this._Tr3 = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Tr3");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public float? Tr4
        {
            get
            {
                return this._Tr4;
            }
            set
            {
                this.ReportPropertyChanging("Tr4");
                this._Tr4 = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Tr4");
            }
        }
        [EdmScalarProperty(EntityKeyProperty = false, IsNullable = true), DataMember]
        public int? Da
        {
            get
            {
                return this._Da;
            }
            set
            {
                this.ReportPropertyChanging("Da");
                this._Da = StructuralObject.SetValidValue(value);
                this.ReportPropertyChanged("Da");
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_CalcPointsIndications", "CalcPoints"), DataMember, SoapIgnore, XmlIgnore]
        public CalcPoints CalcPoints
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<CalcPoints>("AskueModel.fgn_key_CalcPointsIndications", "CalcPoints").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<CalcPoints>("AskueModel.fgn_key_CalcPointsIndications", "CalcPoints").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<CalcPoints> CalcPointsReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<CalcPoints>("AskueModel.fgn_key_CalcPointsIndications", "CalcPoints");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<CalcPoints>("AskueModel.fgn_key_CalcPointsIndications", "CalcPoints", value);
                }
            }
        }
        [EdmRelationshipNavigationProperty("AskueModel", "fgn_key_ProfileIndications", "Profile"), DataMember, SoapIgnore, XmlIgnore]
        public Profile Profile
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Profile>("AskueModel.fgn_key_ProfileIndications", "Profile").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Profile>("AskueModel.fgn_key_ProfileIndications", "Profile").Value = value;
            }
        }
        [Browsable(false), DataMember]
        public EntityReference<Profile> ProfileReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Profile>("AskueModel.fgn_key_ProfileIndications", "Profile");
            }
            set
            {
                if (value != null)
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Profile>("AskueModel.fgn_key_ProfileIndications", "Profile", value);
                }
            }
        }
        public static Indications CreateIndications(int id)
        {
            return new Indications
            {
                ID = id
            };
        }
    }
}
