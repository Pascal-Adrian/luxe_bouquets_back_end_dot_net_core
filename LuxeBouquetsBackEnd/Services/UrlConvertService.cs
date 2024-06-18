namespace LuxeBouquetsBackEnd.Services
{
    public class UrlConvertService
    {
        private readonly string bad_prefix = "https://drive.google.com/file/d/";
        private readonly string good_prefix = "https://drive.google.com/thumbnail?id=";
        private readonly string bad_suffix = "/view?usp=sharing";
        private readonly string good_suffix = "&sz=w512";


        public string ConvertGoogleDriveUrl(string url)
        {

            if (url.Contains(bad_suffix))
            {
                url = url.Replace(bad_suffix, good_suffix);
            }

            if (url.Contains(bad_prefix))
            {
                url = url.Replace(bad_prefix, good_prefix);
            }

            return url;
        }

        public string ChangeImageSize(string url, int size)
        {
            if (url.Contains(good_suffix))
            {
                url = url.Replace(good_suffix, "&sz=w" + size);
            }
            return url;
        }

        public bool IsGoogleDriveUrl(string url)
        {
            if (url.Contains(bad_prefix) && url.Contains(bad_suffix))
            {
                return true;
            }
            return false;
        }
    }
}