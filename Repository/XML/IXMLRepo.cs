﻿namespace MultiSync.Repository.XML
{
    public interface IXMLRepo<XMLItem>
    {
        List<XMLItem> GetAll();
        XMLItem GetById(string id);
        void Add(XMLItem item);
        void Update(XMLItem item);
        void Synced(string itemID);
        void Delete(string id);
    }
}
