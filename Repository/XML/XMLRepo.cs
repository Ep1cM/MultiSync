using MultiSync.Models.Item;
using MultiSync.Services;
using SharpCompress.Common;
using System.Xml.Linq;

namespace MultiSync.Repository.XML
{
    public class XMLRepo : IXMLRepo<XMLItem>
    {
        private readonly string _xmlFilePath;
        private readonly XDocument _xmlDocument;

        public XMLRepo()
        {
            _xmlFilePath = "Items.xml";
            _xmlDocument = LoadXmlData();
        }

        public List<XMLItem> GetAll()
        {
            return _xmlDocument.Root.Elements()
                .Select(x => Deserialize(x))
                .ToList();
        }

        public XMLItem GetById(string id)
        {
            XElement element = _xmlDocument.Root.Elements()
                .FirstOrDefault(x => x.Element("surrogateId").Value == id);
            return element != null ? Deserialize(element) : default(XMLItem);
        }

        public void Add(XMLItem item)
        {
            XElement element = Serialize(item);
            _xmlDocument.Root.Add(element);
            SaveChanges();
        }

        public void Update(XMLItem item)
        {
            string id = item.surrogateId;
            XElement existingElement = _xmlDocument.Root.Elements()
                .FirstOrDefault(x => x.Attribute("surrogateId").Value == id);

            if (existingElement != null)
            {
                existingElement.ReplaceWith(Serialize(item));
                SaveChanges();
            }
        }

        public void Delete(string id)
        {
            XElement elementToDelete = _xmlDocument.Root.Elements()
                .FirstOrDefault(x => x.Attribute("surrogateId").Value == id);

            if (elementToDelete != null)
            {
                elementToDelete.Remove();
                SaveChanges();
            }
        }

        private XDocument LoadXmlData()
        {
            if (File.Exists(_xmlFilePath))
            {
                return XDocument.Load(_xmlFilePath);
            }
            else
            {
                return new XDocument(new XElement("Items"));
            }
        }

        public void Synced(String itemID)
        {
            XElement existingElement = _xmlDocument.Root.Elements()
                .FirstOrDefault(x => x.Attribute("surrogateId").Value == itemID);
            var updItem = Deserialize(existingElement);
            updItem.sync = true;

            if (existingElement != null)
            {
                existingElement.ReplaceWith(Serialize(updItem));
                SaveChanges();
            }
        }
        private void SaveChanges()
        {
            _xmlDocument.Save(_xmlFilePath);
        }

        private XElement Serialize(XMLItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            XElement itemElement = new XElement("Item",
                new XAttribute("surrogateId", item.surrogateId),
                new XElement("name", item.name),
                new XElement("description", item.description),
                new XElement("quantity", item.quantity),
                new XElement("price", item.price),
                new XElement("sync", item.sync),
                new XElement("createTime", item.createTime.ToString("yyyy-MM-dd HH:mm:ss")),
                new XElement("changeTime", item.changeTime.ToString("yyyy-MM-dd HH:mm:ss"))
            );

            return itemElement;
        }

        private XMLItem Deserialize(XElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            XMLItem item = new XMLItem
            {
                surrogateId = element.Attribute("surrogateId").Value,
                name = element.Element("name")?.Value,
                description = element.Element("description")?.Value,
                quantity = int.Parse(element.Element("quantity")?.Value),
                price = double.Parse(element.Element("price")?.Value),
                sync = bool.Parse(element.Element("sync")?.Value),
                createTime = DateTime.Parse(element.Element("createTime")?.Value),
                changeTime = DateTime.Parse(element.Element("changeTime")?.Value)
            };

            return item;
        }
    }
}
