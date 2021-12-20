using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotizBuch.Controllers.Interfaces;
using NotizBuch.Models;
using NotizBuch.SQL.Connections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace NotizBuch.Controllers
{
    public class NotesController : Controller, IControllerBase<Notes>
    {
        public NotesConnection NotesConnection { get; set; }

        public NotesController()
        {
            NotesConnection = new NotesConnection();
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            NotesConnection.Delete(id);
            return Ok();
        }


        [HttpPost]
        [Authorize]
        public IActionResult Edit([FromBody] Notes notes)
        {
            NotesConnection.Edit(notes);
            return Ok();
        }

        [Authorize]
        public IActionResult Get(int id)
        {
            return new JsonResult(NotesConnection.Get(id), new JsonSerializerOptions());
        }
        [Authorize]
        public IActionResult GetAll()
        {
            return new JsonResult(NotesConnection.GetAll(), new JsonSerializerOptions());
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public IActionResult Insert([FromBody] Notes notes)
        {
            NotesConnection.Insert(notes);
            return Ok();
        }
    }
}
