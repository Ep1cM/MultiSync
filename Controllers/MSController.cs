﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver.Core.Events;
using MultiSync.Models.Item;
using MultiSync.Repository.MS;
using MultiSync.Repository.XML;
using MultiSync.Services;
using Newtonsoft.Json.Linq;

namespace MultiSync.Controllers
{
    public class MSController : Controller
    {
        private readonly EventManager _eventManager;
        private readonly EventHandlerService _eventSubscriber;
        private readonly IMSRepo<MSItem> _msRepo;
        private IMapper _mapper;
        public MSController(EventManager eventManager,EventHandlerService eventSubscriber,IMSRepo<MSItem> mSRepo, IMapper mapper) 
        {
            _eventManager = eventManager;
            _eventSubscriber = eventSubscriber;
            _msRepo = mSRepo;
            _mapper = mapper;
        }

        // GET: MSController
        [Authorize]
        public ActionResult Index()
        {
            var list =  _mapper.Map<List<ItemViewModel>>(_msRepo.GetAll().ToList());
            return View(list);
        }

        // GET: MSController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MSController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: MSController/Create
        [HttpPost]
        [Route("/MSController/Create")]
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
                var item = _mapper.Map<MSItem>(addItem);
                _msRepo.Add(item);
                _eventManager.Publish(new MSItemEventArgs(item,"Create"));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok("success");
        }

        // GET: MSController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MSController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
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

        // GET: MSController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MSController/Delete/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
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
