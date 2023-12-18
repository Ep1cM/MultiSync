using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MultiSync.Models.Item;
using MultiSync.Repository.MongoDB;
using MultiSync.Repository.MS;
using MultiSync.Repository.XML;
using MultiSync.Services;

namespace MultiSync.Controllers
{
    public class SyncController : Controller
    {
        private readonly EventHandlerService _eventSubscriber;
        private readonly IMSRepo<MSItem> _msRepo;
        private readonly IMongoDBRepo<BsonItem> _mongoRepo;
        private readonly IXMLRepo<XMLItem> _xmlRepo;
        private IMapper _mapper;
        public SyncController( IMapper mapper)
        {
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        /*
        [HttpGet]
        [Route("/SyncController/Check")]
        public bool CheckDifferences()
        {
            var ms =_msRepo.GetAll().Where(x => x.sync = false);
            var mongo = _mongoRepo.GetAllAsync().Result.Where(x => x.sync = false);
            var xml = _xmlRepo.GetAll().Where(x => x.sync = false);
            if (ms.Any() || mongo.Any() || xml.Any()) 
            {
                return true;
            }
            return CheckDeletes();
        }
        
        public List listOfNewItems()
        {
            var ms = _msRepo.GetAll().Where(x => x.sync = false).Select(ms => ms.surrogateId);
            var mongo = _mongoRepo.GetAllAsync().Result.Where(x => x.sync = false).Select(mg => mg.surrogateId);
            var xml = _xmlRepo.GetAll().Where(x => x.sync = false).Select(x => x.surrogateId);
        }*/

        /*
        public bool CheckDeletes()
        {
            var ms = _msRepo.GetAll().Where(x => x.sync = true).Select(ms => ms.surrogateId);
            var mongo = _mongoRepo.GetAllAsync().Result.Where(x => x.sync = true).Select(mg => mg.surrogateId);
            var xml = _xmlRepo.GetAll().Where(x => x.sync = true).Select(x => x.surrogateId);

            var ms12 = ms.Except(mongo);
            var ms13 = ms.Except(xml);

            var mongo21 = mongo.Except(ms);
            var mongo23 = mongo.Except(xml);

            var xml31 = xml.Except(ms);
            var xml32 = xml.Except(mongo);

            if (ms12.Any() || ms13.Any() || mongo21.Any() || mongo23.Any() || xml31.Any() || xml32.Any())
            {
                return true;
            }
            return false;
        }*/
    }
}
