using Microsoft.AspNetCore.Mvc;
using SimpleCrudApi.Models;
using SimpleCrudApi.Services;
using System.Collections.Generic;

namespace SimpleCrudApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ItemService _itemService;

        public ItemsController(ItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: api/items
        [HttpGet]
        public ActionResult<List<Item>> GetAll() => _itemService.GetAll();

        // GET: api/items/{id}
        [HttpGet("{id}")]
        public ActionResult<Item> Get(int id)
        {
            var item = _itemService.GetById(id);
            if (item == null) return NotFound();
            return item;
        }

        // POST: api/items
        [HttpPost]
        public ActionResult<Item> Create(Item item)
        {
            var newItem = _itemService.Add(item);
            return CreatedAtAction(nameof(Get), new { id = newItem.Id }, newItem);
        }

        // PUT: api/items/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, Item item)
        {
            var result = _itemService.Update(id, item);
            if (!result) return NotFound();
            return NoContent();
        }

        // DELETE: api/items/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _itemService.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
