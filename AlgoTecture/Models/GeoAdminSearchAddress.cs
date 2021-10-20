using System.Collections.Generic;

namespace AlgoTecture.Models
{
    public class Attrs
    {
        public string origin { get; set; }
        public string geom_quadindex { get; set; }
        public int zoomlevel { get; set; }
        //if source=address -> first part is EGID
        public string featureId { get; set; }
        public double lon { get; set; }
        public string detail { get; set; }
        public int rank { get; set; }
        public string geom_st_box2d { get; set; }
        public double lat { get; set; }
        public int num { get; set; }
        public double y { get; set; }
        public double x { get; set; }
        public string label { get; set; }
    }

    public class Result
    {
        public int id { get; set; }
        public int weight { get; set; }
        public Attrs attrs { get; set; }
    }

    public class GeoadminApiSearch
    {
        public List<Result> results { get; set; }
        public string status { get; set; }

    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class IdentifyGeometry
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class IdentifyProperties
    {
        public bool adr_reliable { get; set; }
        public string str_label { get; set; }
        public int com_fosnr { get; set; }
        public string adr_modified { get; set; }
        public int adr_edid { get; set; }
        public int label { get; set; }
        public int bdg_egid { get; set; }
        public string adr_number { get; set; }
        public string adr_zip { get; set; }
        public bool adr_official { get; set; }
        public string adr_status { get; set; }
        public int str_esid { get; set; }
        public string com_name { get; set; }
    }

    public class IdentifyResult
    {
        public IdentifyGeometry geometry { get; set; }
        public string layerBodId { get; set; }
        public List<double> bbox { get; set; }
        public int featureId { get; set; }
        public string layerName { get; set; }
        public string type { get; set; }
        public int id { get; set; }
        public IdentifyProperties properties { get; set; }
    }

    public class GeoadminApiIdentify
    {
        public List<IdentifyResult> results { get; set; }
    }  
    }
