using System.Xml.Serialization;

namespace DisasterTracker.PdcApiModels
{
    [Serializable]
    [XmlType(AnonymousType = true)]
    public class HazardBean
    {
        [XmlElement("app_ID")]
        public byte AppId { get; set; }

        [XmlElement("app_IDs")]
        public object AppIds { get; set; }

        [XmlElement("autoexpire")]
        public string AutoExpire { get; set; }

        [XmlElement("category_ID")]
        public string CategoryId { get; set; }

        [XmlElement("charter_Uri")]
        public object CharterUri { get; set; }

        [XmlElement("comment_Text")]
        public string CommentText { get; set; }

        [XmlElement("create_Date_hst")]
        public DateTimeOffset CreateDateHst { get; set; }

        [XmlElement("create_Date")]
        public DateTimeOffset CreateDate { get; set; }

        [XmlElement("creator")]
        public string Creator { get; set; }

        [XmlElement("end_Date_hst")]
        public DateTimeOffset EndDateHst { get; set; }

        [XmlElement("end_Date")]
        public DateTimeOffset EndDate { get; set; }

        [XmlElement("glide_Uri")]
        public object GlideUri { get; set; }

        [XmlElement("hazard_ID")]
        public uint HazardId { get; set; }

        [XmlElement("hazard_Name")]
        public string HazardName { get; set; }

        [XmlElement("last_Update_hst")]
        public DateTimeOffset LastUpdateHst { get; set; }

        [XmlElement("last_Update")]
        public DateTimeOffset LastUpdate { get; set; }

        [XmlElement("latitude")]
        public decimal Latitude { get; set; }

        [XmlElement("longitude")]
        public decimal Longitude { get; set; }

        [XmlElement("master_Incident_ID")]
        public string MasterIncidentId { get; set; }

        [XmlElement("message_ID")]
        public string MessageId { get; set; }

        [XmlElement("org_ID")]
        public sbyte OrgId { get; set; }

        [XmlElement("severity_ID")]
        public string SeverityId { get; set; }

        [XmlElement("snc_url")]
        public string SncUrl { get; set; }

        [XmlElement("start_Date_hst")]
        public DateTimeOffset StartDateHst { get; set; }

        [XmlElement("start_Date")]
        public DateTimeOffset StartDate { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("type_ID")]
        public string TypeId { get; set; }

        [XmlElement("update_Date_hst")]
        public DateTimeOffset UpdateDateHst { get; set; }

        [XmlElement("update_Date")]
        public DateTimeOffset UpdateDate { get; set; }

        [XmlElement("update_User")]
        public string UpdateUser { get; set; }

        [XmlElement("product_total")]
        public ushort ProductTotal { get; set; }

        [XmlElement("uuid")]
        public string ApiId { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }
    }
}