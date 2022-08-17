using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Api.Resources
{
    public class ManageTranslateResource
    {
        private ResourceManager _en;
        private ResourceManager _de;
        private ResourceManager _fr;
        private ResourceManager _sw;
        private ManageTranslateResource()
        {
            _en = new ResourceManager("CustomerArea.Api.Resources.en",
                                         typeof(ManageTranslateResource).Assembly);

            _de = new ResourceManager("CustomerArea.Api.Resources.de",
                                        typeof(ManageTranslateResource).Assembly);
            _fr = new ResourceManager("CustomerArea.Api.Resources.fr",
                                        typeof(ManageTranslateResource).Assembly);
            _sw = new ResourceManager("CustomerArea.Api.Resources.sw",
                                        typeof(ManageTranslateResource).Assembly);

        }

        private static ManageTranslateResource _instance;
        public static ManageTranslateResource GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ManageTranslateResource();
            }
            return _instance;
        }

        public string En(string key, params object[] args)
        {
            string value = _instance._en.GetString(key);
            value = value == null ? key : value;
            if (args.Length == 0)
                return value;
            return string.Format(value, args);
        }
        public string De(string key, params object[] args)
        {
            string value = _instance._de.GetString(key);
            value = value == null ? key : value;
            if (args.Length == 0)
                return value;
            return string.Format(value, args);
        }

        public string Fr(string key, params object[] args)
        {
            string value = _instance._fr.GetString(key);
            value = value == null ? key : value;
            if (args.Length == 0)
                return value;
            return string.Format(value, args);
        }

        public string Sw(string key, params object[] args)
        {
            string value = _instance._sw.GetString(key);
            value = value == null ? key : value;
            if (args.Length == 0)
                return value;
            return string.Format(value, args);
        }
    }
}
