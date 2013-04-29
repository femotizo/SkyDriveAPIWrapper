using Newtonsoft.Json;
using System.Collections.Generic;

namespace SkyDriveAPI.Objects.Folder
{

    #region Folder Class

    public class From_Folder
    {
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
    }

    public class SharedWith_Folder
    {
        [JsonProperty(PropertyName = "access")]
        public string access { get; set; }
    }

    public class FolderData
    {
        [JsonProperty(PropertyName = "id")]
        public string id { get; set; }
        [JsonProperty(PropertyName = "from")]
        public From_Folder from { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string name { get; set; }
        [JsonProperty(PropertyName = "description")]
        public string description { get; set; }
        [JsonProperty(PropertyName = "parent_id")]
        public string parent_id { get; set; }
        [JsonProperty(PropertyName = "size")]
        public int size { get; set; }
        [JsonProperty(PropertyName = "upload_location")]
        public string upload_location { get; set; }
        [JsonProperty(PropertyName = "comments_count")]
        public int comments_count { get; set; }
        [JsonProperty(PropertyName = "comments_enabled")]
        public bool comments_enabled { get; set; }
        [JsonProperty(PropertyName = "is_embeddable")]
        public bool is_embeddable { get; set; }
        [JsonProperty(PropertyName = "count")]
        public int count { get; set; }
        [JsonProperty(PropertyName = "link")]
        public string link { get; set; }
        [JsonProperty(PropertyName = "type")]
        public string type { get; set; }
        [JsonProperty(PropertyName = "shared_with")]
        public SharedWith_Folder shared_with { get; set; }
        [JsonProperty(PropertyName = "created_time")]
        public string created_time { get; set; }
        [JsonProperty(PropertyName = "updated_time")]
        public string updated_time { get; set; }
    }

    public class Folder
    {
        [JsonProperty(PropertyName = "data")]
        public List<FolderData> Folders { get; set; }
    }

#endregion

    #region Folder Content Class

    public class From_FolderContent
    {
        public string name { get; set; }
        public string id { get; set; }
    }

    public class SharedWith_FolderContent
    {
        public string access { get; set; }
    }

    public class FolderContentData
    {
        public string id { get; set; }
        public From_FolderContent from { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string parent_id { get; set; }
        public int size { get; set; }
        public string upload_location { get; set; }
        public int comments_count { get; set; }
        public bool comments_enabled { get; set; }
        public bool is_embeddable { get; set; }
        public int count { get; set; }
        public string link { get; set; }
        public string type { get; set; }
        public SharedWith_FolderContent shared_with { get; set; }
        public string created_time { get; set; }
        public string updated_time { get; set; }
        public string source { get; set; }
    }

    public class FolderContent
    {
        [JsonProperty(PropertyName = "data")]
        public List<FolderContentData> Files { get; set; }
    }

#endregion

}