using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using _5051.Models;

namespace _5051.Backend
{

    /// <summary>
    /// Datasource Interface for Games
    /// </summary>
    public interface IGameInterface
    {
        GameModel Create(GameModel data);
        GameModel Read(string id);
        GameModel Update(GameModel data);
        bool Delete(string id);
        List<GameModel> Index();
        void Reset();
        void LoadDataSet(DataSourceDataSetEnum setEnum);
    }
}