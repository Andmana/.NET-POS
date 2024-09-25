using Microsoft.AspNetCore.Mvc;
using Xpos341.Models;

namespace Xpos341.Controllers
{
    public class FriendsController : Controller
    {
        private static List<Friend> friends = new List<Friend>()
            {
                new Friend() { Id = 1, Name = "Sheva", Address = "Palembang" },
                new Friend() { Id = 2, Name = "Dave", Address = "Tangerang" },
                new Friend() { Id = 3, Name = "Vendra", Address = "Pemalang" },
            };

        // Index table
        public IActionResult Index()
        {
            ViewBag.ListFriends = friends;
            return View();

            // Another way
            //return View(friends);
        }

        // Form add list
        public IActionResult Create()
        {
            return View();
        }

        // add list -> to table
        [HttpPost]
        public IActionResult Create(Friend friend)
        {
            friends.Add(friend);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            //Friend friend = friends.Where(friend => friend.Id == id).FirstOrDefault();
            Friend friend = friends.FirstOrDefault(friend => friend.Id == id);
            //Friend friend = friends.Find(x => x.Id == id)!;
            return View(friend);
        }

        [HttpPost]
        public IActionResult Edit(Friend data)
        {
            Friend friend = friends.Find(x => x.Id == data.Id)!;
            int idx = friends.IndexOf(friend);
            if (idx != -1)
            {
                friends[idx].Name = data.Name;
                friends[idx].Address = data.Address;
            }
            return RedirectToAction("Index");
        }
        public IActionResult Detail(int id)
        {
            Friend friend = friends.Find(x => x.Id == id)!;
            return View(friend);
        }
        public IActionResult Delete(int id)
        {
            Friend friend = friends.Find(x => x.Id == id)!;
            return View(friend);
        }
        [HttpPost]
        public IActionResult Delete(Friend data)
        {
            Friend friend = friends.Find(x => x.Id == data.Id)!;
            friends.Remove(friend);
            return RedirectToAction("Index");
        }
    }
}
