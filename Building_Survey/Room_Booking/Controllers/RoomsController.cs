
using Room_Booking.ViewModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Room_Booking.Controllers
{
    public class RoomsController : Controller
    {
        private DbEntities db = new DbEntities();
        private List<RoomVM> lstRoomsVm = new List<RoomVM>();
        
        // GET: Rooms
        public ActionResult Index()
        {   
            foreach (Room item in db.Rooms)
            {
                lstRoomsVm.Add(ToVM(item));
            }
            return View(lstRoomsVm);
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(ToVM(room));
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "name")] Room room)
        {
            if (ModelState.IsValid)
            {
                room.status=true;
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(room);
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(ToVM(room));
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,status")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(ToVM(room));
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        private RoomVM ToVM(Room item)
        {
            RoomVM newRoom= new RoomVM();
            newRoom.Id= item.Id;
            newRoom.name= item.name;
            Task<StaffBooking> results= db.StaffBookings.SqlQuery("select * from StaffBooking where id={0} and status={1}",item.Id,true).FirstOrDefaultAsync();
            newRoom.status=item.status;
            return newRoom;
        }
    }
}
