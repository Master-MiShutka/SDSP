using System;
using System.Data.EntityClient;
using System.Data.Objects;
namespace SDSPServiceImplementation.DatabaseModel
{
    public class AskueEntities : ObjectContext
    {
        private ObjectSet<CalcPoints> _CalcPoints;
        private ObjectSet<Channels> _Channels;
        private ObjectSet<Collectors> _Collectors;
        private ObjectSet<Collectors_Statuses> _Collectors_Statuses;
        private ObjectSet<Counters> _Counters;
        private ObjectSet<Counters_type> _Counters_type;
        private ObjectSet<FES> _FES;
        private ObjectSet<Flats> _Flats;
        private ObjectSet<Houses> _Houses;
        private ObjectSet<Indications> _Indications;
        private ObjectSet<Places> _Places;
        private ObjectSet<Profile> _Profile;
        private ObjectSet<Regions> _Regions;
        private ObjectSet<RES> _RES;
        private ObjectSet<Streets> _Streets;
        private ObjectSet<Counters_Statuses> _Counters_Statuses;
        public ObjectSet<CalcPoints> CalcPoints
        {
            get
            {
                if (this._CalcPoints == null)
                {
                    this._CalcPoints = base.CreateObjectSet<CalcPoints>("CalcPoints");
                }
                return this._CalcPoints;
            }
        }
        public ObjectSet<Channels> Channels
        {
            get
            {
                if (this._Channels == null)
                {
                    this._Channels = base.CreateObjectSet<Channels>("Channels");
                }
                return this._Channels;
            }
        }
        public ObjectSet<Collectors> Collectors
        {
            get
            {
                if (this._Collectors == null)
                {
                    this._Collectors = base.CreateObjectSet<Collectors>("Collectors");
                }
                return this._Collectors;
            }
        }
        public ObjectSet<Collectors_Statuses> Collectors_Statuses
        {
            get
            {
                if (this._Collectors_Statuses == null)
                {
                    this._Collectors_Statuses = base.CreateObjectSet<Collectors_Statuses>("Collectors_Statuses");
                }
                return this._Collectors_Statuses;
            }
        }
        public ObjectSet<Counters> Counters
        {
            get
            {
                if (this._Counters == null)
                {
                    this._Counters = base.CreateObjectSet<Counters>("Counters");
                }
                return this._Counters;
            }
        }
        public ObjectSet<Counters_type> Counters_type
        {
            get
            {
                if (this._Counters_type == null)
                {
                    this._Counters_type = base.CreateObjectSet<Counters_type>("Counters_type");
                }
                return this._Counters_type;
            }
        }
        public ObjectSet<FES> FES
        {
            get
            {
                if (this._FES == null)
                {
                    this._FES = base.CreateObjectSet<FES>("FES");
                }
                return this._FES;
            }
        }
        public ObjectSet<Flats> Flats
        {
            get
            {
                if (this._Flats == null)
                {
                    this._Flats = base.CreateObjectSet<Flats>("Flats");
                }
                return this._Flats;
            }
        }
        public ObjectSet<Houses> Houses
        {
            get
            {
                if (this._Houses == null)
                {
                    this._Houses = base.CreateObjectSet<Houses>("Houses");
                }
                return this._Houses;
            }
        }
        public ObjectSet<Indications> Indications
        {
            get
            {
                if (this._Indications == null)
                {
                    this._Indications = base.CreateObjectSet<Indications>("Indications");
                }
                return this._Indications;
            }
        }
        public ObjectSet<Places> Places
        {
            get
            {
                if (this._Places == null)
                {
                    this._Places = base.CreateObjectSet<Places>("Places");
                }
                return this._Places;
            }
        }
        public ObjectSet<Profile> Profile
        {
            get
            {
                if (this._Profile == null)
                {
                    this._Profile = base.CreateObjectSet<Profile>("Profile");
                }
                return this._Profile;
            }
        }
        public ObjectSet<Regions> Regions
        {
            get
            {
                if (this._Regions == null)
                {
                    this._Regions = base.CreateObjectSet<Regions>("Regions");
                }
                return this._Regions;
            }
        }
        public ObjectSet<RES> RES
        {
            get
            {
                if (this._RES == null)
                {
                    this._RES = base.CreateObjectSet<RES>("RES");
                }
                return this._RES;
            }
        }
        public ObjectSet<Streets> Streets
        {
            get
            {
                if (this._Streets == null)
                {
                    this._Streets = base.CreateObjectSet<Streets>("Streets");
                }
                return this._Streets;
            }
        }
        public ObjectSet<Counters_Statuses> Counters_Statuses
        {
            get
            {
                if (this._Counters_Statuses == null)
                {
                    this._Counters_Statuses = base.CreateObjectSet<Counters_Statuses>("Counters_Statuses");
                }
                return this._Counters_Statuses;
            }
        }
        public AskueEntities()
            : base("name=AskueEntities", "AskueEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }
        public AskueEntities(string connectionString)
            : base(connectionString, "AskueEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }
        public AskueEntities(EntityConnection connection)
            : base(connection, "AskueEntities")
        {
            base.ContextOptions.LazyLoadingEnabled = true;
        }
        public void AddToCalcPoints(CalcPoints calcPoints)
        {
            base.AddObject("CalcPoints", calcPoints);
        }
        public void AddToChannels(Channels channels)
        {
            base.AddObject("Channels", channels);
        }
        public void AddToCollectors(Collectors collectors)
        {
            base.AddObject("Collectors", collectors);
        }
        public void AddToCollectors_Statuses(Collectors_Statuses collectors_Statuses)
        {
            base.AddObject("Collectors_Statuses", collectors_Statuses);
        }
        public void AddToCounters(Counters counters)
        {
            base.AddObject("Counters", counters);
        }
        public void AddToCounters_type(Counters_type counters_type)
        {
            base.AddObject("Counters_type", counters_type);
        }
        public void AddToFES(FES fES)
        {
            base.AddObject("FES", fES);
        }
        public void AddToFlats(Flats flats)
        {
            base.AddObject("Flats", flats);
        }
        public void AddToHouses(Houses houses)
        {
            base.AddObject("Houses", houses);
        }
        public void AddToIndications(Indications indications)
        {
            base.AddObject("Indications", indications);
        }
        public void AddToPlaces(Places places)
        {
            base.AddObject("Places", places);
        }
        public void AddToProfile(Profile profile)
        {
            base.AddObject("Profile", profile);
        }
        public void AddToRegions(Regions regions)
        {
            base.AddObject("Regions", regions);
        }
        public void AddToRES(RES rES)
        {
            base.AddObject("RES", rES);
        }
        public void AddToStreets(Streets streets)
        {
            base.AddObject("Streets", streets);
        }
        public void AddToCounters_Statuses(Counters_Statuses counters_Statuses)
        {
            base.AddObject("Counters_Statuses", counters_Statuses);
        }
    }
}
