using System.Web.Mvc;
using _5051.Models;
using _5051.Backend;

namespace _5051.Controllers
{
    /// <summary>
    /// Controller for the Game Engine
    /// </summary>

    public class GameController : BaseController
    {
        /// <summary>
        /// This updates the Game based on the information posted from the udpate page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetIterationNumber([Bind(Include=
                                        "Id," +
                                        "")] GameModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return Json(new
                {
                    Error = true,
                    Msg = "Invalid State",
                    Data = string.Empty,
                });
            }

            if (data == null)
            {
                return Json(new
                {
                    Error = true,
                    Msg = "Invalid State",
                    Data = string.Empty,
                });
            }

            if (string.IsNullOrEmpty(data.Id))
            {
                return Json(new
                {
                    Error = true,
                    Msg = "Invalid State",
                    Data = string.Empty,
                });
            }

            var DataResult = DataSourceBackend.Instance.GameBackend.Simulation();

            return Json(new
            {
                Error = false,
                Msg = "OK",
                Data = DataResult.ToString(),
            });
        }

        /// <summary>
        /// This updates the Game based on the information posted from the udpate page
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetRefreshRate()
        {
            var data = DataSourceBackend.Instance.GameBackend.GetDefault();

            return Json(new
            {
                Error = false,
                Msg = "OK",
                Data = data.RefreshRate.TotalMilliseconds,
            });
        }

        /// <summary>
        /// This updates the Game based on the information posted from the udpate page
        /// And Returns the results for the student passed in
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Student results as json package</returns>
        [HttpPost]
        public ActionResult GetResults([Bind(Include=
                                        "Id," +
                                        "")] StudentInputModel data)
        {
            if (!ModelState.IsValid)
            {
                // Send back for edit
                return Json(new
                {
                    Error = true,
                    Msg = "Invalid State",
                    Data = string.Empty,
                });
            }

            //var DataResult = DataSourceBackend.Instance.GameBackend.Simulation();
            var result = DataSourceBackend.Instance.GameBackend.GetResult(data.Id);

            return Json(new
            {
                Error = false,
                Msg = "OK",
                Data = result,
            });
        }
    }
}
