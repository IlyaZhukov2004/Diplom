using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplom
{
    internal class AppData
    {
        private static Пример3Entities1 _context;
        public static Пример3Entities1 GetContext()
        {
            if (_context == null)
                _context = new Пример3Entities1();
            return _context;
        }
    }
}
