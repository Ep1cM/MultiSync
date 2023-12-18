using AutoMapper;
using MultiSync.Models.Item;
using MultiSync.Repository.MongoDB;
using MultiSync.Repository.MS;
using MultiSync.Repository.XML;
using NuGet.Configuration;
using NuGet.Protocol.Core.Types;

namespace MultiSync.Services
{
    public class EventHandlerService
    {
        private readonly EventManager _eventManager;
        private readonly IMSRepo<MSItem> _msRepo;
        private readonly IMongoDBRepo<BsonItem> _mongoRepo;
        private readonly IXMLRepo<XMLItem> _xmlRepo;
        private IMapper _mapper;
        public EventHandlerService(IMSRepo<MSItem> mSRepo, IMongoDBRepo<BsonItem> mongoRepo, IXMLRepo<XMLItem> xmlRepo, IMapper mapper,EventManager eventManager)
        {
            _eventManager = eventManager;
            _msRepo = mSRepo;
            _mongoRepo = mongoRepo;
            _xmlRepo = xmlRepo;
            _mapper = mapper;
            _eventManager.Subscribe<MSItemEventArgs>(this, HandleMSItemEvent);
            _eventManager.Subscribe<BsonItemEventArgs>(this, HandleBsonItemEvent);
            _eventManager.Subscribe<XMLItemEventArgs>(this, HandleXMLItemEvent);

        }
        private void HandleMSItemEvent(MSItemEventArgs e)
        {
            Console.WriteLine("MSHandleItemAdded");
            var actionCall = e.Action;
            if (actionCall == "Create")
            {
                Console.WriteLine("---------CreateMS");
                var item = _mapper.Map<ItemViewModel>(e.Item);
                MSCreated(item);

            }
            else if (actionCall == "Update")
            {
                Console.WriteLine("---------UpdateMS");
                var item = _mapper.Map<ItemViewModel>(e.Item);
                MSUpdated(item);

            }
            else if (actionCall == "Delete")
            {
                Console.WriteLine("---------DeleteMS");
                MSDeleted(e.Item.surrogateId);
            }
            else
            {
                Console.WriteLine("SOMETHING HAPPENED");
            }
        }

        private void MSCreated(ItemViewModel item)
        {
            try
            {
                _mongoRepo.CreateAsync(_mapper.Map<BsonItem>(item));
                _mongoRepo.SyncedAsync(item.surrogateId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                _xmlRepo.Add(_mapper.Map<XMLItem>(item));
                _xmlRepo.Synced(item.surrogateId);
                _msRepo.Synced(item.surrogateId);
            
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


        private void MSUpdated(ItemViewModel item)
        {
            try
            {
                _mongoRepo.UpdateAsync(item.surrogateId,_mapper.Map<BsonItem>(item));
                _mongoRepo.SyncedAsync(item.surrogateId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                _xmlRepo.Update(_mapper.Map<XMLItem>(item));
                _xmlRepo.Synced(item.surrogateId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void MSDeleted(String itemID)
        {
            try
            {
                _mongoRepo.DeleteAsync(itemID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                _xmlRepo.Delete(itemID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void HandleBsonItemEvent(BsonItemEventArgs e)
        {
            Console.WriteLine("HandleBsonItemEvent");
            var actionCall = e.Action;
            if (actionCall == "Create")
            {
                Console.WriteLine("---------CreateBson");
                var item = _mapper.Map<ItemViewModel>(e.Item);
                BsonCreated(item);

            }
            else if (actionCall == "Update")
            {
                Console.WriteLine("---------UpdateBson");
                var item = _mapper.Map<ItemViewModel>(e.Item);
                BsonUpdated(item);

            }
            else if (actionCall == "Delete")
            {
                Console.WriteLine("---------DeleteBson");
                BsonDeleted(e.Item.surrogateId);
            }
            else
            {
                Console.WriteLine("SOMETHING HAPPENED");
            }
        }

        private void BsonCreated(ItemViewModel item)
        {
            try
            {
                _msRepo.Add(_mapper.Map<MSItem>(item));
                _msRepo.Synced(item.surrogateId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                _xmlRepo.Add(_mapper.Map<XMLItem>(item));
                _xmlRepo.Synced(item.surrogateId);
                _mongoRepo.SyncedAsync(item.surrogateId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


        private void BsonUpdated(ItemViewModel item)
        {
            try
            {
                _msRepo.Update(_mapper.Map<MSItem>(item));
                _msRepo.Synced(item.surrogateId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                _xmlRepo.Update(_mapper.Map<XMLItem>(item));
                _xmlRepo.Synced(item.surrogateId);
                _mongoRepo.SyncedAsync(item.surrogateId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void BsonDeleted(String itemID)
        {
            try
            {
                _msRepo.Delete(itemID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                _xmlRepo.Delete(itemID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
        private void HandleXMLItemEvent(XMLItemEventArgs e)
        {
            Console.WriteLine("HandleXMLItemEvent");
            var actionCall = e.Action;
            if (actionCall == "Create")
            {
                Console.WriteLine("---------CreateXML");
                var item = _mapper.Map<ItemViewModel>(e.Item);
                XMLCreated(item);

            }
            else if (actionCall == "Update")
            {
                Console.WriteLine("---------UpdateXML");
                var item = _mapper.Map<ItemViewModel>(e.Item);
                XMLUpdated(item);

            }
            else if (actionCall == "Delete")
            {
                Console.WriteLine("---------DeleteXML");
                XMLDeleted(e.Item.surrogateId);
            }
            else
            {
                Console.WriteLine("SOMETHING HAPPENED");
            }
        }

        private void XMLCreated(ItemViewModel item)
        {
            try
            {
                _msRepo.Add(_mapper.Map<MSItem>(item));
                _msRepo.Synced(item.surrogateId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                _mongoRepo.CreateAsync(_mapper.Map<BsonItem>(item));
                _mongoRepo.SyncedAsync(item.surrogateId);
                _xmlRepo.Synced(item.surrogateId);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


        private void XMLUpdated(ItemViewModel item)
        {
            try
            {
                _msRepo.Update(_mapper.Map<MSItem>(item));
                _msRepo.Synced(item.surrogateId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                _mongoRepo.UpdateAsync(item.surrogateId, _mapper.Map<BsonItem>(item));
                _mongoRepo.SyncedAsync(item.surrogateId);
                _xmlRepo.Synced(item.surrogateId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        private void XMLDeleted(String itemID)
        {
            try
            {
                _msRepo.Delete(itemID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            try
            {
                _mongoRepo.DeleteAsync(itemID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

    }
}
