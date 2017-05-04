using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lazywg.Facade
{
    [Serializable]
    public class FacadeSet
    {
        [XmlElement]
        public Facade[] Item { get; set; }
    }

    [Serializable]
    public class Facade {

        public Facade() {
            LifeMode = LifeMode.Singleton;
        }

        public string Constructor { get; set; }

        [XmlElement("Prop")]
        public Property[] Prop { get; set; }

        [XmlAttribute("Interface")]
        public string Interface { get; set; }

        [XmlAttribute("Implement")]
        public string Implement { get; set; }

        [XmlAttribute("LifeMode")]
        [DefaultValue(LifeMode.Singleton)]
        public LifeMode LifeMode { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }
    }

    [Serializable]
    public enum LifeMode {
        Singleton,
        PerRequest
    }

    [Serializable]
    public class Property {
       
        [XmlAttribute]
        public string Name { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
