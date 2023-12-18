using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiSync.Models.Item;
using MultiSync.Repository.MongoDB;
using MultiSync.Repository.MS;
using MultiSync.Repository.XML;
using MultiSync.Services;

namespace MultiSync.Controllers
{
    public class XMLController : Controller
    {
        private readonly EventManager _eventManager;
        private readonly EventHandlerService _eventSubscriber;
        private readonly IXMLRepo<XMLItem> _xmlRepo;
        private IMapper _mapper;
        public XMLController(EventManager eventManager,EventHandlerService eventSubscriber, IXMLRepo<XMLItem> xmlRepo, IMapper mapper)
        {
            _eventManager = eventManager;
            _eventSubscriber = eventSubscriber;
            _xmlRepo = xmlRepo;
            _mapper = mapper;
        }
        // GET: XMLController
        public ActionResult Index()
        {
            var list = _mapper.Map<List<ItemViewModel>>(_xmlRepo.GetAll().ToList());
            return View(list);
        }

        // GET: XMLController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: XMLController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: XMLController/Create
        [HttpPost]
        [Route("/XMLController/Create")]
        public ActionResult Create([FromBody] NewItem data)
        {
            Console.WriteLine("Creating");
            if (data == null)
            {
                return BadRequest("Invalid JSON data");
            }

            var addItem = new ItemViewModel(data.Name, data.Description, data.Quantity, data.Price);
            try
            {
                var item = _mapper.Map<XMLItem>(addItem);
                _xmlRepo.Add(item);
                _eventManager.Publish(new XMLItemEventArgs(item,"Create"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok("success");
        }


        // GET: XMLController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: XMLController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: XMLController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: XMLController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
