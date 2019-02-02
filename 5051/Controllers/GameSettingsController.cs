using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    /// <summary>
    /// School Dismissal Settings defaults to a single record.  So no Create or Delete, just Read, and Update
    /// </summary>

    public class GameSettingsController : BaseController
    {
        // The Backend Data source
        private GameBackend GameBackend = GameBackend.Instance;

        /// <summary>
        /// Read information on a single GameSettings
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: GameSettings/Details/5
        public ActionResult Read(string id = null)
        {
            var myData = GameBackend.Read(id);
            if (myData == null)
            {
                // If no ID is passed in, get the first one.
                if (id == null)
                {
                    myData = GameBackend.GetDefault();
                }
                if (myData == null)
                {
                    return RedirectToAction("Error", "Home");
                }
            }

            return View(myData);
        }

        /// <summary>
        /// This will show the details of the GameSettings to update
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: GameSettings/Edit/5
        public ActionResult Update(string id = null)
        {
            var myData = GameBackend.Read(id);
            if (myData == null)
            {
                return RedirectToAction("Error", "Home");
            }

            return View(myData);
        }

        /// <summary>
        /// This updates the GameSettings based on the information posted from the udpate page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        // POST: GameSettings/Update/5
        [HttpPost]
        public ActionResult Update([Bind(Include=
                                        "Id," +
                                        "Enabled," +
                                        "RefreshRate,"+
                                        "TimeIteration,"+
                                        "")] GameModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return View(data);
            }

            if (data == null)
            {
                // Send to Error Page
                return RedirectToAction("Error", "Home");
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                // Send back for edit
                return View(data);
            }

            GameBackend.Update(data);

            return RedirectToAction("Settings", "Admin");
        }
    }
}
