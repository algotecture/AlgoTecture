namespace Algotecture.Libraries.GeoAdminSearch.Models.GeoAdminModels
{
    public class Attrs
    {
        public string origin { get; set; } = null!;
        public string geom_quadindex { get; set; } = null!;
        public int zoomlevel { get; set; }
        //if source=address -> first part is EGID
        public string featureId { get; set; } = null!;
        public double lon { get; set; }
        public string detail { get; set; } = null!;
        public int rank { get; set; }
        public string geom_st_box2d { get; set; } = null!;
        public double lat { get; set; }
        public int num { get; set; }
        public double y { get; set; }
        public double x { get; set; }
        public string label { get; set; } = null!;
    }

    public class Result
    {
        public int id { get; set; }
        public int weight { get; set; }
        public Attrs? attrs { get; set; }
    }

    public class GeoadminApiSearch
    {
        public List<Result>? results { get; set; }
        public string status { get; set; } = null!;

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class IdentifyGeometry
    {
        public string type { get; set; } = null!;
        public List<double>? coordinates { get; set; }
    }

    public class IdentifyProperties
    {
        public bool adr_reliable { get; set; }
        public string str_label { get; set; } = null!;
        public int com_fosnr { get; set; }
        public string adr_modified { get; set; } = null!;
        public int adr_edid { get; set; }
        public int label { get; set; }
        public int bdg_egid { get; set; }
        public string adr_number { get; set; } = null!;
        public string adr_zip { get; set; } = null!;
        public bool adr_official { get; set; }
        public string adr_status { get; set; } = null!;
        public int str_esid { get; set; }
        public string com_name { get; set; } = null!;
    }

    public class IdentifyResult
    {
        public IdentifyGeometry geometry { get; set; } = null!;
        public string layerBodId { get; set; } = null!;
        public List<double> bbox { get; set; } = null!;
        public int featureId { get; set; }
        public string layerName { get; set; } = null!;
        public string type { get; set; } = null!;
        public int id { get; set; }
        public IdentifyProperties? properties { get; set; }
    }

    public class GeoadminApiIdentify
    {
        public List<IdentifyResult> results { get; set; } = null!;
    }  
    }
