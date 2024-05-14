using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PWApp.Data.Models
{
    public class ThemeBinder
    {
        [PrimaryKey]
        public string ThemeId { get; set; }
        public bool IsDark { get; set; }
    }
}
