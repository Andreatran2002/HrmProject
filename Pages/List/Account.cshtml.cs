using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using App.Models;

namespace hrmProject.Pages_List
{
    public class AccountModel : PageModel
    {
        private readonly App.Models.AppDbContext _context;

        public AccountModel(App.Models.AppDbContext context)
        {
            _context = context;
        }

        public IList<Post> Post { get;set; }

        public async Task OnGetAsync()
        {
            Post = await _context.Posts
                .Include(p => p.User).ToListAsync();
        }
    }
}
