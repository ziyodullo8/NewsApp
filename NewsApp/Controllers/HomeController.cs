using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.PostModel;
using System.Diagnostics;


namespace NewsApp.Controllers
{
    public class HomeController : Controller
    {
 
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHost;
        public HomeController(DataContext dataContext, IWebHostEnvironment webHost )
        {
            _webHost= webHost;
            _dataContext = dataContext;
        }

        

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Show()
        {
            return View();
        }
       
        [HttpGet]
        public async Task<IActionResult> show()
        {
            var news = await _dataContext.News.ToListAsync();
            return View(news);
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostNews postNews)
        {
            if(!ModelState.IsValid) 
            {
                return View(postNews);
            }

            string[] ImageExcs = { "jpg", "png" };
            string ptExc = postNews.Image.FileName.Split('.').Last().ToLower();
            if (ImageExcs.Contains(ptExc))
                throw new KeyNotFoundException($"Rasmni faqat " + string.Join(",", ptExc + "Farmatlarda yuborish kerak"));
            string UIname = Guid.NewGuid().ToString();
            UIname += "." + ptExc;
            UIname = Path.Combine("Images", "News", UIname);

            string ImagePath = Path.Combine(_webHost.WebRootPath, UIname);

            NewsModels news = new NewsModels();

            using (var stream = System.IO.File.Open(ImagePath, FileMode.OpenOrCreate))
            {
                postNews.Image.CopyTo(stream);
                news.ImageUrl = ImagePath;
            }

            news.Title = postNews.Title;
            news.Disciription=postNews.Disciription;
            await _dataContext.News.AddAsync(news);
            await _dataContext.SaveChangesAsync();
            return View(news);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PostNews postNews)
        {
            var newss = await _dataContext.News.FirstOrDefaultAsync(p => p.Id == id);
            if (newss is null)
                throw new KeyNotFoundException("Bunday Yangilik Mavjud emas");
            if (newss.ImageUrl is not null)
            {
                string fullPath = Path.Combine(_webHost.WebRootPath, newss.ImageUrl);
                await using (var stream = System.IO.File.Open(fullPath, FileMode.OpenOrCreate))
                {
                    await postNews.Image.CopyToAsync(stream);
                }
            }
            if (!string.IsNullOrEmpty(postNews.Title))
                newss.Title = postNews.Title;
            if(!string.IsNullOrEmpty(postNews.Disciription))
                newss.Disciription= postNews.Disciription;
            _dataContext.News.Update(newss);
            await _dataContext.SaveChangesAsync();
            return View(newss);

        }
    }
}