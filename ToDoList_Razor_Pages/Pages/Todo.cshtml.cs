using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;

namespace ToDoList_Razor_Pages.Pages
{


    public class TodoModel : PageModel
    {

      
        private readonly IMemoryCache _cache;
        public TodoModel(IMemoryCache cache)
        {
            _cache = cache;
        }
       

        [BindProperty]
        public List<TodoItem> todos { get;set; } = new List<TodoItem>();
       

        public async Task OnGetAsync()
        {
           List<TodoItem> todocache;
            if(_cache.TryGetValue("todos", out todocache))
            {
                    todos = todocache;
            }
        }

        public async Task<IActionResult> OnPostAsync(TodoItem todo)
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

             List<TodoItem> todocache;
            if(_cache.TryGetValue("todos", out todocache))
            {
                    todos = todocache;
            }

            todos.Add(todo);
            _cache.Set<List<TodoItem>>("todos", todos);

            return RedirectToPage();
        }
    }


}
