using System.Xml.Serialization;

namespace MultiSync.Models.Item
{
    [XmlRoot("Items")]
    public class XMLItemList
    {
        [XmlElement("item")]
        public List<XMLItem> ItemList { get; set; }

        public XMLItemList()
        {
            this.ItemList = new List<XMLItem>();
        }

    }
}
