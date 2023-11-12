﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiSync.Models.Item;
using MultiSync.Repository.MongoDB;
using MultiSync.Repository.MS;
using MultiSync.Repository.XML;

namespace MultiSync.Controllers
{
    public class MongoDBController : Controller
    {
        private readonly IMongoDBRepo<BsonItem> _mongoRepo;
        private IMapper _mapper;
        public MongoDBController(IMongoDBRepo<BsonItem> mongoRepo,  IMapper mapper)
        {
            _mongoRepo = mongoRepo;
            _mapper = mapper;
        }
        // GET: MongoDBController
        public ActionResult Index()
        {
            var list = _mapper.Map<List<ItemViewModel>>(_mongoRepo.GetAllAsync().Result.ToList());
            return View(list);
        }

        // GET: MongoDBController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MongoDBController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MongoDBController/Create
        [HttpPost]
        [Route("/MongoDBController/Create")]
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
                _mongoRepo.CreateAsync(_mapper.Map<BsonItem>(addItem));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok("success");
        }


        // GET: MongoDBController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MongoDBController/Edit/5
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

        // GET: MongoDBController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MongoDBController/Delete/5
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
