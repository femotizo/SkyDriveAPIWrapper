using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

using SkyDriveAPI.Objects.Folder;
using SkyDriveAPI.Utils;
using SkyDriveAPI.Objects;

namespace SkyDriveAPI
{
    public class SkyDrive
    {
        protected const string AuthUri = "https://login.live.com/oauth20_authorize.srf";
        protected const string FolderUri = "https://apis.live.net/v5.0/me/skydrive/files";
        protected const string BaseURI = "https://apis.live.net/v5.0/";
        public string AuthURI = string.Empty;
        protected string _SiteURL = string.Empty;
        protected string _ClientId = string.Empty;
        public string AccessToken { get; set; }

        public SkyDrive(string ClientID, string SiteURL)
        {
            this._ClientId = ClientID;
            this._SiteURL = SiteURL;
        }

        public void Authenticate()
        {
            string authorizeUri = AuthUri;
            authorizeUri = authorizeUri + String.Format("?client_id={0}&", _ClientId);
            authorizeUri = authorizeUri + String.Format("scope={0}&", "wl.signin,wl.skydrive_update");
            authorizeUri = authorizeUri + String.Format("response_type={0}&", "token");
            authorizeUri = authorizeUri + String.Format("redirect_uri={0}", HttpUtility.UrlEncode(_SiteURL));
            this.AuthURI = authorizeUri;
        }

        public User GetUserInfo()
        {
            string URI = BaseURI + "me?access_token=" + AccessToken;
            string user_json = SkyDriveResponse.ProcessRequest(URI);
            User user = JsonConvert.DeserializeObject<User>(user_json);
            return user;

        }

        public Dictionary<string, string> ListFolders()
        {
            Dictionary<string, string> Folders = new Dictionary<string, string>();
            string folderUri = FolderUri;
            folderUri = FolderUri + String.Format("?access_token={0}", AccessToken);

            string json = SkyDriveResponse.ProcessRequest(folderUri);

            var skyDrive_Folder = JsonConvert.DeserializeObject<Folder>(json);

            for (int idx = 0; idx < skyDrive_Folder.Folders.Count; idx++)
            {
                Folders.Add(skyDrive_Folder.Folders[idx].id, skyDrive_Folder.Folders[idx].name);
            }
            return Folders;
        }

        public Folder ListAllFolder_CompleteData()
        {
            Folder skyDrive_Folders = new Folder();
            string folderUri = FolderUri;
            folderUri = FolderUri + String.Format("?access_token={0}", AccessToken);

            string json = SkyDriveResponse.ProcessRequest(folderUri);

            skyDrive_Folders = JsonConvert.DeserializeObject<Folder>(json);

            return skyDrive_Folders;
        }

        public FolderContent ListFolderContent(string FolderID)
        {
            FolderContent content = new Objects.Folder.FolderContent();
            string folderContentUri = BaseURI + FolderID + "/files";
            folderContentUri = folderContentUri + String.Format("?access_token={0}", AccessToken);

            string json = SkyDriveResponse.ProcessRequest(folderContentUri);
            content = JsonConvert.DeserializeObject<FolderContent>(json);

            return content;
        }

        public string GetFileHotLink(string FolderID, string FileName)
        {
            string HotLink = string.Empty;

            FolderContent content = ListFolderContent(FolderID);
            if (content != null)
            {
                for (int idx = 0; idx < content.Files.Count; idx++)
                {
                    if (content.Files[idx].name.ToLower() == FileName.ToLower())
                    {
                        HotLink = content.Files[idx].source;
                    }
                }

                return HotLink;
            }
            else
            {
                throw new FileNotFoundException("No such file/folder found");
            }
        }

        public bool UploadFile(string FolderID, string FilePath)
        {
            bool Uploaded = false;

            try
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(FilePath);

                string UploadURI = BaseURI + FolderID + "/files/" + Path.GetFileName(FilePath) + "?access_token=" + AccessToken;

                var request = (HttpWebRequest)HttpWebRequest.Create(UploadURI);
                request.Method = "PUT";
                request.ContentLength = fileBytes.Length;

                using (var dataStream = request.GetRequestStream())
                {
                    dataStream.Write(fileBytes, 0, fileBytes.Length);
                }

                string status = (((HttpWebResponse)request.GetResponse()).StatusDescription);

                if (status.ToLower() == "created")
                {
                    Uploaded = true;
                }

                return Uploaded;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}