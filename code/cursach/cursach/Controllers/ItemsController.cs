using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//using cursach.ListObject;
using cursach.Data;
using cursach.Models;
using System.Drawing.Printing;

namespace cursach.Controllers
{
    public class ItemsController : Controller
    {
        private readonly CursachClientAddItemContext _context;

        public ItemsController(CursachClientAddItemContext context)
        {
            _context = context;
        }

        // GET: Items
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 5)
        {
            var query = _context.Items
                .Include(i => i.Available)
                .Include(i => i.Client)
                .Include(i => i.ItemType)
                .Include(i => i.Reserved)
                .Include(i => i.Sold)
                .AsQueryable();

            int totalItems = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
            .ToListAsync();

            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.CurrentPage = pageNumber;

            return View(items);
        }

        public async Task<IActionResult> SelectName()
        {
            return View();
        }

        // GET: Items/Details/5
        [HttpGet("Items/Details/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.ItemType)
                .Include(i => i.Reserved)
                .Include(i => i.Sold)
                .Include(i => i.Client)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Details?itemName
        [HttpGet("Items/Details")]
        public async Task<IActionResult> Details(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName))
            {
                return NotFound("itemName is required.");
            }

            var item = await _context.Items
                .Include(i => i.ItemType)
                .Include(i => i.Reserved)
                .Include(i => i.Sold)
                .Include(i => i.Client)
                .FirstOrDefaultAsync(i => i.ItemName == itemName);

            if (item == null)
            {
                return NotFound($"No item found with Item Name: {itemName}");
            }

            return View(item);
        }

        // GET: Items/Create
        public IActionResult Create()
        {
            ViewData["ItemType"] = new SelectList(_context.ItemTypes, "ItemTypeId", "NameType");

            ViewData["Available"] = new SelectList(_context.Availables, "AvailableId", "DateListed");
            ViewData["Reserved"] = new SelectList(_context.Reserveds, "ReservedId", "ReservedDate");
            ViewData["Sold"] = new SelectList(_context.Solds, "SoldId", "SaleDate");
            ViewData["StatusOptions"] = new SelectList(new List<string> { "Available", "Reserved", "Sold" });

            ViewData["Client"] = new SelectList(_context.Clients, "ClientId", "LastName"); 

            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemId,ItemName,ItemTypeId,Price,Status,AvailableId,ReservedId,SoldId,ClientId")] Item item)
        {
            if (ModelState.IsValid)
            {
                if (item.Status == "Available")
                {
                    item.ReservedId = null;
                    item.SoldId = null;
                    item.ClientId = null;
                }
                else if (item.Status == "Reserved")
                {
                    item.AvailableId = null;
                    item.SoldId = null;
                }
                else if (item.Status == "Sold")
                {
                    item.AvailableId = null;
                    item.ReservedId = null;
                }

                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemType"] = new SelectList(_context.ItemTypes, "ItemTypeId", "NameType", item.ItemTypeId);

            ViewData["Available"] = new SelectList(_context.Availables, "AvailableId", "DateListed", item.AvailableId);
            ViewData["Reserved"] = new SelectList(_context.Reserveds, "ReservedId", "ReservedDate", item.ReservedId);
            ViewData["Sold"] = new SelectList(_context.Solds, "SoldId", "SaleDate", item.SoldId);

            ViewData["Client"] = new SelectList(_context.Clients, "ClientId", "LastName", item.ClientId);

            return View(item);
        }

        // GET: Items/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            ViewData["ItemType"] = new SelectList(_context.ItemTypes, "ItemTypeId", "NameType", item.ItemTypeId);

            ViewData["Available"] = new SelectList(_context.Availables, "AvailableId", "DateListed", item.AvailableId);
            ViewData["Reserved"] = new SelectList(_context.Reserveds, "ReservedId", "ReservedDate", item.ReservedId);
            ViewData["Sold"] = new SelectList(_context.Solds, "SoldId", "SaleDate", item.SoldId);
            ViewData["StatusOptions"] = new SelectList(new List<string> { "Available", "Reserved", "Sold" });

            ViewData["Client"] = new SelectList(_context.Clients, "ClientId", "LastName", item.ClientId);
            
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ItemId,ItemName,ItemTypeId,Price,Status,AvailableId,ReservedId,SoldId,ClientId")] Item item)
        {
            if (id != item.ItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                if (item.Status == "Available")
                {
                    item.ReservedId = null;
                    item.SoldId = null;
                    item.ClientId = null;
                }
                else if (item.Status == "Reserved")
                {
                    item.AvailableId = null;
                    item.SoldId = null;
                }
                else if (item.Status == "Sold")
                {
                    item.AvailableId = null;
                    item.ReservedId = null;
                }

                try
                {
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["ItemType"] = new SelectList(_context.ItemTypes, "ItemTypeId", "NameType", item.ItemTypeId);

            ViewData["Available"] = new SelectList(_context.Availables, "AvailableId", "DateListed", item.AvailableId);
            ViewData["Reserved"] = new SelectList(_context.Reserveds, "ReservedId", "ReservedDate", item.ReservedId);
            ViewData["Sold"] = new SelectList(_context.Solds, "SoldId", "SaleDate", item.SoldId);

            ViewData["Client"] = new SelectList(_context.Clients, "ClientId", "LastName", item.ClientId);
            return View(item);
        }

        // GET: Items/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.Items
                .Include(i => i.Available)
                .Include(i => i.Client)
                .Include(i => i.ItemType)
                .Include(i => i.Reserved)
                .Include(i => i.Sold)
                .FirstOrDefaultAsync(m => m.ItemId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item != null)
            {
                _context.Items.Remove(item);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.Items.Any(e => e.ItemId == id);
        }

        // GET: Items/Grouping
        public async Task<IActionResult> Grouping(int pageNumber = 1, int pageSize = 5)
        {
            // Получаем все элементы с необходимыми связями
            var items = await _context.Items
                .Include(i => i.ItemType)
                .Include(i => i.Available)
                .Include(i => i.Reserved)
                .Include(i => i.Sold)
                .Include(i => i.Client)
                .Where(i => i.Status == "Reserved")
                .ToListAsync();

            // Группируем по дате резервирования
            var groupedItems = items
                .GroupBy(i => i.Reserved.ReservedDate)
                .ToList();

            // Передача данных в представление через ViewData
            ViewData["GroupedItems"] = groupedItems;

            // Пагинация
            int totalItems = items.Count;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.CurrentPage = pageNumber;

            return View();
        }

        //FilterClient
        public async Task<IActionResult> SelectFilterClient()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            var reservedList = await _context.Reserveds
                .Where(r => r.ExpirationDate.HasValue && r.ExpirationDate.Value < today)
                .Select(r => new SelectListItem
                {
                    Value = r.ReservedId.ToString(),
                    Text = r.ExpirationDate.HasValue
                        ? r.ExpirationDate.Value.ToString("dd.MM.yyyy")
                        : ""
                })
                .ToListAsync();

            // Передаем список в ViewData
            ViewData["Reserved"] = reservedList;

            return View();
        }


        // GET: Items/Grouping
        public async Task<IActionResult> FilterClient(int? ReservedId)
        {
            var itemsQuery = _context.Items
                .Include(i => i.ItemType)
                .Include(i => i.Available)
                .Include(i => i.Reserved)
                .Include(i => i.Sold)
                .Include(i => i.Client)
                .Where(i => i.Status == "Reserved");

            if (ReservedId.HasValue)
            {
                itemsQuery = itemsQuery.Where(i => i.ReservedId == ReservedId.Value);
            }

            // Получаем все элементы с необходимыми данными
            var items = await itemsQuery.ToListAsync();

            // Группируем элементы по цене
            var groupedItems = items
                .GroupBy(i => i.Price)
                .Select(g => new GroupedItemViewModel
                {
                    Price = g.Key,
                    Items = g.ToList()
                })
                .ToList();

            // Передаем данные в ViewData
            ViewData["GroupedItems"] = groupedItems;

            return View();
        }

        //FilterItems
        public async Task<IActionResult> SelectFilterItems()
        {
            return View();
        }

        public async Task<IActionResult> FilterItems(string itemName)
        {
            var itemsQuery = _context.Items
                .Include(i => i.ItemType)
                .Include(i => i.Available)
                .Include(i => i.Reserved)
                .Include(i => i.Sold)
                .Include(i => i.Client)
                .Where(i => i.Status == "Sold")
                .Where(i => EF.Functions.Like(i.ItemName, $"%{itemName}%"));  // Здесь используем LIKE

            var items = await itemsQuery.ToListAsync();

            if (items.Any())
            {
                Console.WriteLine($"Found {items.Count} items matching the search criteria.");
            }
            else
            {
                Console.WriteLine("No items found matching the search criteria.");
            }

            // Создаем модель для отображения
            var viewModel = items.Select(i => new FilteredItemViewModel
            {
                ItemId = i.ItemId,
                ItemName = i.ItemName,
                Price = i.Price,
                ClientName = $"{i.Client?.FirstName} {i.Client?.LastName}",
                SoldDate = i.Sold != null ? (i.Sold.SaleDate.HasValue ? i.Sold.SaleDate.Value.ToDateTime(new TimeOnly(0, 0)) : (DateTime?)null) : (DateTime?)null
            }).ToList();

            return View(viewModel);  // Передаем viewModel напрямую
        }





    }
}
