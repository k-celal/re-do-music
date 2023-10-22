using ReDoMusic.Domain.Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReDoMusic.Domain.Entites
{
    public class Instrument : EntityBase<Guid>
    {
        public string Name { get; set; }
        public Brand Brand { get; set; }
        public string Model { get; set; }
        public Color Color { get; set; }
        public DateTime? ProductionYear { get; set; }
        public decimal Price { get; set; }

    }
}
